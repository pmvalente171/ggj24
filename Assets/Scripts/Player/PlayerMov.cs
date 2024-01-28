using System;
using System.Collections;
using System.Collections.Generic;
using GameArchitecture.Util;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMov : MonoBehaviour
{
    [Serializable] public class FlagEvent : UnityEvent<Vector2Int> { }
    [Serializable] public class MomentumEvent : UnityEvent<Vector2> { }
    
    [SerializeField] private bool moveWhenLooking = false;
    [Space] [SerializeField] private float startRotationSpeed = 10f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float momentumDecayRate = 0.95f;
    [Space] [SerializeField] private FlagEvent onFlagChange;
    [SerializeField] private MomentumEvent onMomentumChange;
    
    [Space] [SerializeField] private float staminaChange = 0.5f / 2f; // 0.5f / 2 means it takes 2 seconds to go from 0.5 to 1 or 0.5 to 0
    [SerializeField] private float staminaRecovery = 0.5f / 4f; // 0.5f / 2 means it takes 2 seconds to go from 0.5 to 1 or 0.5 to 0

    /*
    * staminaBalance at 0 means the player can't rotate clockwise.
    * staminaBalance at 1 means the player can't rotate counterclockwise.
    */
    public float staminaBalance = 0.5f;

    [SerializeField] private KillStreakCounter killStreakCounter;

    [SerializeField] private float killStreakImpact = 0.0015f;


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
        float absRecovery = Mathf.Min(staminaRecovery, Mathf.Abs(staminaBalance - 0.5f));
        staminaBalance += (staminaBalance > 0.5f? -absRecovery : absRecovery) * Time.deltaTime;
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
            staminaBalance += staminaChange * Time.deltaTime;
            leftFootMomentum -= staminaBalance >= 1 ? 0 
            : startRotationSpeed * Time.deltaTime * 60f;
            _leftFootFlag = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            staminaBalance -= staminaChange * Time.deltaTime;
            leftFootMomentum += staminaBalance <= 0 ? 0 
            : startRotationSpeed * Time.deltaTime * 60f;
            _leftFootFlag = -1;
        }
        if (Input.GetKey(KeyCode.O))
        {
            staminaBalance -= staminaChange * Time.deltaTime;
            rightFootMomentum += staminaBalance <= 0 ? 0 
            : startRotationSpeed * Time.deltaTime * 60f;
            _rightFootFlag = 1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            staminaBalance += staminaChange * Time.deltaTime;
            rightFootMomentum -= staminaBalance >= 1 ? 0 
            : startRotationSpeed * Time.deltaTime * 60f;
            _rightFootFlag = -1;
        }

        staminaBalance = Mathf.Clamp01(staminaBalance);
        
        onFlagChange.Invoke(new Vector2Int(_leftFootFlag, _rightFootFlag));
        onMomentumChange.Invoke(new Vector2(_leftFootMomentum, _rightFootMomentum));
        
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
        // cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(0f, 0f, 
        //     (_leftFootMomentum + _rightFootMomentum) / 2f * maxCameraRotation), cameraRotationSpeed * Time.deltaTime);
    }
}
