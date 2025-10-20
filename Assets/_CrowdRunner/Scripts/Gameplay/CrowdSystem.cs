using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform charactersParent;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle

    // Update is called once per frame
    void Update()
    {
        PlaceRunners();

        if (!GameManager.instance.IsGameState())
            return;

        if (charactersParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < charactersParent.childCount; i++)
        {
            charactersParent.GetChild(i).localPosition = GetRunnerLocalPos(i);
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
        return radius * Mathf.Sqrt(charactersParent.childCount);
    }

    public void ApplyAmount(DoorType doorType, int amount)
    {
        switch (doorType)
        {
            case DoorType.Addition:
                AddCharacter(amount);
                break;
            case DoorType.Subtraction:
                RemoverRunner(amount);
                break;
            case DoorType.Multiplication:
                int characterToAdd = (charactersParent.childCount * amount) - charactersParent.childCount;
                AddCharacter(characterToAdd);
                break;
        }
    }

    private void AddCharacter(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(characterPrefab, charactersParent);
        }

        playerAnimator.Run();
    }

    private void RemoverRunner(int amount)
    {
        // Clamp to avoid resulting in 0 characters
        if (amount >= charactersParent.childCount)
            amount = charactersParent.childCount - 1;

        int characterAmount = charactersParent.childCount;
        for (int i = characterAmount - 1; i >= characterAmount - amount; i--)
        {
            Transform characterToDestroy = charactersParent.GetChild(i);
            characterToDestroy.SetParent(null);
            Destroy(characterToDestroy.gameObject);
        }
    }
}
