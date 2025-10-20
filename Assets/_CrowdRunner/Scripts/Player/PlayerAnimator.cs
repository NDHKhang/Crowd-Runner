using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform charactersParent;

    public void Run()
    {
        for (int i = 0; i < charactersParent.childCount; i++)
        {
            Animator animator = charactersParent.GetChild(i).GetComponent<Animator>();

            animator.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < charactersParent.childCount; i++)
        {
            Animator animator = charactersParent.GetChild(i).GetComponent<Animator>();

            animator.Play("Idle");
        }
    }
}
