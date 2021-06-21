using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* NO LONGER BEING USED */
public class UIManager : MonoBehaviour
{
    NarrativeManager narrativeManager;
    //public LevelOneManager levelManager;
    public NarrativeContinueBtn continueBtn;
    public Slider healthSlider;
    public Slider manaSlider;

    public static UIManager Instance;

    public Image fadeOutImage;

    /* Objective stuff */ /* WORK: change the sprite on the objective that displayhs when an orb is dropped, the orb should call this method when it spawns to change the icon, icon is changed back when new objective is set */
    public Image objectiveImg;
    public Sprite attackSprite;
    public Sprite purgeOrbSprite;
    bool attackSpriteEnabled;
    Transform currObjectiveTransform;
    bool displayObjective;
    Transform playerTransform;
    Color objImgColor;
    float objectiveDistanceTillFade = 20.0f;

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
        narrativeManager = NarrativeManager.Instance;
        fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, 0);
        fadeOutImage.gameObject.SetActive(false);
    }

    /* https://www.youtube.com/watch?v=oBkfujKPZw8&ab_channel=OmarBalfaqih - objective marker stuff */
    void FixedUpdate()
    {
        if(displayObjective)
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
    }

    public void HideNonNarrativeUI()
    {
        CanvasGroup[] uiElements = GetComponentsInChildren<CanvasGroup>();
        foreach (CanvasGroup uiElement in uiElements)
        {
            if(uiElement.tag != "NarrativeUI")
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

        Invoke(nameof(EndGame), 3.0f);
    }

    void EndGame()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void BeginFadeOut()
    {
        Invoke(nameof(FadeOutCamera), 10.0f);
    }

    public void FadeOutCamera()
    {
        fadeOutImage.gameObject.SetActive(true);

        StartCoroutine(FadeImageToFullAlpha(5.0f, fadeOutImage));
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

}
