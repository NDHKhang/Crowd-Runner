using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        DataManager.onCoinsChanged += UpdateCoinsText;
    }

    private void OnDisable()
    {
        DataManager.onCoinsChanged -= UpdateCoinsText;
    }

    void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void Init()
    {
        progressBar.value = 0;
        levelText.text = "Level " + (ChunkManager.Instance.GetLevel() + 1);
        UpdateCoinsText(DataManager.Instance.Coins);
    }

    public void Show()
    {
        gamePanel.SetActive(true);
    }

    public void Hide()
    {
        gamePanel.SetActive(false);
    }

    private void UpdateCoinsText(int coins)
    {
        coinText.text = coins.ToString();
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        float progress = PlayerController.Instance.transform.position.z / ChunkManager.Instance.GetFinishZ();
        progressBar.value = progress;
    }
}
