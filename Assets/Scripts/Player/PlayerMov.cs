using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float footRotationSpeed = 10f;
    [SerializeField] private Transform LeftFoot;
    [SerializeField] private Transform RightFoot;
    
    private Rigidbody _rigidbody;
    private float _leftFootRotation;
    private float _rightFootRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _leftFootRotation += footRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _rightFootRotation += footRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _leftFootRotation -= footRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            _rightFootRotation -= footRotationSpeed * Time.deltaTime;
        }
    }
    
    // Function to rotate rigidbody around a point
    private void RotateAroundPoint(Rigidbody rigidbody, Vector3 point, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        Vector3 pointToFoot = rigidbody.position - point;
        Vector3 rotatedPointToFoot = rotation * pointToFoot;
        rigidbody.position = point + rotatedPointToFoot;
        rigidbody.rotation *= rotation;
    }
    
    private void FixedUpdate()
    {
        // Rotate the left foot
        RotateAroundPoint(_rigidbody, LeftFoot.position, Vector3.up, _leftFootRotation);
        // Rotate the right foot
        RotateAroundPoint(_rigidbody, RightFoot.position, Vector3.up, _rightFootRotation);
    }
}
