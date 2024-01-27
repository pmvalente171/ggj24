using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillStreakCounter: MonoBehaviour
{

    [SerializeField] private static int _killStreak = 0;

    // Returns the current kill streak
    public static int increaseKillStreak()
    {
        return ++_killStreak;
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
}
