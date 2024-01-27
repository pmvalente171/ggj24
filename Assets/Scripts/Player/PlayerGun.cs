using System;
using System.Collections;
using GameArchitecture.ScriptablePatterns;
using GameArchitecture.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerGun : MonoBehaviour
    {
        [Serializable] public class RecoilEvent : UnityEvent<float> { }
        
        [SerializeField] private Camera playerCamera;
        [Space] [SerializeField] private float recoilTime = 0.5f;
        [SerializeField] private float recoilAmount = 0.1f;
        [Space] [SerializeField] private RecoilEvent onRecoil;

        private bool _isRecoiling;
        public float _recoilTimer;
        
        private IEnumerator RecoilRoutine()
        {
            _recoilTimer = 0f;
            yield return AnimationUtil.LerpInTimeWindow(recoilTime, f =>
            {
                _recoilTimer = Mathf.Sin(f * Mathf.PI / 2) * recoilAmount;
                onRecoil.Invoke(_recoilTimer);
            });
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && !_isRecoiling)
            {
                StartCoroutine(RecoilRoutine());
                
                // Cast a ray from the center of the screen
                var ray = playerCamera.ScreenPointToRay(
                    new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
                
                // If the ray hits something
                if (Physics.Raycast(ray, out var hit))
                {
                    // If the ray hits an enemy
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        // TODO: Damage the enemy
                    }
                }
            }
        }
    }
}