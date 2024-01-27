using System;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float startRotationSpeed = 10f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float momentumDecayRate = 0.95f;

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
        if (Input.GetKey(KeyCode.D))
        {
            _leftFootMomentum += startRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.O))
        {
            _rightFootMomentum += startRotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.K))
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

    private void FixedUpdate()
    {
        RotateAroundPoint(_rigidbody, leftFoot.position, Vector3.up, _leftFootMomentum * rotationSpeed);
        RotateAroundPoint(_rigidbody, rightFoot.position, Vector3.up, _rightFootMomentum * rotationSpeed);
    }

    private void RotateAroundPoint(Rigidbody rigidbody, Vector3 point, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        Vector3 pointToFoot = rigidbody.position - point;
        Vector3 rotatedPointToFoot = rotation * pointToFoot;
        rigidbody.position = point + rotatedPointToFoot;
        rigidbody.rotation *= rotation;
    }
}
