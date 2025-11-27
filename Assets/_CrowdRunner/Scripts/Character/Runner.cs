using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    private bool isTarget = false;

    public bool IsTarget { get { return isTarget; } set { isTarget = value; } }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
