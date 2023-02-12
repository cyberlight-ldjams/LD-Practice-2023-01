using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _cellSize = 1;

    private List<GridCell> _gridCells;

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
        GridCell gc;
        GridCell findGC;
        Vector2 loc = GetCellLocation(x, y);
        findGC = _gridCells.Find(gc => gc.Location.Equals(loc));
        if (findGC != null)
        {
            return findGC;
        } // else
        gc = new GridCell(x, y);
        _gridCells.Add(gc);
        return gc;
    }

    public Vector2 GetCellLocation(float x, float y)
    {
        return new Vector2(RoundToCellSize(x), RoundToCellSize(y));
    }

    private float RoundToCellSize(float x)
    {
        float multiple = Mathf.Round(x / _cellSize);

        return multiple * _cellSize;
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
