using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    private Animator animator;

    public float delay = 0.2f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }

    public float random;
    public WeaponParent weaponParent;

    public Transform circleOrigin;
    public float radius;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attackBlocked = false;
    }

    public void SwordAttack()
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
        float power = transform.parent.parent.GetComponent<PlayerController>().power;
        float critDamage = transform.parent.parent.GetComponent<PlayerController>().critDamage;
        float critChange = transform.parent.parent.GetComponent<PlayerController>().critChange;

        random = UnityEngine.Random.Range(1.0f, 100.0f);

        if (random <= critChange)
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
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(calculateDamage(), transform.gameObject);
            }
        }
    }
}
