using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float startRotationSpeed = 10f;
    [SerializeField] private float rotationSpeed = 4f;
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
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D))
        {
            _leftFootRotation = 0f;
        }
        
        if (Input.GetKeyUp(KeyCode.O) || Input.GetKeyUp(KeyCode.K))
        {
            _rightFootRotation = 0f;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _leftFootRotation -= startRotationSpeed * Time.deltaTime;
            _leftFootRotation = Mathf.Clamp(_leftFootRotation, -1, 1);
            return;
        }
        if (Input.GetKey(KeyCode.O))
        {
            _rightFootRotation += startRotationSpeed * Time.deltaTime;
            _rightFootRotation = Mathf.Clamp(_rightFootRotation, -1, 1);
            return;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _leftFootRotation += startRotationSpeed * Time.deltaTime;
            _leftFootRotation = Mathf.Clamp(_leftFootRotation, -1, 1);
            return;
        }
        if (Input.GetKey(KeyCode.K))
        {
            _rightFootRotation -= startRotationSpeed * Time.deltaTime;
            _rightFootRotation = Mathf.Clamp(_rightFootRotation, -1, 1);
            return;
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
        RotateAroundPoint(_rigidbody, LeftFoot.position, Vector3.up, _leftFootRotation * rotationSpeed);
        // Rotate the right foot
        RotateAroundPoint(_rigidbody, RightFoot.position, Vector3.up, _rightFootRotation * rotationSpeed);
    }
}
