using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrowdSystem : Singleton<CrowdSystem>
{
    [Header("References")]
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle
    [SerializeField] private float lerpSpeed = 10f;

    [Header("Events")]
    public static Action<int> onLevelCompleteRunnerCount;

    private HashSet<Obstacle> activeObstacles = new HashSet<Obstacle>();
    private bool shouldLerpRunners = false; // For checking place runners smooth or not

    private void OnEnable()
    {
        DataManager.onRunnersLevelChanged += SpawnRunners;
        GameManager.onGameStateChanged += OnGameStateChangedCallback;
    }

    private void OnDisable()
    {
        DataManager.onRunnersLevelChanged -= SpawnRunners;
        GameManager.onGameStateChanged -= OnGameStateChangedCallback;
    }

    private void Start()
    {
        SpawnRunners(DataManager.Instance.StartingRunnersLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Only reposition if not colliding with any obstacle
        if (activeObstacles.Count <= 0)
        {
            if (shouldLerpRunners)
                PlaceSmoothRunners();
            else
                PlaceRunners();
        }

        //if (isLerpRunners)
        //else
        //    PlaceRunners();


        if (!GameManager.Instance.IsGameState())
            return;

        if (runnerParent.childCount <= 0)
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void OnGameStateChangedCallback(GameManager.GameState state)
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

    private void PlaceSmoothRunners()
    {
        bool allRunnersInPlace = true;

        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Vector3 targetPos = GetRunnerLocalPos(i);
            Transform runner = runnerParent.GetChild(i);
            runner.localPosition = Vector3.Lerp(runner.localPosition, targetPos, Time.deltaTime * lerpSpeed);

            if (Vector3.Distance(runner.localPosition, targetPos) > 0.01f)
            {
                allRunnersInPlace = false;
            }
        }

        if(allRunnersInPlace)
            shouldLerpRunners = false;
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

    public void RegisterObstacle(Obstacle obstacle)
    {
        activeObstacles.Add(obstacle);
    }

    public void UnregisterObstacle(Obstacle obstacle)
    {
        shouldLerpRunners = true;
        activeObstacles.Remove(obstacle);
    }
}
