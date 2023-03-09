using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Mortality : MonoBehaviour
{

    [SerializeField]
    private float _maxHealth = 100f;

    private int _healthPercentage = 100;

    [SerializeField]
    private float _health = 100f;

    private Dictionary<string, Action> _damageCallbacks = new Dictionary<string, Action>();

    private Action _onDeath;

    #region Event Registrations

    private void RegisterDamageChanged(bool dropping, int percentage, Action method, bool unregister = false)
    {
        string key = ((dropping) ? "-" : "+") + percentage;

        if (!_damageCallbacks.ContainsKey(key))
            _damageCallbacks[key] = method;
        else
        {
            _damageCallbacks[key] += method;
        }
    }

    /**
     * Tower.WhenHealthDropsBelow(10, () => Debug.Log("We gonna Die!");
     */
    public void WhenHealthDropsBelow(int percentage, Action method)
    {
        RegisterDamageChanged(dropping: true, percentage, method);
    }

    /**
     * Tower.WhenHealthRisesAbove(50, () => Debug.Log("What a save!");
     * 
     */
    public void WhenHealthRisesAbove(int percentage, Action method)
    {
        RegisterDamageChanged(dropping: false, percentage, method);
    }

    public void RemoveHealthAlerts(int atPercentage, Action method)
    {
        _damageCallbacks["+" + atPercentage] -= method;
        _damageCallbacks["-" + atPercentage] -= method;
    }

    public void RegisterOnDeath(Action method)
    {
        _onDeath += method;
    }

    public void UnregisterOnDeath(Action method)
    {
        _onDeath -= method;
    }


    #endregion

    public void TakeDamage(float amount)
    {
        float previousHealth = _health;    
        _health -= amount;

        string key = (_health < previousHealth)? "-" : "+";

        int percentage = (int)(_health / _maxHealth) * 100;

        //check if anyone cares about a damage percentage that we passed
        foreach(KeyValuePair<string, Action> entry in _damageCallbacks)
        {
            //Make sure they care about the type of change we're making
            if(!entry.Key.StartsWith(key))
            {
                continue;
            }
            int KeyValue = int.Parse(key.Substring(1));

            //Make sure that the percentage they care about is between the new and old healths.
            bool calculation = _healthPercentage > KeyValue && KeyValue >= percentage;

            //The calculation flips if we care about healing instead of taking damage.
            if ((key == "-" && calculation) ||
                (key == "+" && !calculation))
            {
                entry.Value.Invoke();

            }
        }


        _healthPercentage = Math.Clamp(percentage, 0, 100);
        _health = Mathf.Clamp(_health, 0f, _maxHealth);

        //Did we die?
        if (_health == 0f)
        {
            _onDeath(); //same as _onDeath.Invoke();
        }
    }


    public void Heal(float amount)
    {
        TakeDamage(amount * -1);
    }
}
