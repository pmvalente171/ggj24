using System;
using System.Collections;
using GameArchitecture.ScriptablePatterns;
using GameArchitecture.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerGun : MonoBehaviour
    {
        [Serializable] public class RecoilEvent : UnityEvent<float> { }
        
        [SerializeField] private AnimationCurve recoilCurve;
        [SerializeField] private Camera playerCamera;
        [Space] [SerializeField] private float recoilTime = 0.5f;
        [SerializeField] private float maxRecoil = 0.1f;
        [Space] [SerializeField] private RecoilEvent onRecoil;

        [SerializeField] private KillStreakCounter killStreakCounter;

        private bool _isRecoiling;
        public float _recoilAmmount;
        
        private IEnumerator RecoilRoutine()
        {
            float prevAmmount = 0f;
            _recoilAmmount = 0f;
            _isRecoiling = true;
            yield return AnimationUtil.LerpInTimeWindow(recoilTime, f =>
            {
                _recoilAmmount = recoilCurve.Evaluate(f) * maxRecoil;
                onRecoil.Invoke(-(_recoilAmmount - prevAmmount));
                prevAmmount = _recoilAmmount;
            });
            _isRecoiling = false;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isRecoiling)
            {
                StartCoroutine(RecoilRoutine());
                
                var ray = playerCamera.ScreenPointToRay(
                    new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

                if (Physics.Raycast(ray, out var hit))
                {   
                    var damageableComponent = hit.collider.GetComponent<IDamageable>();
                    if (damageableComponent != null)
                    {
                        damageableComponent.TakeDamage(1);
                    }
                    KillStreakCounter.increaseKillStreak();
                } else
                {
                    KillStreakCounter.resetKillStreak();
                }
            }
        }
    }
}