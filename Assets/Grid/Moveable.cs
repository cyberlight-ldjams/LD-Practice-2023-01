using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Collider))]
public class Moveable : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private Camera mainCamera;

    private float cameraDistanceZ;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cameraDistanceZ = mainCamera.WorldToScreenPoint(transform.position).z;

        if (grid == null)
        {
            GameObject gridGO = GameObject.FindGameObjectWithTag("Grid");
            grid = gridGO.GetComponent<Grid>();
        }
    }

    void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistanceZ);
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        Vector2 gridPosition = grid.GetCellLocation(newPosition.x, newPosition.z);
        Vector3 finalPosition = new Vector3(gridPosition.x, 2, gridPosition.y);

        transform.position = finalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
