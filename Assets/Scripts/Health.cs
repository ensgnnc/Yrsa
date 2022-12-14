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

    public GameObject waveManagerOBJ;
    public WaveManager waveManager;

    private void Start()
    {
        HealthBarController.SetHealtBar(currentHealth, maxHealth);
        waveManagerOBJ = GameObject.Find("WaveManager");
        waveManager = waveManagerOBJ.GetComponent<WaveManager>();
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
            waveManager.onEnemyDie();
            OnDeathWithReference?.Invoke(sender);
        }
    }
}
