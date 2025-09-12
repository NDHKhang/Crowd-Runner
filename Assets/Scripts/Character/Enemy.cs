using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Settings")]
    [SerializeField] private float searchRadius = 1f;
    [SerializeField] private float moveSpeed = 10f;
    private State state;
    private Transform targetRunner;
    private EnemyGroup enemyGroup;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        enemyGroup = GetComponentInParent<EnemyGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                //FindTarget();
                break;
            case State.Running:
                RunToTarget();
                break;
        }
    }

    private void FindTarget()
    {
        Collider[] detectColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectColliders.Length; i++)
        {
            if (detectColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget)
                    continue;

                runner.IsTarget = true;
                targetRunner = runner.transform;

                EnterRunningState();

                return; // prevent an enemy from targeting mutiple runners
            }
        }
    }

    public void StartChasing(Transform target)
    {
        if (target.TryGetComponent(out Runner runner))
        {
            if (runner.IsTarget)
                return;

            runner.IsTarget = true;
        }
        targetRunner = target;
        EnterRunningState();
    }

    private void EnterRunningState()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunToTarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetRunner.transform.position) < 0.2f)
        {
            if(targetRunner.TryGetComponent(out Runner runner))
            {
                enemyGroup.OnEnemyDestroyed(this);
                Destroy(targetRunner.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
