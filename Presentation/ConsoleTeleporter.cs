using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleTeleporter : MonoBehaviour
{

    PC_InputManager inputManager;

    public Transform[] tpPoints;

    // Start is called before the first frame update
    void Awake()
    {
        inputManager = FindObjectOfType<PC_InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.tp1 && tpPoints.Length > 0)
        {
            inputManager.transform.position = tpPoints[0].transform.position;
        }
        if (inputManager.tp2 && tpPoints.Length > 1)
        {
            inputManager.transform.position = tpPoints[1].transform.position;
        }
        if (inputManager.tp3 && tpPoints.Length > 2)
        {
            inputManager.transform.position = tpPoints[2].transform.position;
        }
        if (inputManager.lvl1)
        {
            SceneManager.LoadScene(2);
        }
        if (inputManager.lvl2)
        {
            SceneManager.LoadScene(3);
        }
        if (inputManager.lvl3)
        {
            SceneManager.LoadScene(4);
        }
        if (inputManager.lvl4)
        {
            SceneManager.LoadScene(5);
        }
    }
}
