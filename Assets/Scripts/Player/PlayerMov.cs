using System;
using System.Collections;
using System.Collections.Generic;
using GameArchitecture.Util;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private bool moveWhenLooking = false; 
    [Space] [SerializeField] private float startRotationSpeed = 10f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float momentumDecayRate = 0.95f;
    [Space] [SerializeField] private Camera cam;
    [SerializeField] private float maxCameraRotation = 10f; // tilt the camera along the Z axis
    [SerializeField] private float cameraRotationSpeed = 10f;
    
    private Rigidbody _rigidbody;
    private float _leftFootMomentum;
    private float _rightFootMomentum;

    public int _leftFootFlag = 0;
    public int _rightFootFlag = 0;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
        ApplyMomentumDecay();
    }

    private void HandleInput()
    {
        float leftFootMomentum = _leftFootMomentum;
        float rightFootMomentum = _rightFootMomentum;
        
        _leftFootFlag = 0;
        _rightFootFlag = 0;
        
        if (Input.GetKey(KeyCode.W))
        {
            leftFootMomentum -= startRotationSpeed;
            _leftFootFlag = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            leftFootMomentum += startRotationSpeed;
            _leftFootFlag = -1;
        }
        if (Input.GetKey(KeyCode.O))
        {
            rightFootMomentum += startRotationSpeed;
            _rightFootFlag = 1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            rightFootMomentum -= startRotationSpeed;
            _rightFootFlag = -1;
        }
        
        print($"Left foot flag: {_leftFootFlag}, Right foot flag: {_rightFootFlag}");
        
        if (!moveWhenLooking && 
            ((_rightFootFlag == -1 && _leftFootFlag == -1) || 
             (_rightFootFlag == 1 && _leftFootFlag == 1))) return;
        
        _leftFootMomentum = Mathf.Clamp(leftFootMomentum, -1, 1);
        _rightFootMomentum = Mathf.Clamp(rightFootMomentum, -1, 1);
    }

    private void ApplyMomentumDecay()
    {
        // Gradually reduce the momentum over time if no input is provided
        _leftFootMomentum -= momentumDecayRate * _leftFootMomentum * Time.deltaTime * 5f;
        _rightFootMomentum -= momentumDecayRate * _rightFootMomentum * Time.deltaTime * 5f;
    }


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
        RotateAroundPoint(_rigidbody, leftFoot.position, Vector3.up, _leftFootMomentum * rotationSpeed);
        RotateAroundPoint(_rigidbody, rightFoot.position, Vector3.up, _rightFootMomentum * rotationSpeed);
    }

    private void LateUpdate()
    {
        // Rotate the camera
        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(0f, 0f, 
            (_leftFootMomentum + _rightFootMomentum) / 2f * maxCameraRotation), cameraRotationSpeed * Time.deltaTime);
    }
}
