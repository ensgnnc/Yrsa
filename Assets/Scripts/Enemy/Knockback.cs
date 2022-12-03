using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public Transform player;
    public Enemy enemy;

    [SerializeField]
    private float power;
    private float delay = 0.15f;
    private float weight;
    private Vector2 knokbackForce;

    private PlayerController playerController;

    public UnityEvent OnStart, OnEnd;

    private void Start()
    {
        player = findPlayer();

        weight = enemy.weight;
        playerController = player.GetComponent<PlayerController>();
    }

    public Transform findPlayer()
    {
        return GameObject.Find("Player").transform;
    }

    public void DoKnockback()
    {
        StopAllCoroutines();
        OnStart?.Invoke();
        Vector2 direction = (transform.position - findPlayer().position).normalized;
        if ((direction * playerController.power) / ((weight * 0.3f)) == Vector2.zero)
        {
            knokbackForce = direction;
        }
        else
        {
            knokbackForce = (direction * playerController.power) / ((weight * 0.3f));
        }
        rb.AddForce(knokbackForce, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnEnd?.Invoke();
    }
}
