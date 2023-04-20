using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Resource> resourceTypes;

    [SerializeField]
    private Vector2Int resourceRange = new Vector2Int(50, 50);

    [SerializeField]
    private Grid grid;

    // Start is called before the first frame update
    void Start()
    {
        if (grid == null)
        {
            GameObject gridGO = GameObject.FindGameObjectWithTag("Grid");
            grid = gridGO.GetComponent<Grid>();
        }

        bool spotAvailable;
        int count;
        int xloc = 0;
        int yloc = 0;

        foreach (Resource r in resourceTypes)
        {
            int resourcePatchCount = Random.Range(r.AmountRange.x, r.AmountRange.y);
            for (int i = 0; i < resourcePatchCount; i++)
            {
                count = 0;
                spotAvailable = false;
                while (!spotAvailable)
                {
                    xloc = Random.Range(-resourceRange.x, resourceRange.x);
                    yloc = Random.Range(-resourceRange.y, resourceRange.y);

                    if (!grid.GetCell(xloc, yloc).HasGameObject())
                    {
                        spotAvailable = true;
                    }
                    else if (count == 50)
                    {
                        spotAvailable = true;
                    }
                }

                if (count < 50)
                {
                    createResourcePatch(r, new Vector2(xloc, yloc));
                }
            }
        }
    }

    public bool GenerateResourcePatch(Resource resource, Vector2 location)
    {
        return createResourcePatch(resource, location);
    }

    private bool createResourcePatch(Resource resource, Vector2 location)
    {
        bool placed = false;
        if (!grid.GetCell(location).HasGameObject())
        {
            placed = placeResource(resource, location);
        }

        int count = 0;
        if (placed)
        {
            GridCell[] gca = grid.GetAdjecentCells(location, true);
            
            while (count == 0 || Random.Range(0, 1f) < resource.Spread)
            {
                foreach (GridCell gc in gca)
                {
                    // Place more resources
                    if (Random.Range(0, 1f) < resource.Rarity)
                    {
                        if (!gc.HasGameObject())
                        {
                            placeResource(resource, gc);
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
        GameObject model = resource.GetRandomModel();
        bool placed = location.SetGameObject(model);
        Instantiate(model, new Vector3(location.Location.x, 2f, location.Location.y), 
            Quaternion.identity);
        model.name = resource.name + " " + location.Location;
        return placed;
    }


}
