using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _cellSize = 1;

    private List<GridCell> _gridCells;

    public GameObject Selected { get; set; }

    public Grid()
    {
        _gridCells = new List<GridCell>();
    }

    /**
     * Gets the cell associated with that location
     * 
     * If it does not exist yet, it creates it
     */
    public GridCell GetCell(float x, float y)
    {
        GridCell findGC = null;
        Vector2 loc = GetCellLocation(x, y);

        // See if that grid cell already exists
        foreach (GridCell gc in _gridCells)
        {
            if (gc.Location.Equals(loc))
            {
                findGC = gc;
                break;
            }
        }

        if (findGC != null)
        {
            return findGC;
        } // else
        findGC = new GridCell(loc);
        _gridCells.Add(findGC);
        return findGC;
    }

    public GridCell GetCell(Vector2 location)
    {
        return GetCell(location.x, location.y);
    }

    public GridCell GetCell(Vector3 location)
    {
        return GetCell(location.x, location.z);
    }

    public Vector2 GetCellLocation(float x, float y)
    {
        return new Vector2(RoundToCellSize(x), RoundToCellSize(y));
    }

    public GridCell GetCellLocation(Vector2 location)
    {
        return GetCell(location.x, location.y);
    }

    public GridCell GetCellLocation(Vector3 location)
    {
        return GetCell(location.x, location.z);
    }

    private float RoundToCellSize(float x)
    {
        float multiple = Mathf.Round(x / _cellSize);

        return Mathf.Round(multiple * _cellSize);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
