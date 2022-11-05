using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float power = 7, delay = 0.15f;

    public UnityEvent OnStart, OnEnd;

    public void DoKnockback(GameObject sender)
    {
        StopAllCoroutines();
        OnStart?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * power, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnEnd?.Invoke();
    }
}
