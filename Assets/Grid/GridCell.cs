// Needed for Vector2
using UnityEngine;

public class GridCell
{
    public Vector2 Location { get; private set; }

    public GridCell(Vector2 location)
    {
        this.Location = location;
    }

    public GridCell(float x, float y)
    {
        this.Location = new Vector2(x, y);
    }
}
