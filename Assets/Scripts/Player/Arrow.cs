using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [HideInInspector] public float ArrowDamage;
    [SerializeField] Rigidbody2D rb;

    GameObject player;
    PlayerController playerController;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        Destroy(gameObject, 15f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().GetHit(playerController.power, transform.gameObject);
        }
        Destroy(gameObject);
    }
}
