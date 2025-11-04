using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle

    [Header("Events")]
    public static Action<int> onLevelCompleteRunnerCount;

    private void OnEnable()
    {
        DataManager.onRunnersLevelChanged += SpawnRunners;
        GameManager.onGameStateChanged += onGameStateChangedCallback;
    }

    private void OnDisable()
    {
        DataManager.onRunnersLevelChanged -= SpawnRunners;
        GameManager.onGameStateChanged -= onGameStateChangedCallback;
    }

    private void Start()
    {
        SpawnRunners(DataManager.instance.StartingRunnersLevel);
    }

    // Update is called once per frame
    void Update()
    {
        PlaceRunners();

        if (!GameManager.instance.IsGameState())
            return;

        if (runnerParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void onGameStateChangedCallback(GameManager.GameState state)
    {
        if (state == GameManager.GameState.LevelComplete)
            onLevelCompleteRunnerCount?.Invoke(runnerParent.childCount);
    }

    private void SpawnRunners(int amount)
    {
        int currentCount = runnerParent.childCount;
        int runnerToAdd = amount - currentCount; //only spawn the extra runners

        if(runnerToAdd > 0)
            AddRunner(runnerToAdd, false); // Spawn the runners with idle animation
    }
    
    private void PlaceRunners()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            runnerParent.GetChild(i).localPosition = GetRunnerLocalPos(i);
        }
    }

    private Vector3 GetRunnerLocalPos(int index)
    {
        // // Calculate coordinate using Fermat's spiral formula and polar-to-Cartesian conversion
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyAmount(DoorType doorType, int amount)
    {
        switch (doorType)
        {
            case DoorType.Addition:
                AddRunner(amount);
                break;
            case DoorType.Subtraction:
                RemoveRunner(amount);
                break;
            case DoorType.Multiplication:
                int characterToAdd = (runnerParent.childCount * amount) - runnerParent.childCount;
                AddRunner(characterToAdd);
                break;
        }
    }

    private void AddRunner(int amount, bool triggerAnimation = true)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(characterPrefab, runnerParent);
        }

        if(triggerAnimation)
            playerAnimator.Run();
    }

    private void RemoveRunner(int amount)
    {
        // Clamp to avoid resulting in 0 characters
        if (amount >= runnerParent.childCount)
            amount = runnerParent.childCount - 1;

        int characterAmount = runnerParent.childCount;
        for (int i = characterAmount - 1; i >= characterAmount - amount; i--)
        {
            Transform characterToDestroy = runnerParent.GetChild(i);
            characterToDestroy.SetParent(null);
            Destroy(characterToDestroy.gameObject);
        }
    }
}
