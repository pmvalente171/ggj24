using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [Space] [SerializeField] private float lookAcc = 1.3f;
        [SerializeField] private float lookSpeed = 3f;
        [SerializeField] private float lookDecayRate = 0.93f;
        [Space] [SerializeField] private float maxCameraRotationZ = 10f;
        [SerializeField] private float maxCameraRotationX = 1f;
         
        private float _recoilModifier;
        private float _cameraRotationX; // -1 to 1
        
        private float _leftFootMomentum;
        private float _rightFootMomentum;
        
        public void SetFlag(Vector2Int flag)
        {
            if (flag.x == 0 && flag.y == 0) return;

            if (flag.x == 1 && flag.y == 1)
            {
                _cameraRotationX -= lookAcc * Time.deltaTime;
            }
            
            if (flag.x == -1 && flag.y == -1)
            {
                _cameraRotationX += lookAcc * Time.deltaTime;
            }

            // _rotationMomentum -= _rotationMomentum * lookDecayRate * Time.deltaTime * 10f; 
            _cameraRotationX = Mathf.Clamp(_cameraRotationX, -1, 1);
        }

        public void AddRecoil(float amount)
        {
            _cameraRotationX += amount;
            _cameraRotationX = Mathf.Clamp(_cameraRotationX, -1, 1);
        }
        
        public void SetMomentum(Vector2 momentum)
        {
            _leftFootMomentum = momentum.x;
            _rightFootMomentum = momentum.y;
        }
        
        // Add to the camera rotation on the X axis
        private void AddRotation(float amount)
        {
            var localRotation = cameraTransform.localRotation;
            
            // Clamp the rotation within the min and max rotation angles
            localRotation = Quaternion.Lerp(localRotation, Quaternion.Euler(amount * maxCameraRotationX, 0f,
                (_leftFootMomentum + _rightFootMomentum) / 2f * maxCameraRotationZ), lookDecayRate);
            cameraTransform.localRotation = localRotation;
        }
        
        private void LateUpdate()
        {
            AddRotation(_cameraRotationX * lookSpeed);
        }
    }
}