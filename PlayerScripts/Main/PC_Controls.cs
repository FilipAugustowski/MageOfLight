using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that gives option to change controls, all input should use these keycodes */
public class PC_Controls : MonoBehaviour
{
    public static PC_Controls Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveBack = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode jump = KeyCode.Space;
    public KeyCode leftHand = KeyCode.Mouse0;
    public KeyCode rightHand = KeyCode.Mouse1;
    public KeyCode chargeSpell = KeyCode.Q;
    public KeyCode switchSpell = KeyCode.E;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode dash = KeyCode.Z;
}
