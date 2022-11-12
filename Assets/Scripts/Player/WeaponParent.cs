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

    public float random;

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

    public float calculateDamage()
    {
        float power = transform.parent.GetComponent<PlayerController>().power;
        float critDamage = transform.parent.GetComponent<PlayerController>().critDamage;
        float critChange = transform.parent.GetComponent<PlayerController>().critChange;

        random = UnityEngine.Random.Range(1.0f, 100.0f);

        if(random <= critChange)
        {
            return power * critDamage;
        }
        else
        {
            return power;
        }
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Health health;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(calculateDamage(), transform.gameObject);
            }
        }
    }
}
