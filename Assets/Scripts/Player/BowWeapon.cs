using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowWeapon : MonoBehaviour
{

    private Animator animator;

    public float delay = 0.2f;
    private bool attackBlocked;

    public bool IsAttacking { get; private set; }

    public float random;

    public Transform bow;

    public GameObject arrowPrefab;
    public WeaponParent weaponParent;

    [Range(0, 10)]
    [SerializeField] public float BowPower;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        attackBlocked = false;
    }

    public void BowAttack()
    {
        if (attackBlocked) return;

        animator.SetTrigger("Attack");

        float angle = WeaponParent.AngleTowardsMouse(bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Arrow arrow = Instantiate(arrowPrefab, bow.position, rot).GetComponent<Arrow>();
        arrow.ArrowVelocity = BowPower;

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
