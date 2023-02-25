using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom/Game Resource", fileName = "Resource")]
public class Resource : ScriptableObject
{
    private GameObject _chosenModel = null;

    public string Name;

    [Range(0f, 1f)]
    public float Rarity;

    [Range(0f, 1f)]
    public float Spread;

    [Range(0f, 5f)]
    public float HarvestSpeed = 1.0f;

    [Range(0f, 10f)]
    public float HarvestLevel = 0f;

    [SerializeField]
    public Vector2Int AmountRange = new Vector2Int(3, 7);

    public Image Icon;  //can set to default image?

    public List<GameObject> modelOptions;

    public GameObject GetRandomModel()
    {
        _chosenModel = modelOptions[Random.Range(0, modelOptions.Count)];

        return _chosenModel;
    }

    public GameObject GetChosenModel()
    {
        if (_chosenModel == null)
        {
            _chosenModel = modelOptions[Random.Range(0, modelOptions.Count)];
        }

        return _chosenModel;
    }
}
