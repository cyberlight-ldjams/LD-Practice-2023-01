using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float _cellSize = 5;

    public GridCell GetCell(float x, float y)
    {

        return new GridCell(new Vector2(0, 0));
    }

    public Vector2 GetCellCenter(float x, float y)
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
