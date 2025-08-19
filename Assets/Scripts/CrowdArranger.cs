using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdArranger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform charactersParent;

    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle

    // Update is called once per frame
    void Update()
    {
        PlaceCharacters();
    }

    private void PlaceCharacters()
    {
        for (int i = 0; i < charactersParent.childCount; i++)
        {
            charactersParent.GetChild(i).localPosition = GetCharacterLocalPos(i);
        }
    }

    private Vector3 GetCharacterLocalPos(int index)
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
}
