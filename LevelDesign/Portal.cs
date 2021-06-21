using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal connection;

    public Transform tpPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = connection.tpPos.position;
            other.gameObject.transform.rotation = connection.tpPos.rotation;

            PC_UIManager.Instance.CameraFadeIn(.5f);

            Invoke(nameof(FadeOut), .6f);
        }
    }

    void FadeOut()
    {
        PC_UIManager.Instance.CameraFadeOut(1.25f);

    }
}
