using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject settingsPanel;

    public void Show()
    {
        settingsPanel.SetActive(true);
    }

    public void Hide()
    {
        settingsPanel.SetActive(false);
    }

    public void OnClosedPressed()
    {
        UIManager.Instance.HideSettings();
    }
}
