using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSkinSelector runnerSelectorPrefab;

    private void OnEnable()
    {
        StoreManager.onSkinSelected += SelectSkin;
    }

    private void OnDisable()
    {
        StoreManager.onSkinSelected -= SelectSkin;
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnersParent.childCount; i++)
            runnersParent.GetChild(i).GetComponent<RunnerSkinSelector>().SelectRunnerSkin(skinIndex);

        // Change skin for prefab
        runnerSelectorPrefab.SelectRunnerSkin(skinIndex);
    }
}
