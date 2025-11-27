using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button upgradeRunnersButton;
    [SerializeField] private Button upgradeIncomeButton;
    [SerializeField] private TextMeshProUGUI runnersLevelText;
    [SerializeField] private TextMeshProUGUI runnersLevelCost;
    [SerializeField] private TextMeshProUGUI incomeLevelText;
    [SerializeField] private TextMeshProUGUI incomeLevelCost;

    private void OnEnable()
    {
        DataManager.onRunnersLevelChanged += UpdateRunnersLevel;
        DataManager.onIncomeLevelChanged += UpdateIncomeLevel;
        DataManager.onCoinsChanged += UpdateButtonStates;
    }

    private void OnDisable()
    {
        DataManager.onRunnersLevelChanged -= UpdateRunnersLevel;
        DataManager.onIncomeLevelChanged -= UpdateIncomeLevel;
        DataManager.onCoinsChanged -= UpdateButtonStates;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        UpdateButtonStates(DataManager.Instance.Coins);
        UpdateRunnersLevel(DataManager.Instance.StartingRunnersLevel);
        UpdateIncomeLevel(DataManager.Instance.IncomeLevel);
    }

    public void Show()
    {
        menuPanel.SetActive(true);
    }

    public void Hide()
    {
        menuPanel.SetActive(false);
    }

    public void OnPlayButtonPressed()
    {
        UIManager.Instance.StartGame();
    }

    private void UpdateRunnersLevel(int level)
    {
        int upgradeCost = DataManager.Instance.GetUpgradeCost(level);

        runnersLevelText.text = level.ToString() + "\nLVL";
        runnersLevelCost.text = upgradeCost.ToString();
        UpdateButtonStates(DataManager.Instance.Coins);
    }

    private void UpdateIncomeLevel(int level)
    {
        int upgradeCost = DataManager.Instance.GetUpgradeCost(level);

        incomeLevelText.text = level.ToString() + "\nLVL";
        incomeLevelCost.text = upgradeCost.ToString();
        UpdateButtonStates(DataManager.Instance.Coins);
    }

    public void UpgradeRunnerButtonPressed()
    {
        DataManager.Instance.UpgradeStartingRunners();
    }

    public void UpgradeIncomeButtonPressed()
    {
        DataManager.Instance.UpgradeIncomeLevel();
    }

    public void OnSettingsButtonPressed()
    {
        UIManager.Instance.ShowSettings();
    }

    private void UpdateButtonStates(int coins)
    {
        int runnersCost = DataManager.Instance.GetUpgradeCost(DataManager.Instance.StartingRunnersLevel);
        int incomeCost = DataManager.Instance.GetUpgradeCost(DataManager.Instance.IncomeLevel);

        upgradeRunnersButton.interactable = coins >= runnersCost;
        upgradeIncomeButton.interactable = coins >= incomeCost;
    }

    public void OnStoreButtonPressed()
    {
        UIManager.Instance.ShowStore();
    }
}
