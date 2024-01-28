using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        public Sprite [] healthSprites;
        public Image healthImage;
        
        private PlayerHealth _playerHealth;
        
        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
        }
        
        private void Update()
        {
            if (_playerHealth.currentHealth <= 0) return;
            healthImage.sprite = healthSprites[_playerHealth.currentHealth - 1];
        }
    }
}