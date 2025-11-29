using System;
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
    [SerializeField] private float searchRadius = 5f;
    [SerializeField] private bool hasTrigger = false;


    [Header("Spawn Settings")]
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float angle = 137.508f; // 137.508 is golden angle

    private Transform runnerParent;
    private List<Enemy> enemies = new List<Enemy>();
    private Collider[] detectColliders = new Collider[10];

    [Header("Events")]
    public static Action OnCombatStart;
    public static Action OnCombatEnd;

    // Start is called before the first frame update
    void Start()
    {
        runnerParent = GameObject.FindWithTag("Player").transform;
        GenerateEnemies();
    }

    private void Update()
    {
        if (hasTrigger) return;
        FindTarget();
    }

    // Checking if runner is in the search radius
    private void FindTarget()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, detectColliders, LayerMask.GetMask("Player"));

        if (numColliders > 0)
        {
            hasTrigger = true;
            TriggerEnemies(); // Trigger enemies to chase
            OnCombatStart?.Invoke();
            return;
        }
    }

    private void TriggerEnemies()
    {
        List<Transform> allRunners = FindAllRunners();

        for (int i = 0; i < enemies.Count && i < allRunners.Count; i++)
        {
            Enemy enemy = enemies[i];
            Transform runner = allRunners[i].transform;

            if (enemy != null)
                enemy.StartChasing(runner);
        }

        // Exception: If there are more enemies than available runners
        for (int i = allRunners.Count; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy != null)
                enemy.StartChasing(runnerParent);
        }
    }

    private List<Transform> FindAllRunners()
    {
        List<Transform> allRunners = new List<Transform>();

        for(int i = 0; i < runnerParent.childCount; i++)
        {
            allRunners.Add(runnerParent.GetChild(i));
        }

        return allRunners;
    }

    private void GenerateEnemies()
    {
        enemies.Clear();

        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPos = GetEnemyLocalPos(i);
            Vector3 enemyWorldPos = enemiesParent.TransformPoint(enemyLocalPos);

            Enemy enemy = Instantiate(enemyPrefab, enemyWorldPos, Quaternion.Euler(0, 180, 0), enemiesParent);

            enemies.Add(enemy);
        }
    }

    public void OnEnemyDestroyed(Enemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);

        if(enemies.Count == 0)
        {
            OnCombatEnd?.Invoke();
        }
    }

    private Vector3 GetEnemyLocalPos(int index)
    {
        // // Calculate coordinate using Fermat's spiral formula and polar-to-Cartesian conversion
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
