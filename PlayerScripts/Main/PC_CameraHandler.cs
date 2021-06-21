using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_CameraHandler : MonoBehaviour
{
    PC_PlayerManager playerManager;
    PC_InputManager inputManager;

    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static PC_CameraHandler Instance;
    PC_UIManager playerUI;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.1f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;
    public float lockedPivotPosition = 4.5f;
    public float unlockedPivotPosition = 3.5f;

    void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        targetTransform = FindObjectOfType<PC_PlayerManager>().transform;
        playerManager = targetTransform.GetComponent<PC_PlayerManager>();
        inputManager = targetTransform.GetComponent<PC_InputManager>();
        playerUI = FindObjectOfType<PC_UIManager>();

        ignoreLayers = ~(1 << 10); /* 1 << 8 | 1 << 9 |  other layers that could be added */
    }




    public void FollowTarget(float _delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, 
            targetTransform.position,
            ref cameraFollowVelocity,
            _delta / followSpeed);
        myTransform.position = targetPosition;

        HandleCameraCollision(_delta);
    }

    public void HandleCameraRotation(float _delta, float _mouseXInput, float _mouseYInput)
    {

        lookAngle += (_mouseXInput * lookSpeed) / _delta;
        pivotAngle -= (_mouseYInput * pivotSpeed) / _delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;


    }

    private void HandleCameraCollision(float _delta)
    {
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, 
            Mathf.Abs(targetPosition), ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = -minimumCollisionOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, _delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }

    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(.6f, lockedPivotPosition);
        Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

        if (playerManager.isInCombatMode)
        {
            playerUI.EnableReticle();
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            playerUI.DisableReticle();
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }
    public void SetLookSpeed(float look)
    {
        lookSpeed = look;
    }
    public void SetPivotSpeed(float pivot)
    {
        pivotSpeed = pivot;
    }

}
