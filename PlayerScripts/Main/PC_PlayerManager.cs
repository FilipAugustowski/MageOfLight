using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The following script updates all of the methods that relate
 * to the player. It also holds flags that lets other scripts know 
 * what can and cannot be done to the player.
 */

public class PC_PlayerManager : MonoBehaviour
{
    public static PC_PlayerManager Instance;

    PC_InputManager inputManager;
    PC_AnimatorController animatorController;
    PC_CameraHandler cameraHandler;
    PC_MovementController movementController;
    PC_PlayerVitals playerVitals;
    public PC_SwordHolder swordHolder;

    public bool uiHidden = false;

    public bool gamePaused = false;

    #region Player Flags
    /* These flags give information on the player, originally a state machine was used,
     * but it just makes code longer to read */
    [Header("Player Flags")]
    public bool isInteracting;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool isAttacking;
    public bool isCasting;
    public bool isDashing;
    public bool isJumping;
    public bool isInCombatMode;
    public bool isAccessingSheathe;
    public bool isShrugging;
    public bool isRolling;
    public bool isDead;
    public bool isTakingDamage;
    #endregion

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }

        cameraHandler = FindObjectOfType<PC_CameraHandler>();
        movementController = GetComponent<PC_MovementController>();
    }

    void Start()
    {
        inputManager = GetComponent<PC_InputManager>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        playerVitals = GetComponent<PC_PlayerVitals>();
        swordHolder = GetComponentInChildren<PC_SwordHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animatorController.GetIsInteracting();
        animatorController.animator.SetBool("IsInAir", isInAir);
        animatorController.animator.SetBool("IsInCombatMode", isInCombatMode);
        isDashing = animatorController.animator.GetBool("Dashing");
        isJumping = animatorController.animator.GetBool("Jumping");
        isCasting = animatorController.animator.GetBool("Casting");
        isAccessingSheathe = animatorController.animator.GetBool("IsAccessingSheathe");
        isShrugging = animatorController.animator.GetBool("IsShrugging");
        isRolling = animatorController.animator.GetBool("IsRolling");
        isDead = animatorController.animator.GetBool("IsDead");
        isTakingDamage = animatorController.animator.GetBool("IsTakingDamage");

        if (isDead)
        {
            movementController.fallingSpeed = 400;
            movementController.canMove = false;
        }
        else
        {
            movementController.canMove = true;
        }

        inputManager.TickInput(delta);

        if (movementController.canMove)
        {
            movementController.HandleRollingAndSpring(delta);

            movementController.HandleJumping();
        }

        playerVitals.RegenStats();
        playerVitals.HandleKnockBackTimer();
    }

    /* Function calls included here pertain to rigidbody modification */
    private void FixedUpdate()
    {
        float delta = Time.deltaTime;

        if (movementController.canMove)
        {
            movementController.HandleMovement(delta);
        }


        if (animatorController.canRotate)
        {
            movementController.HandleRotation(delta);
        }

        movementController.HandleFalling(delta, movementController.moveDirection);

    }


    private void LateUpdate()
    {
        float delta = Time.fixedDeltaTime;


        inputManager.rollFlag = false;
        inputManager.jumpInput = false;
        inputManager.airDashInput = false;
        inputManager.combatModeInput = false;
        inputManager.switchSpellInput = false;
        inputManager.interactInput = false;
        inputManager.pauseInput = false;
        inputManager.hideUIInput = false;

        #region Console Command Bools
        inputManager.tp1 = false;
        inputManager.tp2 = false;
        inputManager.tp3 = false;
        inputManager.lvl1 = false;
        inputManager.lvl2 = false;
        inputManager.lvl3 = false;
        inputManager.lvl4 = false;

        #endregion
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputManager.mouseX, inputManager.mouseY);
        }

        if (isInAir)
        {
            movementController.inAirTimer += delta;
        }
    }
}
