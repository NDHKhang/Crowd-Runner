using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelCompletePanel;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI incomeLevelCompleteText;

    private void OnEnable()
    {
        DataManager.onCoinsChanged += UpdateCoinsText;
        DataManager.onLevelIncomeCalculated += UpdateLevelCompleteIncome;
    }

    private void OnDisable()
    {
        DataManager.onCoinsChanged -= UpdateCoinsText;
        DataManager.onLevelIncomeCalculated -= UpdateLevelCompleteIncome;
    }

    public void Show()
    {
        levelCompletePanel.SetActive(true);
    }

    public void Hide()
    {
        levelCompletePanel.SetActive(false);
    }

    public void OnNextButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateCoinsText(int coins)
    {
        coinText.text = coins.ToString();
    }

    private void UpdateLevelCompleteIncome(int income)
    {
        incomeLevelCompleteText.text = "+" + income.ToString();
    }
}
