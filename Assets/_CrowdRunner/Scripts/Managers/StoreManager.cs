using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoreManager : Singleton<StoreManager>
{
    [Header("References")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Sprite[] skins;

    private SkinButton selectedSkin;

    private const string SKIN_BUTTON_KEY = "skinButton";
    private const string SELECTED_INDEX_KEY = "selectedSkinIndex";

    [Header("Configuration")]
    [SerializeField] private int startingPrice = 250;
    [SerializeField] private int priceIncreasePerSkin = 250;

    [Header("Events")]
    public static Action<int> onPriceChanged;
    public static Action<int> onSkinSelected;

    private int currentPrice;
    public int CurrentPrice => currentPrice;
    private const string CURRENT_PRICE_KEY = "currentPrice";

    private int unlockedSkinsCount = 0;
    public int UnlockedSkinsCount => unlockedSkinsCount;

    private void OnEnable()
    {
        SkinButton.OnSkinButtonClicked += SetSelectedSkin;
    }

    private void OnDisable()
    {
        SkinButton.OnSkinButtonClicked -= SetSelectedSkin;
    }

    protected override void Awake()
    {
        base.Awake();
        currentPrice = SaveLoadManager.LoadInt(CURRENT_PRICE_KEY, startingPrice);
    }

    private void Start()
    {
        ConfigureButtons();

        // Only unlock skin 0 if it's the first time playing
        if (unlockedSkinsCount <= 0)
            UnlockSkin(0);
    }

    private void ConfigureButtons()
    {
        // Configure the store when starting game
        int selectedSkinIndex = SaveLoadManager.LoadInt(SELECTED_INDEX_KEY, 0);
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = SaveLoadManager.LoadInt(SKIN_BUTTON_KEY + i, 0) == 1;
            if(unlocked)
                unlockedSkinsCount++;

            skinButtons[i].Configure(skins[i], unlocked, i);

            // Select last skin
            if (skinButtons[i].Index == selectedSkinIndex)
                SetSelectedSkin(skinButtons[i]);
        }
    }

    private void SetSelectedSkin(SkinButton skin)
    {
        // Avoid unnecessary code if clicking same item
        if (selectedSkin == skin) return;

        if (selectedSkin != null)
            selectedSkin.UnSelectedSkin();

        selectedSkin = skin;
        selectedSkin.SelectSkin();
        onSkinSelected.Invoke(selectedSkin.Index);

        SaveLoadManager.SaveInt(SELECTED_INDEX_KEY, selectedSkin.Index);
    }

    public void UnlockSkin(int skinIndex)
    {
        SaveLoadManager.SaveInt(SKIN_BUTTON_KEY + skinIndex, 1);
        skinButtons[skinIndex].Unlocked();
        unlockedSkinsCount++;
    }

    public void OpenSkin()
    {
        List<SkinButton> skinButtonsList = new List<SkinButton>();
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked)
                skinButtonsList.Add(skinButtons[i]);
        }

        // If there's still have skin to unlock
        if (skinButtonsList.Count <= 0) return;

        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];

        UnlockSkin(randomSkinButton.Index);
        SetSelectedSkin(randomSkinButton);
        int skinPrice = currentPrice;
        currentPrice += priceIncreasePerSkin; // Calculate current price
        onPriceChanged?.Invoke(currentPrice);

        DataManager.Instance.WithDrawCoins(skinPrice);
        SaveLoadManager.SaveInt(CURRENT_PRICE_KEY, currentPrice);
    }

    public bool IsUnlockedAllSkins()
    {
        return unlockedSkinsCount >= skinButtons.Length;
    }
}
