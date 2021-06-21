using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCDemonRotation : MonoBehaviour
{
    void OnEnable()
    {
        transform.LookAt(PC_PlayerManager.Instance.transform);
    }
}
