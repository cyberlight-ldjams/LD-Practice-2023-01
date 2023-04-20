using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (Collider))]
public class Moveable : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField]
    public bool selected { get; private set; }

    private float selectedTime;

    //The minimum amount of time (sec) that an object
    //can be selected - so that it doesn't become
    //deselected from a single click at high FPS
    private float minSelectionTime = 0.15f;

    private Camera mainCamera;

    private float cameraDistanceZ;

    public GridCell currentCell { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cameraDistanceZ = mainCamera.WorldToScreenPoint(transform.position).z;
        selectedTime = 0;
        currentCell = null;

        if (grid == null)
        {
            GameObject gridGO = GameObject.FindGameObjectWithTag("Grid");
            grid = gridGO.GetComponent<Grid>();
        }
    }

    private void OnMouseOver()
    {
        if (!selected && Mouse.current.leftButton.wasPressedThisFrame && grid.Selected == null)
        {
            Select(true);
        }
    }

    public void Select(bool select)
    {
        selected = select;

        if (grid == null)
        {
            GameObject gridGO = GameObject.FindGameObjectWithTag("Grid");
            grid = gridGO.GetComponent<Grid>();
        }

        if (select)
        {
            grid.Selected = this.gameObject;
        } else
        {
            grid.Selected = null;
            selectedTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            selectedTime += Time.deltaTime;

            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
                cameraDistanceZ);
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            GridCell gridCell = grid.GetCell(newPosition.x, newPosition.z);

            // If there's not something in that grid cell already
            // then we can move to that grid cell
            if (!gridCell.HasGameObject())
            {
                Vector2 gridPosition = gridCell.Location;
                Vector3 finalPosition = new Vector3(gridPosition.x, 2, gridPosition.y);
                transform.position = finalPosition;

                // If it has been at least the minimum selection time and the user clicked
                // then the user wants to place this item here
                if (selectedTime >= minSelectionTime &&
                    Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Select(false);

                    // Move to the new cell;
                    if (currentCell != null)
                    {
                        currentCell.RemoveGameObject();
                    }
                    currentCell = gridCell;
                    gridCell.SetGameObject(this.gameObject);
                }
            } else
            {
                if (selectedTime >= minSelectionTime &&
                    Mouse.current.leftButton.wasPressedThisFrame)
                {
                    //TODO - Make this a proper warning message for the player
                    Debug.Log("Cannot place item on top of another item");
                }
            }
        } else // if !selected
        {
            
        }
    }
}
