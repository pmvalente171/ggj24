using System;
using System.Collections;
using System.Collections.Generic;
using GameArchitecture.Util;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float startRotationSpeed = 10f;
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
        if (Input.GetKey(KeyCode.W))
        {
            _leftFootMomentum -= startRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K))
        {
            _leftFootMomentum += startRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.O))
        {
            _rightFootMomentum += startRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rightFootMomentum -= startRotationSpeed * Time.deltaTime;
        }

        _leftFootMomentum = Mathf.Clamp(_leftFootMomentum, -1, 1);
        _rightFootMomentum = Mathf.Clamp(_rightFootMomentum, -1, 1);
    }

    private void ApplyMomentumDecay()
    {
        // Gradually reduce the momentum over time if no input is provided
        _leftFootMomentum *= momentumDecayRate;
        _rightFootMomentum *= momentumDecayRate;
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
            _leftFootMomentum + -_rightFootMomentum / 2f * maxCameraRotation), cameraRotationSpeed * Time.deltaTime);
    }
}
