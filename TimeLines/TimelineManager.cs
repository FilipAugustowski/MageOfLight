using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    PC_PlayerManager playerManager;
    PC_UIManager uiManager;
    public PlayableDirector timeline;
    TransformationControl transformationControl;
    public GameObject particleEffects;

    public int level = 1;

    bool played = false;
    bool loading = false;

    void Awake()
    {
        playerManager = FindObjectOfType<PC_PlayerManager>(); /* Turn off the player */
        transformationControl = GetComponentInChildren<TransformationControl>();
        uiManager = FindObjectOfType<PC_UIManager>();
        particleEffects.SetActive(false);
    }

    void Start()
    {
        ActivateCinematic();
    }

    public void ActivateCinematic()
    {
        uiManager.CameraFadeIn(.75f);

        Invoke(nameof(InvokeCinematic), .75f);
    }

    void InvokeCinematic()
    {
        playerManager.gameObject.SetActive(false);

        if (level - 1 >= 1) transformationControl.ActivateLevelTransformation(level - 1);
        if (level == 1) AudioManager.Instance.Play("Level_1_FirstTransformation", true);
        timeline.Play();
        uiManager.CameraFadeOut(.75f);

    }

    public void ActivateTransformation()
    {
        transformationControl.ActivateLevelTransformation(level);
    }
    public void ActivateParticleEffects()
    {
        particleEffects.SetActive(true);
    }
    public void DeactivateParticleEffects()
    {
        particleEffects.GetComponent<RFX4_EffectSettings>().IsVisible = false;
    }

    public void EndCinematic()
    {
        uiManager.CameraFadeIn(.75f);
        Invoke(nameof(LoadLevel), .75f);
    }

    void LoadLevel()
    {
        if (level != 4) SceneManager.LoadScene("TransitionScreens");
        else SceneManager.LoadScene("WinScreen");
    }

}
