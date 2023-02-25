// Needed for Vector2
using UnityEngine;

public class GridCell
{
    public Vector2 Location { get; private set; }

    public GameObject GO { get; private set; }

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
            if (GO.TryGetComponent<Moveable>(out Moveable m))
            {
                if (GO.GetComponent<Moveable>().currentCell != this)
                {
                    GO = null;
                    return false;
                } // else the GO is in this cell
                return true;
            } // else the GO doesn't have a moveable component, but it exists
            return true;
        } // else, GO is null
        return false;
    }

    public bool SetGameObject(GameObject go)
    {
        if (go == null)
        {
            RemoveGameObject();
            return true;
        } // else

        if (!HasGameObject())
        {
            GO = go;
            return true;
        } // else

        return false;
    }

    public void RemoveGameObject()
    {
        GO = null;
    }
}
