using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState { Menu, Game, LevelComplete, GameOver }

    private GameState gameState;

    public static Action<GameState> onGameStateChange;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChange?.Invoke(gameState);
    }

    public bool IsGameState()
    {
        return this.gameState == GameState.Game;
    }
}
