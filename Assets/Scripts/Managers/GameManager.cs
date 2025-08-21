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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChange?.Invoke(gameState);

        Debug.Log("Game State change to" + gameState);
    }

    public bool IsGameState()
    {
        return this.gameState == GameState.Game;
    }
}
