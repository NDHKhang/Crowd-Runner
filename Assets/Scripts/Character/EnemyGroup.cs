using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemiesParent;

    [Header("Settings")]
    [SerializeField] private int amount;

    [Header("Settings")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle

    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemies();
    }


    private void GenerateEnemies()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPos = GetRunnerLocalPos(i);
            Vector3 enemyWorldPos = enemiesParent.TransformPoint(enemyLocalPos);

            Instantiate(enemyPrefab, enemyWorldPos, Quaternion.Euler(0, 180, 0), enemiesParent);
        }
    }

    private Vector3 GetRunnerLocalPos(int index)
    {
        // // Calculate coordinate using Fermat's spiral formula and polar-to-Cartesian conversion
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }
}
