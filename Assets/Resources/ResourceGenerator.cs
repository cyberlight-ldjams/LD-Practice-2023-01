using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public List<Resource> resourceTypes;

    [SerializeField]
    Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        if (grid == null)
        {
            GameObject gridGO = GameObject.FindGameObjectWithTag("Grid");
            grid = gridGO.GetComponent<Grid>();
        }

        GenerateResourcePatch(new Resource("Rock", 0.5f, 0.5f), new Vector2(0, 0));
    }

    public bool GenerateResourcePatch(Resource resource, Vector2 location)
    {
        return createResourcePatch(resource, location);
    }

    private bool createResourcePatch(Resource resource, Vector2 location)
    {
        Debug.Log("Place first resource");
        bool placed = placeResource(resource, location);

        int count = 0;
        if (placed)
        {
            GridCell[] gca = grid.GetAdjecentCells(location, true);
            
            while (count == 0 || Random.Range(0, 1f) < resource.spread)
            {
                foreach (GridCell gc in gca)
                {
                    // Place more resources
                    if (Random.Range(0, 1f) < resource.rarity)
                    {
                        if (!gc.HasGameObject())
                        {
                            placeResource(new Resource
                                (resource.type, resource.rarity, resource.spread), gc);
                        }
                    }
                }

                GridCell next = gca[Random.Range(0, gca.Length - 1)];
                gca = grid.GetAdjecentCells(next, true);
                count++;
            }

            return true;
        }
        return false;
    }

    private bool placeResource(Resource resource, Vector2 location)
    {
        GridCell center = grid.GetCell(location);

        return placeResource(resource, center);
    }

    private bool placeResource(Resource resource, GridCell location)
    {
        location = grid.GetCell(location);
        bool placed = location.SetGameObject(resource.resource);
        resource.resource.transform.position =
            new Vector3(location.Location.x, 2f, location.Location.y);

        return placed;
    }


}
