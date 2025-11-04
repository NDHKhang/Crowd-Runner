using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        DataManager.onCoinsChanged += UpdateCoinsText;
    }

    private void OnDisable()
    {
        DataManager.onCoinsChanged -= UpdateCoinsText;
    }

    void Start()
    {
        UpdateCoinsText(DataManager.instance.Coins);
    }

    public void Show()
    {
        gameOverPanel.SetActive(true);
    }

    public void Hide()
    {
        gameOverPanel.SetActive(false);
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateCoinsText(int coins)
    {
        coinText.text = coins.ToString();
    }
}
