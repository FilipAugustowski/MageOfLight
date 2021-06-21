using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int priority;
    LvlManager level;
   
    void Start()
    {
        //level = GameObject.Find("LevelManager").GetComponent<LvlManager>();
        level = (LvlManager)FindObjectOfType(typeof(LvlManager));

    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            level.NewCheckpoint(transform.position, transform.eulerAngles, priority);

        }


    }
}
