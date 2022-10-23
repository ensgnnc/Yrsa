using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AltarInteract : Interactable
{

    private bool isEnable;

    public override void Interact()
    {
        if (isEnable)
            Debug.Log("disabled");
        else
            Debug.Log("enabled");

        isEnable = !isEnable;
    }
}
