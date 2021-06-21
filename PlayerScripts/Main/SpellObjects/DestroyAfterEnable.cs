using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEnable : MonoBehaviour
{
    public float destroyTime = 1.5f;

    void OnEnable()
    {
        Invoke(nameof(DestroyThisObject), destroyTime);
    }

    void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
