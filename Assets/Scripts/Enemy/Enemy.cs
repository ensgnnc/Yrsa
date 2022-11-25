using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int levelWorth = 1;

    public float Speed = 3f;

    Rigidbody2D rb;
    Animator animator;
    public Vector2 MovementInput { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public Transform findPlayer()
    {
        return GameObject.Find("Player").transform;
    }

    public void giveDrops() {
        findPlayer().GetComponent<PlayerController>().level += levelWorth;
        Destroy(transform.gameObject);
    }

    public void AttackPlayer()
    {
        --findPlayer().gameObject.GetComponent<PlayerController>().currentHealth;
    }

    private void FixedUpdate()
    {
        Vector2 scale = transform.localScale;

        if (MovementInput.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        rb.MovePosition(rb.position + MovementInput * Speed * Time.fixedDeltaTime);

        if (MovementInput.x > 0) {
            scale.x = 1.2f;
        }
        else if (MovementInput.x < 0)
        {
            scale.x = -1.2f;
        }
        transform.localScale = scale;
    }
}
