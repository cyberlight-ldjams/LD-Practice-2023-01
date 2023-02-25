using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Resource", menuName ="Resources/Resource Type")]
public class ResourceType : ScriptableObject
{
    public Type BaseResource;

    public string Name;
    public int Rarity;

    public float Spread;

    public Image Icon;

    public GameObject Model;

    public enum Type
    {
        STONE,
        GOLD,
        WOOD,
        FOOD
    }
}


