using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    SpriteRenderer sr;
    Animator animator;
    bool dead = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public Transform findPlayer()
    {
        return GameObject.Find("Player").transform;
    }

    public void destroy()
    {
        dead = true;
        moveSpeed = 0;
        moveDirection = Vector3.zero;
        Destroy(transform.gameObject);
    }

    private void Start()
    {
        target = findPlayer();
    }

    private void Update()
    {
        if(target && !dead)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target && !dead)
        {
            Vector2 scale = transform.localScale;
            if (moveDirection.x > 0) {
                scale.x = 1.2f;
            }
            else if (moveDirection.x < 0)
            {
                scale.x = -1.2f;
            }
            animator.SetBool("isMoving", true);
            transform.localScale = scale;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
}
