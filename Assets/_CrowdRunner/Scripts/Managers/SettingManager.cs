using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private VibrationManager vibrationManager;

    [SerializeField] private Sprite optionOnSprite;
    [SerializeField] private Sprite optionOffSprite;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Image vibrationButtonImage;

    private bool soundState = true;
    private bool vibrationState = true;

    private const string SOUNDS_KEY = "sound";
    private const string VIBRATION_KEY = "vibration";

    private void Awake()
    {
        // For the first time in game
        soundState = SaveLoadManager.LoadInt(SOUNDS_KEY, 1) == 1;
        vibrationState = SaveLoadManager.LoadInt(VIBRATION_KEY, 1) == 1;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (soundState)
            EnableSounds();
        else
            DisableSounds();

        if (vibrationState)
            EnableVibration();
        else
            DisableVibration();
    }

    public void ChangeSoundState()
    {
        if (soundState)
            DisableSounds();
        else
            EnableSounds();

        soundState = !soundState;

        // 0: sounds off, 1: sounds on
        SaveLoadManager.SaveInt(SOUNDS_KEY, soundState ? 1 : 0);
    }

    private void EnableSounds()
    {
        soundManager.EnableSounds();
        soundButtonImage.sprite = optionOnSprite;
    }

    private void DisableSounds()
    {
        soundManager.DisableSounds();
        soundButtonImage.sprite = optionOffSprite;
    }

    public void ChangeVibrationState()
    {
        if (vibrationState)
            DisableVibration();
        else
            EnableVibration();

        vibrationState = !vibrationState;

        // 0: sounds off, 1: sounds on
        SaveLoadManager.SaveInt(VIBRATION_KEY, vibrationState ? 1 : 0);
    }

    private void EnableVibration()
    {
        vibrationManager.EnableVibration();
        vibrationButtonImage.sprite = optionOnSprite;
    }

    private void DisableVibration()
    {
        vibrationManager.DisableVibration();
        vibrationButtonImage.sprite = optionOffSprite;
    }
}
