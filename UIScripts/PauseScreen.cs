using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseScreen : MonoBehaviour
{
    LvlManager level;
    bool isPaused = false;
    public GameObject menu;

    public Slider x_sens;
    public Slider y_sens;
    private PC_CameraHandler cam;

    PC_InputManager inputManager;

    void Awake()
    {
        inputManager = FindObjectOfType<PC_InputManager>();
        menu.SetActive(false);
    }

    void Start()
    {
        level = LvlManager.Instance;
        cam = Camera.main.GetComponentInParent<PC_CameraHandler>();
    }

    /* Probably want to put this in the player manager update later on */
    void Update()
    {
        if (inputManager.pauseInput)
        {
            if (!isPaused)
            {
                level.PauseGameForNarrative();
                isPaused = true;
                menu.SetActive(true);
                PC_UIManager.Instance.continueBtn.gameObject.SetActive(false);
            }
            else
            {
                level.ResumeGame();
                isPaused = false;
                menu.SetActive(false);
                PC_UIManager.Instance.continueBtn.gameObject.SetActive(false);
            }
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void UpdateLook()
    {
        cam.SetLookSpeed(x_sens.value);
    }
    public void UpdatePivot()
    {
        cam.SetPivotSpeed(y_sens.value);
    }
}
