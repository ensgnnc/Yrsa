using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer playerRenderer, weaponRenderer;

    private Animator animator;

    public Vector2 mouseWorldPosition;

    public float delay = 0.2f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (IsAttacking)
            return;

        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        Vector2 direction = (mouseWorldPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;

        if(direction.x < 0)
        {
            scale.y = -1;
        } else if (direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;


        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
        } else {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
