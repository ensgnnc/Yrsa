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

    public Transform circleOrigin;
    public float radius;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public int calculateDamage()
    {
        float power = transform.parent.GetComponent<PlayerController>().power;
        float critChange = 0.2f;

        //TODO: Create damage calculator with crit change and crit damage
        float damage = power;

        return ((int)damage);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(calculateDamage(), transform.parent.gameObject);
            }
        }
    }
}
