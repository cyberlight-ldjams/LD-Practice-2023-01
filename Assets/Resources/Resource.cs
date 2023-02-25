using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    public string type { get; private set; }

    [SerializeField]
    public float rarity { get; private set; }

    [SerializeField]
    public float spread { get; private set; }

    [SerializeField]
    public GameObject resource { get; private set; }

    void Start()
    {
        if (resource == null)
        {
            resource = new GameObject();
        }
    }

    /**
     * The "rarity" value of the resource determines how likely a new resource tile 
     * will spawn next to the starting point. 
     * The "spread" value determines how likely there is to be a new starting point 
     * next to the original for the patch to keep growing.
     */
    public Resource(string type, float rarity, float spread)
    {
        this.type = type;
        this.rarity = rarity;
        this.spread = spread;


        //TODO - Make this generate a prefab
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 2f, 0);
        cube.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        cube.AddComponent<Moveable>();
        resource = cube;
    }
}
