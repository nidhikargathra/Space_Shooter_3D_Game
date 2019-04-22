using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void StartGameDelegate();
    public static StartGameDelegate onStartGame;
    public static StartGameDelegate onPlayerDeath;
    public static StartGameDelegate onRespawnPickup;

    public delegate void TakeDamageDelegate(float amt);
    public static TakeDamageDelegate onTakeDamage;

    public delegate void ScorePointsDelegate(int score);
    public static ScorePointsDelegate onScorePoints;

    public static void StartGame()
    {
        if (onStartGame != null)
            onStartGame();
    }

    public static void ReSpawnPickup()
    {
        if (onRespawnPickup != null)
            onRespawnPickup();
    }

    public static void TakeDamage(float percent)
    {
        //Debug.Log("take damage percent: " +percent);
        if (onTakeDamage != null)
            onTakeDamage(percent);
    }

    public static void PlayerDeath()
    {
        //Debug.Log("Player is dead");
        if (onPlayerDeath != null)
            onPlayerDeath();
    }

    public static void ScorePoints(int score)
    {
        //Debug.Log("take damage percent: " +percent);
        if (onScorePoints != null)
            onScorePoints(score);
    }
}
