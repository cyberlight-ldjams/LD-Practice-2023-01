using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Collider))]
public class Moveable : MonoBehaviour
{
    private Camera mainCamera;

    private float cameraDistanceZ;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cameraDistanceZ = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistanceZ);
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
