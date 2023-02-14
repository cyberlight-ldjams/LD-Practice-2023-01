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

    public GridCell GetCell(GridCell location)
    {
        return GetCell(location.Location.x, location.Location.y);
    }

    public Vector2 GetCellLocation(float x, float y)
    {
        return new Vector2(RoundToCellSize(x), RoundToCellSize(y));
    }

    public Vector2 GetCellLocation(Vector2 location)
    {
        return new Vector2(RoundToCellSize(location.x), RoundToCellSize(location.y));
    }

    public Vector2 GetCellLocation(Vector3 location)
    {
        return new Vector2(RoundToCellSize(location.x), RoundToCellSize(location.z));
    }

    private float RoundToCellSize(float x)
    {
        float multiple = Mathf.Round(x / _cellSize);

        return Mathf.Round(multiple * _cellSize);
    }

    public GridCell[] GetAdjecentCells(Vector2 location, bool diagonals)
    {
        return GetAdjecentCells(location.x, location.y, diagonals);
    }

    public GridCell[] GetAdjecentCells(GridCell gc, bool diagonals)
    {
        return GetAdjecentCells(gc.Location.x, gc.Location.y, diagonals);
    }

    public GridCell[] GetAdjecentCells(float x, float y, bool diagonals)
    {
        GridCell[] array;
        if (diagonals)
        {
            array = new GridCell[8];
        } else
        {
            array = new GridCell[4];
        }
         
        Vector2 loc = GetCellLocation(x, y);
        Vector2 locN = GetCellLocation(loc.x, loc.y + _cellSize);
        Vector2 locE = GetCellLocation(loc.x + _cellSize, loc.y);
        Vector2 locS = GetCellLocation(loc.x, loc.y - _cellSize);
        Vector2 locW = GetCellLocation(loc.x - _cellSize, loc.y);

        if (!diagonals)
        {
            array[0] = GetCell(locN);
            array[1] = GetCell(locE);
            array[2] = GetCell(locS);
            array[3] = GetCell(locW);
        } else
        {
            Vector2 locNE = GetCellLocation(loc.x + _cellSize, loc.y + _cellSize);
            Vector2 locSE = GetCellLocation(loc.x + _cellSize, loc.y - _cellSize);
            Vector2 locSW = GetCellLocation(loc.x - _cellSize, loc.y - _cellSize);
            Vector2 locNW = GetCellLocation(loc.x - _cellSize, loc.y + _cellSize);

            array[0] = GetCell(locN);
            array[1] = GetCell(locNE);
            array[2] = GetCell(locE);
            array[3] = GetCell(locSE);
            array[4] = GetCell(locS);
            array[5] = GetCell(locSW);
            array[6] = GetCell(locW);
            array[7] = GetCell(locNW);
        }

        return array;
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
