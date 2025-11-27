using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Menu, Game, LevelComplete, GameOver }

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;

    public static GameManager Instance { get; private set; }
    // Ads
    public int gamePlayed = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }

    private void Start()
    {
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    public bool IsGameState()
    {
        return this.gameState == GameState.Game;
    }

    public void ReloadScene()
    {
        gamePlayed++;

        if (gamePlayed % 3 == 0)
        {
            AdsManager.Instance.interstitialAds.ShowAd();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
