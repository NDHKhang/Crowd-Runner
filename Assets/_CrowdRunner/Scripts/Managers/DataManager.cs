using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("Event")]
    public static Action<int> onCoinsChanged;
    public static Action<int> onRunnersLevelChanged;
    public static Action<int> onIncomeLevelChanged;
    public static Action<int> onLevelIncomeCalculated;

    [Header("Upgrade Parameter")]
    [SerializeField] private int coins;
    public int Coins { get { return coins; } }
    private int startingRunnersLevel;
    public int StartingRunnersLevel { get { return startingRunnersLevel; } }
    private int incomeLevel;
    public int IncomeLevel { get { return incomeLevel; } }

    private const string COINS_KEY = "coins";
    private const string STARTING_RUNNERS_LEVEL_KEY = "runnersLevel";
    private const string INCOME_LEVEL_KEY = "incomeLevel";

    [Header("Upgrade Settings")]
    [Tooltip("Initial coin amount given to new players on first launch")]
    [SerializeField] private int startingCoins = 100;
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private int baseIncome = 30;

    [Tooltip("Income growth coefficient per level (default: +10% per level = 0.1)")]
    [SerializeField] private float incomeMultiplierPerLevel = 0.1f; // +10% per income level
    [Tooltip("Every X runners = +1x multiplier (default: 25 runners = +1x)")]
    [SerializeField] private float runnersToMultiplierRatio = 25f; // Every 25 runners = +1x
    [Tooltip("Max runner multiplier (prevents income from becoming too high")]
    [SerializeField] private float maxRunnersMultiplier = 5;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        coins = SaveLoadManager.LoadInt(COINS_KEY, startingCoins);
        startingRunnersLevel = SaveLoadManager.LoadInt(STARTING_RUNNERS_LEVEL_KEY, 1); // runner level starting at 1
        incomeLevel = SaveLoadManager.LoadInt(INCOME_LEVEL_KEY, 1); // income level starting at 1
    }

    private void OnEnable()
    {
        CrowdSystem.onLevelCompleteRunnerCount += OnRunnerCountReceived;
    }

    private void OnDisable()
    {
        CrowdSystem.onLevelCompleteRunnerCount -= OnRunnerCountReceived;
    }

    private void Start()
    {
        // Initial UI update
        onCoinsChanged?.Invoke(coins);
    }

    private void OnRunnerCountReceived(int runnerCount)
    {
        int levelCompleteIncome = CalculateLevelCompleteIncome(runnerCount);
        AddCoins(levelCompleteIncome);
        onLevelIncomeCalculated?.Invoke(levelCompleteIncome);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        onCoinsChanged?.Invoke(coins);

        SaveLoadManager.SaveInt(COINS_KEY, coins);
    }

    public void WithDrawCoins(int amount)
    {
        coins -= amount;
        onCoinsChanged?.Invoke(coins);

        SaveLoadManager.SaveInt(COINS_KEY, coins);
    }

    public int GetUpgradeCost(int level)
    {
        return baseUpgradeCost * level;
    }

    public void UpgradeStartingRunners()
    {
        int cost = GetUpgradeCost(startingRunnersLevel);

        if (coins >= cost)
        {
            WithDrawCoins(cost);
            startingRunnersLevel++;
            onRunnersLevelChanged?.Invoke(startingRunnersLevel);

            SaveLoadManager.SaveInt(STARTING_RUNNERS_LEVEL_KEY, startingRunnersLevel);
        }
    }

    public void UpgradeIncomeLevel()
    {
        int cost = GetUpgradeCost(incomeLevel);

        if (coins >= cost)
        {
            WithDrawCoins(cost);
            incomeLevel++;
            onIncomeLevelChanged?.Invoke(incomeLevel);

            SaveLoadManager.SaveInt(INCOME_LEVEL_KEY, incomeLevel);
        }
    }

    private int CalculateLevelCompleteIncome(int runnerCount)
    {
        // Level multiplier based on upgrade level (incomeLevel - 1 because level start at 1)
        float incomeLevelMultiplier = 1f + ((incomeLevel - 1) * incomeMultiplierPerLevel);
        // Runner multiplier based on runner count
        float runnerMultiplier = Mathf.Min(1f + (runnerCount / runnersToMultiplierRatio), maxRunnersMultiplier);
        int income = Mathf.RoundToInt(baseIncome * incomeLevelMultiplier * runnerMultiplier);
        return income;
    }
}
