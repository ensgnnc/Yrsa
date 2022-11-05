using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject statsUI;
    public GameObject HUD;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;

    public GameObject player;
    private int level;
    private float health;
    private float maxHealth;

    public Slider healthSlider;
    public Color low;
    public Color high;

    void Start()
    {
        statsUI.SetActive(false);
    }

    void Update()
    {
        level = player.GetComponent<PlayerController>().level;
        levelText.text = "Level: " + level.ToString();

        health = player.GetComponent<PlayerController>().currentHealth;
        maxHealth = player.GetComponent<PlayerController>().maxHealth;
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;

        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);

        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }
}
