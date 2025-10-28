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

    private int coins;
    private int startingRunnersLevel;

    private const string COINS_KEY = "coins";

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        coins = SaveLoadManager.LoadInt(COINS_KEY, 0);
    }

    private void Start()
    {
        // Initial UI update
        onCoinsChanged?.Invoke(coins);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        onCoinsChanged?.Invoke(coins);

        SaveLoadManager.SaveInt(COINS_KEY, coins);
    }
}
