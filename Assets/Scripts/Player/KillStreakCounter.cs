using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillStreakCounter: MonoBehaviour
{

    [SerializeField] private static int _killStreak = 0;

    [SerializeField] private static float _baseStreakLossTimer = 5f;

    [SerializeField] private static float _streakTimerMultiplier = 0.9f;

    private static float _streakLossTimer = _baseStreakLossTimer;

    // Returns the current kill streak
    public static int increaseKillStreak()
    {
        _killStreak++;
        computeScoreTimer();
        return _killStreak;
        
    }

    // Returns the previous kill streak
    public static int resetKillStreak()
    {
        int prevStreak = _killStreak;
        _killStreak = 0;
        return prevStreak;
    }

    public static int getKillStreak()
    {
        return _killStreak;
    }

    public void Update() {
        if (_killStreak > 0) {
            _streakLossTimer -= Time.deltaTime;
            if (_streakLossTimer <= 0) {
                Debug.Log("Kill streak lost!");
                _killStreak--;
                computeScoreTimer();
            }
        }
    }

    private static void computeScoreTimer() {
        _streakLossTimer = _baseStreakLossTimer * Mathf.Pow(_streakTimerMultiplier, (_killStreak - 1));
        Debug.Log("Timer is now " + _streakLossTimer);
    }

}
