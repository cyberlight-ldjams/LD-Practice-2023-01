using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private List<Resource> resourceTypes;

    [SerializeField]
    private Vector2Int resourceRange = new Vector2Int(50, 50);

    [SerializeField]
    private Vector2Int stonePatchRange = new Vector2Int(3, 7);

    [SerializeField]
    private float stoneRarity = 0.5f;

    [SerializeField]
    private float stoneSpread = 0.5f;

    [SerializeField]
    private Vector2Int woodPatchRange = new Vector2Int(3, 7);

    [SerializeField]
    private float woodRarity = 0.5f;

    [SerializeField]
    private float woodSpread = 0.5f;

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

        Resource stone = new Resource("Stone", stoneRarity, stoneSpread);
        Resource wood = new Resource("Wood", woodRarity, woodSpread);

        resourceTypes.Add(stone);
        resourceTypes.Add(wood);

        bool spotAvailable = false;
        int count = 0;
        int xloc = 0;
        int yloc = 0;
        int stonePatchCount = Random.Range(stonePatchRange.x, stonePatchRange.y);
        for (int i = 0; i < stonePatchCount; i++)
        {
            while (!spotAvailable)
            {
                xloc = Random.Range(-resourceRange.x, resourceRange.x);
                yloc = Random.Range(-resourceRange.y, resourceRange.y);

                if (!grid.GetCell(xloc, yloc).HasGameObject())
                {
                    spotAvailable = true;
                    count++;
                } else if (count == 50)
                {
                    spotAvailable = true;
                }
            }

            if (count < 50)
            {
                createResourcePatch(stone, new Vector2(xloc, yloc));
            }
        }

        spotAvailable = false;
        count = 0;
        int woodPatchCount = Random.Range(woodPatchRange.x, woodPatchRange.y);
        for (int i = 0; i < woodPatchCount; i++)
        {
            while (!spotAvailable)
            {
                xloc = Random.Range(-resourceRange.x, resourceRange.x);
                yloc = Random.Range(-resourceRange.y, resourceRange.y);

                if (!grid.GetCell(xloc, yloc).HasGameObject())
                {
                    spotAvailable = true;
                } else if (count == 50)
                {
                    spotAvailable = true;
                }
            }

            if (count < 50)
            {
                createResourcePatch(wood, new Vector2(xloc, yloc));
            }
        }
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
