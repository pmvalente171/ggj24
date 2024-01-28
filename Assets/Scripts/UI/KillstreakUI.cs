using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

namespace UI
{

    public class KillStreakUI : MonoBehaviour
    {
        [SerializeField] private GameObject scoreDisplay;
        private TextMeshProUGUI scoreText;

        void Start()
        {
            if (scoreDisplay == null)
            {
                scoreDisplay = GameObject.Find("ScoreText");
            }

            if (scoreDisplay != null)
            {
                scoreText = scoreDisplay.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("ScoreText GameObject not found.");
            }
        }

        void Update()
        {
            if (scoreText != null)
            {
                scoreText.text = ScoreCounter.getScore().ToString();
            }
        }
    }
}