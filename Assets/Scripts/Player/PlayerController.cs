using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int level = 10;
    public float currentHealth = 100;
    public float maxHealth = 100;
    public float power = 1.0f;
    public float critDamage = 10f;
    public float critChange = 7f;
    // Movement
    public float moveSpeed = 5f;
    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;
    public GameObject body;
    bool canMove = true;
    public bool success;
    public Vector2 movementInput;

    // Interact
    public GameObject interactIcon;

    // Variables
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    // Components
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private PlayerInputActions playerInputActions;
    private PlayerAnimator playerAnimator;
    private Rigidbody2D rb;

    // Game Objects
    private WeaponParent weaponParent;
    private Camera mainCamera;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    void Start()
    {
        interactIcon.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        weaponParent = GetComponentInChildren<WeaponParent>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();

    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void FixedUpdate()
    {
        weaponParent.mouseWorldPosition = GetPointerInput();

        Vector2 lookDirection = GetPointerInput() - (Vector2)transform.position;
        playerAnimator.RotateToPointer(lookDirection);

        movementInput = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;

        if (canMove) {
            if (movementInput != Vector2.zero)
            {
                success = TryMove(movementInput);

                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public void enableInteractIcon()
    {
        interactIcon.SetActive(true);
    }

    public void disableInteractIcon()
    {
        interactIcon.SetActive(false);
    }

    private void checkInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    public void GetHit(float amount, GameObject sender)
    {
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
        }
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) {
            int count = rb.Cast(
                direction,
                movementFilter, 
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset); 

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else {
                return false;
            }
        } else {
            return false;
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mouseScreenPosition = playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        mouseScreenPosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }

    private void OnFire() {
        weaponParent.Attack();
    }

    private void OnInteract()
    {
        checkInteraction();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
