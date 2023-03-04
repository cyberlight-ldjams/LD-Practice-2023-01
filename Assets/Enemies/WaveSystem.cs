using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private bool Paused;

    [SerializeField]
    private int FirstWaveTime = 45;

    [SerializeField]
    private int WaveTimeDecrement = 5;

    [SerializeField]
    private int WaveTimeMinimum = 20;

    [SerializeField]
    private int FirstEnemyValue = 30;

    [SerializeField]
    private int EnemyValueIncrement = 20;

    [SerializeField]
    private int EnemyValueMax = 150;

    private int nextWaveTime;

    private int nextEnemyValue;

    private float currentTime;

    private float lastWaveTime;

    // Start is called before the first frame update
    void Start()
    {
        nextWaveTime = FirstWaveTime;
        nextEnemyValue = FirstEnemyValue;
        currentTime = 0;
        lastWaveTime = 0;
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            currentTime += Time.deltaTime;
        }

        if ((currentTime - lastWaveTime) > nextWaveTime)
        {
            SpawnWave(nextEnemyValue);

            // Decrement the wave time, if we still need to
            if (WaveTimeDecrement > 0)
            {
                nextWaveTime -= WaveTimeDecrement;

                if (nextWaveTime <= WaveTimeMinimum)
                {
                    nextWaveTime = WaveTimeMinimum;

                    // We've reached the minimum, stop decrementing
                    WaveTimeDecrement = 0;
                }
            }

            // Increment the enemy value, if we still need to
            if (EnemyValueIncrement > 0)
            {
                nextEnemyValue += EnemyValueIncrement;

                if (nextEnemyValue >= EnemyValueMax)
                {
                    nextEnemyValue = EnemyValueMax;

                    // We've reached the maximum, stop incrementing
                    EnemyValueIncrement = 0;
                }
            }

            lastWaveTime = currentTime;
        }
    }

    private void SpawnWave(int EnemyValue)
    {

    }
}
