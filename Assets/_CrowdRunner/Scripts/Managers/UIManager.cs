using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("References")]
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LevelCompleteUI levelCompleteUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private StoreUI storeUI;
    [SerializeField] private GameObject crowdCounterBubble;

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        menuUI.Show();
        gameUI.Show();
        gameOverUI.Hide();
        levelCompleteUI.Hide();
        settingsUI.Hide();
        storeUI.Hide();
        crowdCounterBubble.SetActive(false);
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
            crowdCounterBubble.SetActive(true);
        else if (gameState == GameManager.GameState.GameOver)
            ShowGameOver();
        else if (gameState == GameManager.GameState.LevelComplete)
            ShowLevelComplete();
    }

    public void StartGame()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);

        menuUI.Hide();
    }

    public void ShowGameOver()
    {
        gameUI.Hide();
        gameOverUI.Show();
    }

    public void ShowLevelComplete()
    {
        gameUI.Hide();
        levelCompleteUI.Show();
    }

    public void ShowSettings()
    {
        settingsUI.Show();
    }

    public void HideSettings()
    {
        settingsUI.Hide();
    }

    public void ShowStore()
    {
        storeUI.Show();
    }

    public void HideStore()
    {
        storeUI.Hide();
    }
}
