using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject statsUI;
    public GameObject HUD;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;

    public GameObject player;
    private int level;
    private int health;
   void Start()
    {
        statsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        level = player.GetComponent<PlayerController>().level;
        levelText.text = "Level: " + level.ToString();

        health = player.GetComponent<PlayerController>().health;
        healthText.text = "Health: " + health.ToString();
    }
}
