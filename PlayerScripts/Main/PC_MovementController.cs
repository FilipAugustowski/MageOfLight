using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/* Sources
 * https://www.youtube.com/watch?v=1tSt-ZeEZNA&list=PLA6Gf0nq2Gh6zMgOKQGuYdXreOLraKWRz&index=6&ab_channel=DitzelGames&t=237s
 * https://www.youtube.com/watch?v=9rr1Oc0pvLA&ab_channel=DitzelGames
 * https://www.youtube.com/watch?v=7nxpDwnU0uU&ab_channel=StephenBarr
 */

/* Summary:
 * The following script is responsible for activating the physics on the player's rigid body when movement is input. 
 * 
*/

public class PC_MovementController : MonoBehaviour
{
    PC_PlayerManager playerManager;
    PC_InputManager inputManager;
    PC_PlayerVitals playerVitals;
    PC_CameraHandler cameraHandler;
    public Vector3 moveDirection;

    public bool canMove;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public PC_AnimatorController animatorController;

    [HideInInspector]
    public new Rigidbody rigidbody;
    public Transform cameraObject;

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float walkingSpeed = 1;
    [SerializeField]
    float sprintSpeed = 7;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    public float fallingSpeed = 45;

    [Header("Combat Mode Effectors")]
    [SerializeField]
    float combatModeSpeedEffect = .85f;
    [SerializeField]
    float combatModeRotSpeedEffect = .85f;

    [Header("Ground and Air Detection Stats")]
    [SerializeField]
    public float groundDetectionRayStartPoint = 0.4f;
    [SerializeField]
    public float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField]
    public float groundDirectionRayDistance = 0.2f;
    [SerializeField]
    public LayerMask ignoreForGroundChecks;
    [SerializeField]
    public float inAirTimer;

    public float firstFrameFallEffector = 4;

    private new CapsuleCollider collider;

    void Awake()
    {
        cameraHandler = FindObjectOfType<PC_CameraHandler>();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<PC_InputManager>();
        animatorController = GetComponentInChildren<PC_AnimatorController>();
        playerManager = GetComponent<PC_PlayerManager>();
        playerVitals = GetComponent<PC_PlayerVitals>();
        collider = GetComponent<CapsuleCollider>();

        playerManager.isGrounded = true;
        myTransform = transform;
        animatorController.Initialize();
        canMove = true;
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    public void HandleRotation(float _delta)
    {
        if (playerManager.isInCombatMode)
        {
            if(inputManager.sprintFlag || playerManager.isRolling || inputManager.rollFlag)
            {
                Vector3 targetDirection = Vector3.zero;
                targetDirection = cameraHandler.cameraTransform.forward * inputManager.vertical;
                targetDirection += cameraHandler.cameraTransform.right * inputManager.horizontal;
                targetDirection.Normalize();
                targetDirection.y = 0;

                if (targetDirection == Vector3.zero)
                {
                    targetDirection = transform.forward;
                }

                Quaternion tr = Quaternion.LookRotation(targetDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime); /* might need to affect rotation speed */

                transform.rotation = targetRotation;
            }
            else
            {
                Vector3 rotationDirection = moveDirection;
                rotationDirection = cameraHandler.cameraTransform.forward;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                transform.rotation = targetRotation;

            }

        }
        else
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputManager.moveAmount;

            targetDir = cameraObject.forward * inputManager.vertical;
            targetDir += cameraObject.right * inputManager.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero) targetDir = myTransform.forward;

            float rs = rotationSpeed;

            if (playerManager.isInCombatMode)
            {
                rs *= combatModeRotSpeedEffect;
            }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * _delta);

            myTransform.rotation = targetRotation;
        }
    }

    public void HandleMovement(float _delta)
    {
        /* may have to add check for out of mana */
        if (inputManager.rollFlag) return;
        if (playerManager.isInAir) return;
        if (playerManager.isTakingDamage) return;
        if (playerManager.isInteracting 
            && !playerManager.isCasting
            && !playerManager.isAccessingSheathe
            && !playerManager.isShrugging) return; /* Want to move while we cast and access sword */

        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection += cameraObject.right * inputManager.horizontal;
        moveDirection.Normalize();

        if(!playerManager.isInAir)
        {
            moveDirection.y = 0;

        }

        float speed = movementSpeed;

        /* Apply sprint speed if input is true and we are moving already */
        if (inputManager.sprintFlag && inputManager.moveAmount > 0.5)
        {
            speed = sprintSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
            playerVitals.DeductStamina(10 * _delta);
        }
        else
        {
            if(inputManager.moveAmount < 0.5)
            {
                moveDirection *= walkingSpeed;
                playerManager.isSprinting = false;
            }
            moveDirection *= speed;
            playerManager.isSprinting = false;

        }

        if (playerManager.isInCombatMode) /* Slow down the player if sword is drawn */
        {
            moveDirection *= combatModeSpeedEffect;
        }

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        if (playerManager.isInCombatMode && !inputManager.sprintFlag)
        {
            animatorController.UpdateAnimatorValues(inputManager.vertical, inputManager.horizontal, playerManager.isSprinting);
        }
        else
        {
            animatorController.UpdateAnimatorValues(inputManager.moveAmount, 0, playerManager.isSprinting);
        }
    }

    #endregion

    public void HandleRollingAndSpring(float _delta)
    {
        if (animatorController.GetIsInteracting()) return;

        if(inputManager.rollFlag)
        {
            moveDirection = cameraObject.forward * inputManager.vertical;
            moveDirection += cameraObject.right * inputManager.horizontal;


            if (inputManager.moveAmount > 0)
            {
                animatorController.PlayTargetAnimation("Rolling", true, 1);
                playerVitals.DeductStamina(10);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else
            {
                animatorController.PlayTargetAnimation("Backstep", true, 1);
            }
        }
        
    }

    //https://www.youtube.com/watch?v=LOC5GJ5rFFw&list=PLD_vBJjpCwJtrHIW1SS5_BNRk6KZJZ7_d&index=2&ab_channel=SebastianGraves
    public void HandleFalling(float delta, Vector3 moveDirection)
    {

        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if (playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up * fallingSpeed);
            rigidbody.AddForce(moveDirection * fallingSpeed / 10f);

        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        UnityEngine.Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundChecks)) /* Could add ignore for ground checks here */
        {
            if(hit.transform.gameObject.layer == 12)
            {
                playerManager.isDead = true;
            }


            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true; 
            targetPosition.y = tp.y;

            if (playerManager.isInAir)
            {
                if (inAirTimer > 1.0f)
                {
                    animatorController.PlayTargetAnimation("PC_Landing", true, 1);
                    inAirTimer = 0;
                }
                else
                {
                    if(!playerManager.isDashing) animatorController.StopCurrentAnimation();
                    inAirTimer = 0;
                }

                playerManager.isInAir = false;
            }
        }
        else
        {
            if (playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if (!playerManager.isInAir)
            {
                if (!playerManager.isInteracting)
                {
                    animatorController.PlayTargetAnimation("PC_Falling", true, 1);
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * movementSpeed * firstFrameFallEffector;

                /* On the first notice of not is in air we want a stronger force on the player */
                playerManager.isInAir = true;

            }

            if(playerManager.isGrounded)
            {
                if(playerManager.isInteracting || inputManager.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }
        }
        if(playerManager.isInteracting || inputManager.moveAmount > 0)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
        }
        else
        {
            myTransform.position = targetPosition;
        }
    }

    public void HandleJumping()
    {
        if (!playerManager.isJumping) EndJumping();
        if (playerManager.isInteracting) return;

        /* Add a is in the air bool that will add a particle effect to the players air jump */
        if (inputManager.jumpInput)
        {
            if(inputManager.moveAmount > 0) /* No reason to jump in place */
            {
                collider.height = .7f;
                groundDetectionRayStartPoint = 0.9f;
                moveDirection = cameraObject.forward * inputManager.vertical;
                moveDirection += cameraObject.right * inputManager.horizontal;
                animatorController.PlayTargetAnimation("PC_Jump", true, 1);
                moveDirection.y = 0; /* Let the root motion do all the work */
                Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = jumpRotation;

            }
        }
    }


    /* Dash needs work */
    public void HandleAirDash()
    {
        if(inputManager.airDashInput)
        {
            if(playerManager.isJumping ||
                playerManager.isInAir)
            {
                /* Increase dash counter */
                animatorController.PlayTargetAnimation("PC_Falling", true, 1);
                animatorController.PlayTargetAnimation("PC_Dash", true, 2);
                StartCoroutine(DashForce());
            }
        }
    }

    private IEnumerator DashForce()
    {
        yield return new WaitForSeconds(0.1f);

        Vector3 dashForce = Camera.main.transform.forward * 10;

        /* Velocity Change ignores the mass of the rigidbody */
        rigidbody.AddForce(dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(1);

        rigidbody.velocity = Vector3.zero;

        yield return new WaitForSeconds(.5f);

        animatorController.animator.SetBool("Dashing", false);
    }

    public void EndJumping()
    {
        collider.height = 1.8f;
        groundDetectionRayStartPoint = .2f;
    }


    #region Get/Set

    public Vector3 GetPlayerVelocity()
    {
        return rigidbody.velocity;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public void SetRotationSpeed(float _input)
    {
        rotationSpeed = _input;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetMovementSpeed(float _input)
    {
        movementSpeed = _input;
    }
    public void AllowMove()
    {
        canMove = true;
        UnityEngine.Debug.Log("BE FREE");
    }

    #endregion
}