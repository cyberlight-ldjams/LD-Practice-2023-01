using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private Enemy[] possibleEnemies;

    [SerializeField]
    private Vector3 spawnPoint;

    // Stored as a reference for the possible enemies array
    private List<int> enemies;

    private int nextEnemy;

    private void Start()
    {
        nextEnemy = 0;
        enemies = new List<int>();
    }

    public void SetSpawnPoint(Vector3 location)
    {
        spawnPoint = location;
    }

    public void CreateWave(int EnemyValue)
    {
        // To prevent infinite loops, if we go more than
        // maxCount times, just spawn whatever we roll
        int maxCount = 100000;
        int count = 0;

        int currentEnemyValue = 0;
        while (currentEnemyValue < EnemyValue)
        {
            // Roll an enemy value
            int next = Random.Range(0, possibleEnemies.Length);
            int nextValue = possibleEnemies[next].Value;

            if ((nextValue + currentEnemyValue) <= EnemyValue || (count > maxCount))
            {
                enemies.Add(next);
                currentEnemyValue += nextValue;
            }

            count++;
        }

        nextEnemy = 0;
    }

    public void SpawnNextEnemy()
    {
        if (nextEnemy < enemies.Count)
        {
            Instantiate(possibleEnemies[enemies[nextEnemy]], spawnPoint, Quaternion.identity);
            nextEnemy++;
        }
    }

    public void SpawnWholeWave()
    {
        foreach (int e in enemies)
        {
            SpawnNextEnemy();
        }
    }
}
