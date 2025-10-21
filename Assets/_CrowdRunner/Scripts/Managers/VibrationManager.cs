using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    private bool isVibrationOn = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCollision.onDoorHit += Vibrate;
        Enemy.onEnemyDead += Vibrate;

        GameManager.onGameStateChange += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        PlayerCollision.onDoorHit -= Vibrate;
        Enemy.onEnemyDead -= Vibrate;

        GameManager.onGameStateChange -= GameStateChangedCallback;
    }

    private void Vibrate()
    {
        if (isVibrationOn)
        {
            Debug.Log("Vibrate");
            HapticFeedback.LightFeedback();
        }
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.LevelComplete)
            Vibrate();
        else if (state  == GameManager.GameState.GameOver)
            Vibrate();
    }

    public void EnableVibration()
    {
        isVibrationOn = true;
    }

    public void DisableVibration()
    {
        isVibrationOn = false;
    }
}
