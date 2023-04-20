using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    [SerializeField]
    public GameObject ButtonObject;

    [SerializeField]
    public GameObject HeldObject;

    private bool _isMovable;

    private Moveable _mover;

    public void Grab()
    {
        if (ButtonObject == null)
        {
            return; // Do nothing
        }

        HeldObject = Instantiate(ButtonObject);

        _mover = HeldObject.GetComponent<Moveable>();

        if (_mover == null)
        {
            _isMovable = false;
            _mover = HeldObject.AddComponent<Moveable>();
        }

        _mover.Select(true);
    }

    void Update()
    {
        // If we have an object, and that object is no longer selected
        // We need to stop holding it
        if (_mover != null && _mover.selected == false)
        {
            // If the object isn't supposed to be movable,
            // get rid of the movable component
            if (!_isMovable)
            {
                Destroy(_mover);
                Destroy(GetComponent<Collider>());
            }
            HeldObject = null;
        }
    }
}
