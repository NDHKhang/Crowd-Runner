using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject storePanel;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI rewardCoinText;
    [SerializeField] private Button openButton;

    private void OnEnable()
    {
        DataManager.onCoinsChanged += UpdateCoinsText;
        DataManager.onCoinsChanged += UpdateOpenButtonState;
        StoreManager.onPriceChanged += UpdatePriceText;
    }

    private void OnDisable()
    {
        DataManager.onCoinsChanged -= UpdateCoinsText;
        DataManager.onCoinsChanged -= UpdateOpenButtonState;
        StoreManager.onPriceChanged -= UpdatePriceText;
    }

    public void Show()
    {
        AdsManager.Instance.bannerAds.HideBannerAd();
        storePanel.SetActive(true);
        UpdatePriceText(StoreManager.Instance.CurrentPrice);
        UpdateOpenButtonState(DataManager.Instance.Coins);
        UpdateCoinsText(DataManager.Instance.Coins);
        UpdateRewardCoinText(DataManager.Instance.RewardedAdsCoin);
    }

    public void Hide()
    {
        AdsManager.Instance.bannerAds.ShowBannerAd();
        storePanel.SetActive(false);
    }

    private void UpdateCoinsText(int coins)
    {
        coinText.text = coins.ToString();
    }

    private void UpdatePriceText(int price)
    {
        priceText.text = price.ToString();
    }

    private void UpdateRewardCoinText(int rewardCoin)
    {
        rewardCoinText.text = "+" + rewardCoin.ToString();
    }

    private void UpdateOpenButtonState(int coins)
    {
        bool isEnoughCoin = coins >= StoreManager.Instance.CurrentPrice;
        bool isUnlockedAllSkins = StoreManager.Instance.IsUnlockedAllSkins();

        if (isEnoughCoin && !isUnlockedAllSkins)
            openButton.interactable = true;
        else
            openButton.interactable = false;
    }

    public void OnOpenButtonPressed()
    {
        StoreManager.Instance.OpenSkin();
    }

    public void OnBackButtonPressed()
    {
        UIManager.Instance.HideStore();
    }
}
