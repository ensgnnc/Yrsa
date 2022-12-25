using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float weight = 1;
    public int moneyWorth = 5;
    public int levelWorth = 1;

    public float attackDelay = 3f;

    public float Speed = 3f;

    public int hitPower = 5;

    private bool canHit = true;

    Rigidbody2D rb;
    
    public Vector2 MovementInput { get; set; }

    public UnityEvent animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Transform findPlayer()
    {
        return GameObject.Find("Player").transform;
    }

    public void giveDrops()
    {
        findPlayer().GetComponent<PlayerController>().level += levelWorth;
        Destroy(transform.gameObject);
    }

    public void AttackPlayer()
    {
        if (canHit) findPlayer().gameObject.GetComponent<PlayerController>().currentHealth -= hitPower;
    }

    private void FixedUpdate()
    {
        //Vector2 scale = transform.localScale;

        //if (MovementInput.magnitude > 0)
        //{
        //    animator.SetBool("isMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("isMoving", false);
        //}

        //animator.SetFloat("Horizontal", MovementInput.x);
        //animator.SetFloat("Vectoral", MovementInput.y);

        animator?.Invoke();
        rb.MovePosition(rb.position + MovementInput * Speed * Time.fixedDeltaTime);

        //if (MovementInput.x > 0)
        //{
        //    scale.x = 1.2f;
        //}
        //else if (MovementInput.x < 0)
        //{
        //    scale.x = -1.2f;
        //}
        //transform.localScale = scale;
    }
}
