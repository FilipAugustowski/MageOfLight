using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
/* 
 * A script that takes in the players input and sends it to various scripts 
 * This Script also calls the tick functions of a lot of the player's logic. Perhaps could move that part to a player manager script */


public class PC_InputManager : MonoBehaviour
{
    PC_MovementController movementController;
    PC_MeleeHandler meleeHandler;
    PC_PlayerManager playerManager;
    PC_SpellHandler spellHandler;
    PC_PlayerVitals playerVitals;
    PC_CameraHandler cameraHandler;
    PC_AnimatorController animatorController;
    PC_Controls controls;

    public bool allowAbilityInput = true;


    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool rollSprintInput;
    public bool attackInput;
    public bool jumpInput;
    public bool castInput;
    public bool airDashInput;
    public bool combatModeInput;
    public bool interactInput;
    public bool switchSpellInput;
    public bool pauseInput;
    public bool hideUIInput;

    #region Console Command Bools
    public bool tp1;
    public bool tp2;
    public bool tp3;
    public bool lvl1;
    public bool lvl2;
    public bool lvl3;
    public bool lvl4;
    #endregion
    
    float rollInputTimer; 
    public bool rollFlag;
    public bool sprintFlag;

    bool restart = false;

    public bool uiHidden;

    PC_PlayerControls inputActions;


    Vector2 movementInput;
    Vector2 cameraInput;



    //uiManager.HideNonNarrativeUI(); add hide UI input 
 
    void Awake()
    {
        /* Setup Cursor */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerVitals = GetComponent<PC_PlayerVitals>();
        meleeHandler = GetComponent<PC_MeleeHandler>();
        spellHandler = GetComponent<PC_SpellHandler>();
        movementController = GetComponent<PC_MovementController>();
        playerManager = GetComponent<PC_PlayerManager>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        cameraHandler = FindObjectOfType<PC_CameraHandler>();

        uiHidden = false;
    }

    public void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PC_PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.Jump.performed += i => jumpInput = true;
            inputActions.PlayerActions.AirDash.performed += i => airDashInput = true;
            inputActions.PlayerActions.CombatMode.performed += i => combatModeInput = true;
            inputActions.PlayerActions.Restart.performed += i => restart = true;
            inputActions.PlayerActions.Interact.performed += i => interactInput = true;
            inputActions.PlayerActions.SwitchSpell.performed += i => switchSpellInput = true;
            inputActions.PlayerActions.Pause.performed += i => pauseInput = true;
            inputActions.PlayerActions.HideUI.performed += i => hideUIInput = true;

            #region Console Commands
            //inputActions.PlayerActions.TP1.performed += i => tp1 = true;
            //inputActions.PlayerActions.TP2.performed += i => tp2 = true;
            //inputActions.PlayerActions.TP3.performed += i => tp3 = true;

            //inputActions.PlayerActions.Lvl1.performed += i => lvl1 = true;
            //inputActions.PlayerActions.Lvl2.performed += i => lvl2 = true;
            //inputActions.PlayerActions.Lvl3.performed += i => lvl3 = true;
            //inputActions.PlayerActions.Lvl4.performed += i => lvl4 = true;

            #endregion

        }

        inputActions.Enable();

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float _delta)
    {
        HandleMoveInput(_delta);
        HandleRollSprintInput(_delta);
        HandleAttackInput(_delta);
        HandleCastingInput(_delta);
        HandleCombatModeInput();
        HandleSwapSpellInput();
        HandleHideUIInput();

        restart = inputActions.PlayerActions.Restart.phase == UnityEngine.InputSystem.InputActionPhase.Started;

        if(restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void HandleMoveInput(float _delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollSprintInput(float _delta)
    {
        rollSprintInput = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

        sprintFlag = rollSprintInput;

        if (rollSprintInput)
        {
            rollInputTimer += _delta;
        }
        else
        {
            /* If the player holds the roll/sprint btn less than x amount of time roll instead of sprint. 
             * Otherwise start sprinting */
            if (rollInputTimer > 0 && rollInputTimer < 0.5f) 
            {
                sprintFlag = false;
                rollFlag = true;
            }

            rollInputTimer = 0;
        }


    }

    private void HandleAttackInput(float _delta)
    {
        attackInput = inputActions.PlayerActions.MeleeAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        meleeHandler.HandleAttack(attackInput, _delta);
    }

    private void HandleCastingInput(float _delta)
    {
        castInput = inputActions.PlayerActions.Casting.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        spellHandler.HandleCasting(castInput, _delta);
    }

    public void HandleCombatModeInput()
    {
        if (combatModeInput)
        {
            if(!playerManager.isInteracting 
                || playerManager.isJumping)
            {
                if (!playerManager.isInCombatMode)
                {
                    animatorController.PlayTargetAnimation("PC_DrawSword", true, 2);
                    AudioManager.Instance.Play("PC_Sheathe");
                    playerManager.isInCombatMode = true;
                }
                else
                {
                    AudioManager.Instance.Play("PC_Sheathe");

                    animatorController.PlayTargetAnimation("PC_SheatheSword", true, 2);
                    playerManager.isInCombatMode = false;
                }
            }
        }

        cameraHandler.SetCameraHeight();
    }

    public void HandleHideUIInput()
    {
        if(hideUIInput)
        {
            if(!playerManager.uiHidden)
            {
                playerManager.uiHidden = true;
                PC_UIManager.Instance.HideNonNarrativeUI();
            }
            else
            {
                playerManager.uiHidden = false;
                PC_UIManager.Instance.ReenableNonNarrativeUI();
            }
        }
    }

    public void HandleSwapSpellInput()
    {
        spellHandler.HandleSpellSwap(switchSpellInput);
    }

    //void Start()
    //{
    //    controls = PC_Controls.Instance;
    //    stateManager = PC_StateManager.Instance;
    //}

    //void FixedUpdate()
    //{
    //    #region Level Cheat Codes
    //    if(Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.LeftAlt))
    //    {
    //        SceneManager.LoadScene("Level 1");
    //    }
    //    if (Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.LeftAlt))
    //    {
    //        SceneManager.LoadScene("Level 2");
    //    }
    //    if (Input.GetKey(KeyCode.Alpha3) && Input.GetKey(KeyCode.LeftAlt))
    //    {
    //        SceneManager.LoadScene("Level 3");
    //    }
    //    #endregion


    //    /* Remove this we need a pause menu */
    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        SceneManager.LoadScene(SceneTracker.Instance.GetCurrentScene());
    //    }

    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        Application.Quit();
    //    }

    //    /* Get Camera Input */
    //    float mouseX = Input.GetAxis("Mouse X");
    //    float mouseY = Input.GetAxis("Mouse Y");

    //    /* Order is WASD */
    //    bool[] movementInput = new bool[]
    //    {
    //    Input.GetKey(controls.moveForward),
    //    Input.GetKey(controls.moveLeft),
    //    Input.GetKey(controls.moveBack),
    //    Input.GetKey(controls.moveRight),
    //    };


    //    /* Jump should cancel spellcasting */
    //    bool jumpInput = Input.GetKey(controls.jump);

    //    bool sprintInput = Input.GetKey(controls.sprint);

    //    bool dashInput = Input.GetKey(controls.dash);

    //    bool castInput = Input.GetKey(controls.leftHand);

    //    bool meleeInput = Input.GetKey(controls.rightHand);

    //    stateManager.RecieveCastInput(castInput);
    //    stateManager.RecieveSprintInput(sprintInput);
    //    stateManager.RecieveJumpInput(jumpInput);
    //    stateManager.ReceiveDashInput(dashInput);
    //    stateManager.RecieveMeleeInput(meleeInput);
    //    SendMovementInput(movementInput, mouseX, mouseY); /* We send dash to movement because it involves physics */
    //    PC_StateManager.Instance.UpdateLocomotionState();
    //    playerVitals.RegenStats();

    //}


    //void SendMovementInput(bool[] _movementInput, float _mouseX, float _mouseY)
    //{
    //    movementController.RecieveMovementInput(_movementInput, _mouseX, _mouseY);
    //}

}
