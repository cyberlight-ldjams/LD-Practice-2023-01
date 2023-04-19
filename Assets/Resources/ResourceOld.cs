using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOld : MonoBehaviour
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
    public ResourceOld createResource(string type, float rarity, float spread, GameObject prefab)
    {
        this.type = type;
        this.rarity = rarity;
        this.spread = spread;


        //TODO - Make this use a prefab and don't make it moveable
        resource = Instantiate(prefab, new Vector3(0, 2f, 0), Quaternion.identity);
        resource.AddComponent<Moveable>();

        return this;
    }
}
