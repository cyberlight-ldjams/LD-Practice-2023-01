using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;
    private Mortality _towerHealth;

    private void Awake()
    {
        _towerHealth = GameObject.FindGameObjectWithTag("Base")
            .GetComponent<Mortality>();

        _towerHealth.RegisterHealthChanged(SetHealth);

        
    }

    private void SetHealth(float health)
    {
        //as a percentage
        _healthBar.fillAmount = (health / _towerHealth.GetMaxHealth());
    }
}
