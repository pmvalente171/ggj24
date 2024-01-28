using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour 
{

    
    [SerializeField] private static int _score = 0;

    public static int addScore(int amount)
    {
        return _score += amount;
    }

    public static int getScore()
    {
        return _score;
    }


}
