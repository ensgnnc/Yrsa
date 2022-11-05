using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float currentHealth;
    public float maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    public HealthBarController HealthBarController;

    private void Start()
    {
        HealthBarController.SetHealtBar(currentHealth, maxHealth);
    }

    public void GetHit(float amount, GameObject sender)
    {
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;
        
        HealthBarController.SetHealtBar(currentHealth, maxHealth);

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
        }
    }
}
