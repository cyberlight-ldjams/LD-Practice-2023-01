// Needed for Vector2
using UnityEngine;

public class GridCell
{
    public Vector2 Location { get; private set; }

    public GameObject GO { get; set; }

    public GridCell(Vector2 location) : this(location.x, location.y) { }

    public GridCell(float x, float y)
    {
        this.Location = new Vector2(x, y);
        GO = null;
    }

    public bool HasGameObject()
    {
        if (GO != null)
        {
            if (GO.GetComponent<Moveable>() != null)
            {
                if (GO.GetComponent<Moveable>().currentCell != this)
                {
                    GO = null;
                    return false;
                }
            }
            return true;
        } // else
        return false;
    }
}
