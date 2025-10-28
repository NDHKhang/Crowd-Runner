using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI[] coinsText;

    private void OnEnable()
    {
        GameManager.onGameStateChange += GameStateChangedCallback;
        DataManager.onCoinsChanged += UpdateCoinsText;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChange -= GameStateChangedCallback;
        DataManager.onCoinsChanged += UpdateCoinsText;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    private void Init()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingsPanel.SetActive(false);

        // "+1" because the index start at 0
        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);     
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
            ShowGameOver();
        else if (gameState == GameManager.GameState.LevelComplete)
            ShowLevelComplete();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    private void UpdateCoinsText(int coins)
    {
        foreach (TextMeshProUGUI coinText in coinsText)
        {
            coinText.text = coins.ToString();
        }
    }
}
