using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{

    public Button speedButton;
    public Button healthButton;
    public Button powerButton;

    public GameObject player;

    void Start()
    {
        Button _speedButton = speedButton.GetComponent<Button>();
        _speedButton.onClick.AddListener(speedButtonClick);

        Button _powerButton = powerButton.GetComponent<Button>();
        _powerButton.onClick.AddListener(powerButtonClick);

        Button _healthButton = healthButton.GetComponent<Button>();
        _healthButton.onClick.AddListener(healthButtonClick);
    }

    void speedButtonClick()
    {
        if (player.GetComponent<PlayerController>().level == 0)
            return;
        player.GetComponent<PlayerController>().moveSpeed = player.GetComponent<PlayerController>().moveSpeed * 1.08f;
        --player.GetComponent<PlayerController>().level;
    }

    void powerButtonClick()
    {
        if (player.GetComponent<PlayerController>().level == 0)
            return;
        player.GetComponent<PlayerController>().power = player.GetComponent<PlayerController>().power + 0.8f;
        --player.GetComponent<PlayerController>().level;
    }

    void healthButtonClick()
    {
        if (player.GetComponent<PlayerController>().level == 0)
            return;
        player.GetComponent<PlayerController>().maxHealth = player.GetComponent<PlayerController>().maxHealth + 7;
        --player.GetComponent<PlayerController>().level;
    }
}
