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
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI healthText;

    public Slider healthSlider;

    public Slider manaSlider;

    private Transform player;
    private int level;

    private float health;
    private float maxHealth;

    private float mana;
    private float maxMana;

    void Start()
    {
        player = findPlayer();
        statsUI.SetActive(false);
    }

    private Transform findPlayer()
    {
        return GameObject.Find("Player").transform;
    }

    void Update()
    {
        level = player.GetComponent<PlayerController>().level;
        levelText.text = level.ToString();

        health = player.GetComponent<PlayerController>().currentHealth;
        maxHealth = player.GetComponent<PlayerController>().maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        mana = player.GetComponent<PlayerController>().currentMana;
        maxMana = player.GetComponent<PlayerController>().maxMana;
        manaText.text = mana.ToString() + " / " + maxMana.ToString();

        manaSlider.value = mana;
        manaSlider.maxValue = maxMana;

        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;
    }
}
