using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AltarInteract : Interactable
{

    private bool isEnable;
    public GameObject statsUI;
    public GameObject player;

    public override void Interact()
    {
        if (isEnable)
        {
            player.GetComponent<PlayerController>().UnlockMovement();
            Time.timeScale = 1;
            statsUI.SetActive(false);
        }

        else { 
            player.GetComponent<PlayerController>().LockMovement();
            Time.timeScale = 0;
            statsUI.SetActive(true);
        }


        isEnable = !isEnable;
    }
}
