using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrAnimator : MonoBehaviour
{
    public Enemy enemy;
    Animator animator;
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimator()
    {
        if (enemy.MovementInput.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        animator.SetFloat("Horizontal", enemy.MovementInput.x);
    }
}