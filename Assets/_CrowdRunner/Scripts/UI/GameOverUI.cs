using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        UpdateCoinsText(DataManager.Instance.Coins);
        DataManager.onCoinsChanged += UpdateCoinsText;
    }

    private void OnDisable()
    {
        DataManager.onCoinsChanged -= UpdateCoinsText;
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
        GameManager.Instance.ReloadScene();
    }

    private void UpdateCoinsText(int coins)
    {
        coinText.text = coins.ToString();
    }
}
