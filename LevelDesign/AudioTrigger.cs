using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public string clipName;

    bool used = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !used)
        {
            used = true;
            AudioManager.Instance.Play(clipName, true);
        }
    }
}
