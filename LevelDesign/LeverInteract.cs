using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    public bool CanInteract = false;
    public GameObject door;
    public PC_InputManager inputManager;
    private Animator anim;

    public bool doorLever = true;
     
    public void Awake()
    {
        inputManager = FindObjectOfType<PC_InputManager>();
        anim = door.GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (inputManager.interactInput && CanInteract == true)
        {
            OpenDoor();
        }
    }
    private void OnTriggerEnter(Collider other)
    
    {
        if (other.CompareTag("Player"))
        {
            CanInteract = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanInteract = false;
        }

    
    }
    private void OpenDoor()
    {
        AudioManager.Instance.Play("Lever");
        if (doorLever) AudioManager.Instance.Play("DoorCreak");
        anim.SetBool("Isopen", true);
    }
}


