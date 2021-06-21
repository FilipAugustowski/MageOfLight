using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpray : MonoBehaviour
{
    float duration = 2.0f;

    Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        //pick an animation to play

        int x = Random.Range(0, 3);

        animator.SetInteger("AnimToPlay", x);

        Invoke(nameof(DestroyMe), duration);
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

}
