using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerController playerController;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;

        if (playerController.movementInput != Vector2.zero)
        {
            if (playerController.movementInput.x > 0)
            {
                scale.x = 1.2f;
            }
            else if (playerController.movementInput.x < 0)
            {
                scale.x = -1.2f;
            }
        } else
        {
            if (lookDirection.x > 0)
            {
                scale.x = 1.2f;
            }
            else if (lookDirection.x < 0)
            {
                scale.x = -1.2f;
            }
        }
        transform.localScale = scale;
    }

}
