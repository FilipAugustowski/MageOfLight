using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
        public void NextScene()
        {
            Debug.Log("Scene Button");
            SceneManager.LoadScene(1);
        }
}
