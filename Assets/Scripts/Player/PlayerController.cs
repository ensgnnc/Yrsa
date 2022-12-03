using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [Header("Player Stats")]
    public int level = 0;
    public float currentHealth = 100;
    public float maxHealth = 100;
    public float mana = 100;
    public float maxMana = 100;
    public float power = 1.0f;
    public float critDamage = 10f;
    public float critChange = 7f;
    public float dashDelay = 5f;
    public float dashSpeed = 15.0f;
    public float dashManaCost = 20;
    public float moveSpeed = 5f;

    // Movement
    [Header("Movement")]
    public float collisionOffset = 0.01f;
    public GameObject body;
    public ContactFilter2D movementFilter;
    bool canMove = true;
    bool success;
    public Vector2 movementInput;
    bool canDash = true;
    float startDashTime = 0.2f;
    float currentDashTime;

    // Interact
    [Header("Interaction")]
    public GameObject interactIcon;

    // Variables
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector2 boxSize = new Vector2(0.1f, 1f);
    public float speedLevel;

    // Components
    [Header("Components")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Animator interactionAnimator;
    private PlayerInputActions playerInputActions;
    private PlayerAnimator playerAnimator;
    private Rigidbody2D rb;

    // Game Objects
    private WeaponParent weaponParent;
    private Camera mainCamera;

    // Events
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
        if (currentHealth <= 0)
        {
            print("died");
            // TODO: stop the game when player die than calculate players score and ask for main menu or restart
        }

        weaponParent.mouseWorldPosition = GetPointerInput();

        Vector2 lookDirection = GetPointerInput() - (Vector2)transform.position;
        playerAnimator.RotateToPointer(lookDirection);

        movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();

        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
        animator.SetFloat("Speed", movementInput.magnitude);

        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
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
                    interactionAnimator.SetTrigger("Interact");
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
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public IEnumerator Dash()
    {
        if (canDash && mana >= dashManaCost)
        {
            canDash = false;
            canMove = false;

            mana -= dashManaCost;

            currentDashTime = startDashTime;

            while (currentDashTime > 0f)
            {
                currentDashTime -= Time.deltaTime;
                rb.velocity = movementInput.normalized * dashSpeed;
                yield return null;
            }

            rb.velocity = new Vector2(0f, 0f);

            canMove = true;
            StartCoroutine(DashDelay());
        }

    }


    private IEnumerator DashDelay()
    {

        yield return new WaitForSeconds(dashDelay);
        canDash = true;

    }

    private Vector2 GetPointerInput()
    {
        Vector3 mouseScreenPosition = playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        mouseScreenPosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }

    private void OnFire()
    {
        weaponParent.Attack();
    }

    private void OnInteract()
    {
        checkInteraction();
    }

    private void OnDash()
    {
        StartCoroutine(Dash());
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
