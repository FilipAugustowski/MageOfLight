using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance;

    public int currScene = 0;
    public string currSceneName = "";
    public int lastScene = 0;
    public string lastSceneName = "";


    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        SceneManager.sceneLoaded += OnLevelFinishedLoad;

    }

    void OnLevelFinishedLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnLevelFinishedLoad called");

        lastScene = currScene;
        currScene = scene.buildIndex;
        

        lastSceneName = currSceneName;
        currSceneName = scene.name;


        InfoLoader infoLoader = FindObjectOfType<InfoLoader>();

        if (infoLoader) Invoke(nameof(CallInfoLoadUpdate), .1f);

    }

    void CallInfoLoadUpdate()
    {
        InfoLoader infoLoader = FindObjectOfType<InfoLoader>();

        infoLoader.UpdateInfo();
    }

    public int GetLastScene()
    {
        return lastScene;
    }

    public int GetCurrentScene()
    {
        return currScene;
    }
}
