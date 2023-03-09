using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTester : MonoBehaviour
{
    private Mortality _towerHealth;

    [SerializeField]
    private float _strength = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        _towerHealth = GameObject.FindGameObjectWithTag("Base").GetComponent<Mortality>();
    }

    public void DamageTower()
    {
        _towerHealth.TakeDamage(_strength);
    }

    public void HealTower()
    {
        _towerHealth.Heal(_strength);
        Debug.Log("Ahh");

    }
}
