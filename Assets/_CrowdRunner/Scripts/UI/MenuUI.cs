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
    }

    private void OnDisable()
    {
        DataManager.onRunnersLevelChanged -= UpdateRunnersLevel;
        DataManager.onIncomeLevelChanged -= UpdateIncomeLevel;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        UpdateButtonStates(DataManager.instance.Coins);
        UpdateRunnersLevel(DataManager.instance.StartingRunnersLevel);
        UpdateIncomeLevel(DataManager.instance.IncomeLevel);
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
        UIManager.instance.StartGame();
    }

    private void UpdateRunnersLevel(int level)
    {
        int upgradeCost = DataManager.instance.GetUpgradeCost(level);

        runnersLevelText.text = level.ToString() + "\nLVL";
        runnersLevelCost.text = upgradeCost.ToString();
        UpdateButtonStates(DataManager.instance.Coins);
    }

    private void UpdateIncomeLevel(int level)
    {
        int upgradeCost = DataManager.instance.GetUpgradeCost(level);

        incomeLevelText.text = level.ToString() + "\nLVL";
        incomeLevelCost.text = upgradeCost.ToString();
        UpdateButtonStates(DataManager.instance.Coins);
    }

    public void UpgradeRunnerButtonPressed()
    {
        DataManager.instance.UpgradeStartingRunners();
    }

    public void UpgradeIncomeButtonPressed()
    {
        DataManager.instance.UpgradeIncomeLevel();
    }

    public void OnSettingsButtonPressed()
    {
        UIManager.instance.ShowSettings();
    }

    private void UpdateButtonStates(int coins)
    {
        int runnersCost = DataManager.instance.GetUpgradeCost(DataManager.instance.StartingRunnersLevel);
        int incomeCost = DataManager.instance.GetUpgradeCost(DataManager.instance.IncomeLevel);

        upgradeRunnersButton.interactable = coins >= runnersCost;
        upgradeIncomeButton.interactable = coins >= incomeCost;
    }
}
