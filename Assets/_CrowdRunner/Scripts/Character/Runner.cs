using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    private bool isTarget = false;

    [Header("Action")]
    public static Action onRunnerDead;

    public bool IsTarget { get { return isTarget; } set { isTarget = value; } }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }

    private void OnDestroy()
    {
        onRunnerDead?.Invoke();
    }
}
