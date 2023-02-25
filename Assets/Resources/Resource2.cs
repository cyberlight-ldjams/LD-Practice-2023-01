using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom/Game Resource", fileName = "Resource")]
public class Resource2 : ScriptableObject
{

    private GameObject _chosenModel = null;

    public string Name;

    [Range(0f, 1f)]
    public int Rarity;

    [Range(0f, 1f)]
    public float Spread;

    [Range(0f, 5f)]
    public float HarvestSpeed = 1.0f;

    [Range(0f, 10f)]
    public float HarvestLevel = 0f;

    public Image Icon;  //can set to default image?

    public List<GameObject> modelOptions;

    public GameObject GetRandomModel()
    {
        if(_chosenModel == null)
        {
            _chosenModel = modelOptions[Random.Range(0, modelOptions.Count)];
        }

        return _chosenModel;
        
    }

}
