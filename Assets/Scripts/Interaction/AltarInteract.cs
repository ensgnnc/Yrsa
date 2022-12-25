using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AltarInteract : Interactable
{

    private bool isFirstTime = true;
    public QuestManager questManager;

    private bool isEnable;
    public GameObject statsUI;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public override void Interact()
    {
        if (isFirstTime && questManager.questNumber == 2) questManager.nextQuest();
        if (isEnable)
        {
            player.GetComponent<PlayerController>().UnlockMovement();
            Time.timeScale = 1;
            statsUI.SetActive(false);
        }

        else
        {
            player.GetComponent<PlayerController>().LockMovement();
            Time.timeScale = 0;
            statsUI.SetActive(true);
        }


        isEnable = !isEnable;
    }
}
