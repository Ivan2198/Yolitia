using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed = 100f; // Default speed value

    private void Update()
    {
        // Reset rotation to zero
        _rotation = Vector3.zero;
        _rotation += Vector3.up;    // Rotate up
        _rotation += Vector3.forward; // Also rotate forward (along Z axis)
        // Check for input keys to determine rotation direction
        //if (Input.GetKey(KeyCode.P))
        //{
        //    _rotation += Vector3.up;    // Rotate up
        //    _rotation += Vector3.forward; // Also rotate forward (along Z axis)
        //}
        //else if (Input.GetKey(KeyCode.L))
        //{
        //    _rotation += Vector3.down;   // Rotate down
        //    _rotation += Vector3.back;    // Also rotate backward (along Z axis)
        //}

        // Apply the rotation
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
}