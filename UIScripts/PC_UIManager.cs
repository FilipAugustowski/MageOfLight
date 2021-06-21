using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PC_UIManager : MonoBehaviour
{
    NarrativeManager narrativeManager;
 
    public NarrativeContinueBtn continueBtn;
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider staminaSlider;

    public static PC_UIManager Instance;

    public Image fadeOutImage;
    public Image reticle;
    public Image objectiveImg;

    bool attackSpriteEnabled;
    Transform currObjectiveTransform;
    bool displayObjective;
    Transform playerTransform;
    Color objImgColor;
    float objectiveDistanceTillFade = 20.0f;


    /* Tooltip stuff */
    public Text tooltipText;
    float fadeInTimeNorm = 1.0f;
    float pauseFadeInTime = 3.0f;
    float fadeOutTime = 1.0f;
    float averageReadTime = 10.0f;
    bool overrideFadeIn = false;
    bool overrideFadeOut = false;


    void Awake() /* add dont destroy on load */
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        continueBtn = GetComponentInChildren<NarrativeContinueBtn>();
        continueBtn.gameObject.SetActive(false);
        displayObjective = false;
        objImgColor = objectiveImg.color;
    }

    void Start()
    {
        DisableReticle();
        narrativeManager = NarrativeManager.Instance;
        fadeOutImage.enabled = true;
        CameraFadeOut();
    }

    /* https://www.youtube.com/watch?v=oBkfujKPZw8&ab_channel=OmarBalfaqih - objective marker stuff */
    void FixedUpdate()
    {
        #region Moves our Objective Image if there is an objective
        if (displayObjective)
        {
            float minX = objectiveImg.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = objectiveImg.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.width - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(currObjectiveTransform.position);

            if(Vector3.Dot((currObjectiveTransform.position - playerTransform.position), playerTransform.forward) < 0)
            {
                objectiveImg.color = objImgColor;

                /* Target behind player */
                if(pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }

            }

            if (Vector3.Dot((currObjectiveTransform.position - playerTransform.position), playerTransform.forward) > 0)
            {
                Color c = objectiveImg.color;

                c.a = 0.5f;

                objectiveImg.color = c;

            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            objectiveImg.transform.position = pos;

            float distanceToObjective = Vector3.Distance(currObjectiveTransform.position, playerTransform.position);

            if(distanceToObjective < objectiveDistanceTillFade)
            {
                Color c = objectiveImg.color;

                c.a = 0.0f;

                objectiveImg.color = c;
            }


        }
        #endregion
    }

    public void HideNonNarrativeUI()
    {
        CanvasGroup[] uiElements = GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup uiElement in uiElements)
        {
            if(uiElement.tag != "NarrativeUI" && uiElement.tag != "PauseMenu")
            {
                uiElement.alpha = 0;
            }
        }
    }

    public void ReenableNonNarrativeUI()
    {
        CanvasGroup[] uiElements = GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup uiElement in uiElements)
        {
            if (uiElement.tag != "NarrativeUI" && uiElement.tag != "CooldownIcon")
            {
                uiElement.alpha = 1f;
            }
        }
    }

    public void ChangeObjectiveIconToInteractble()
    {
        /* We should switch objective images for certain things later on to guide the player more*/
    }

    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.unscaledDeltaTime / t));
            yield return null;
        }
    }

    public void ClearObjective()
    {
        displayObjective = false;
        objectiveImg.enabled = false;
    }

    public void SignalNewObjectiveToTrack(Transform _transformToTrack, Transform _playerTransform)
    {
        attackSpriteEnabled = true;
        displayObjective = true;
        objectiveImg.enabled = true;
        currObjectiveTransform = _transformToTrack;
        playerTransform = _playerTransform;
    }

    public void DialogueContinueBtnPressed()
    {
        continueBtn.gameObject.SetActive(false);
        narrativeManager.SignalPlayerFinishedDialogue();
        //narrativeManager.ClearNarrativeText();
        //DisableMouse();
    }

    public void SignalGamePaused()
    {
        continueBtn.gameObject.SetActive(true);
        continueBtn.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        EnableMouse();
    }

    void EnableMouse()
    {
        /* Setup Cursor */
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void EnableMouseForGamePlay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void DisableMouse()
    {
        /* Setup Cursor */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateHealth(float _healthValue)
    {
        healthSlider.value = _healthValue;
    }

    public void SetupHealth(int _healthValue)
    {
        healthSlider.maxValue = _healthValue;
        healthSlider.value = _healthValue;
    }

    public void UpdateMana(float _manaValue)
    {
        manaSlider.value = _manaValue;
    }

    public void SetupMana(float _manaValue)
    {
        manaSlider.maxValue = _manaValue;
        manaSlider.value = _manaValue;
    }

    public void UpdateStamina(float _staminaValue)
    {
        staminaSlider.value = _staminaValue;
    }

    public void SetupStamina(float _staminaValue)
    {
        staminaSlider.maxValue = _staminaValue;
        staminaSlider.value = _staminaValue;
    }

    public void CameraFadeIn(float _fadeTime = 2)
    {
        fadeOutImage.CrossFadeAlpha(1, _fadeTime, true);
    }

    public void CameraFadeOut(float _fadeTime = 5)
    {
        fadeOutImage.CrossFadeAlpha(0, _fadeTime, true);

    }

    public void EnableReticle()
    {
        reticle.enabled = true;
    }

    public void DisableReticle()
    {
        reticle.enabled = false;
    }


    public void DisplayTooltip(string _tooltip, bool _clearToolTip = false, float _readTime = 5.0f)
    {
        StartCoroutine(FadeTextToFullAlpha(fadeOutTime, tooltipText)); /* call this by trigger box */
        tooltipText.text = _tooltip;

        if (_clearToolTip) Invoke(nameof(ClearTooltip), _readTime);
    }

    public void ClearTooltip()
    {
        StartCoroutine(FadeTextToZeroAlpha(fadeOutTime, tooltipText));
    }


    /* Taken From https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/ */
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f && !overrideFadeIn)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.unscaledDeltaTime / t));
            yield return null;
        }

        overrideFadeOut = false;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f && !overrideFadeOut)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.unscaledDeltaTime / t));
            yield return null;
        }

        overrideFadeIn = false;
        tooltipText.text = "";

    }

}
