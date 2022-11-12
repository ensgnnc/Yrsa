using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int levelWorth = 1;

    Rigidbody2D rb;
    Animator animator;

    [SerializeField]
    private float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
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

        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            animator.SetBool("isMoving", true);
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            animator.SetBool("isMoving", false);
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = oldMovementInput * currentSpeed;

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
