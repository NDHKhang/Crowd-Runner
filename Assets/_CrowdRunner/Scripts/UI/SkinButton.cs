using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button thisSkinButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selectorImage;

    [Header("Events")]
    public static Action<SkinButton> OnSkinButtonClicked;

    private bool isUnlocked;
    public bool IsUnlocked => isUnlocked;

    private int index;
    public int Index => index;

    public void Configure(Sprite skinSprite, bool isUnlocked, int index)
    {
        skinImage.sprite = skinSprite;
        this.isUnlocked = isUnlocked;
        this.index = index;

        if (isUnlocked)
            Unlocked();
        else
            Locked();

    }

    public void Unlocked()
    {
        thisSkinButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        isUnlocked = true;
    }

    public void Locked()
    {
        thisSkinButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void OnButtonClicked()
    {
        OnSkinButtonClicked?.Invoke(this);
    }

    public void SelectSkin()
    {
        selectorImage.SetActive(true);
    }

    public void UnSelectedSkin()
    {
        selectorImage.SetActive(false);
    }
}
