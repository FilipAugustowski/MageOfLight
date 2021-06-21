using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownIcon : MonoBehaviour
{
    CanvasGroup canvasGroup;
    Sprite currIconToDisplay;
    public Image highlightImage;
    Image abilityIcon;
    public bool beingUsed;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        abilityIcon = GetComponent<Image>();
        beingUsed = false;
        HideIcon();
    }

    public void InitiateIconCooldownLogic(float _cooldown, Sprite _iconSprite)
    {
        beingUsed = true;
        abilityIcon.sprite = _iconSprite;
        canvasGroup.alpha = 1.0f;

        StartCoroutine(ChangeFillAmount(_cooldown));
    }

    IEnumerator ChangeFillAmount(float _cooldown)
    {
        float timer = 0;

        while (timer < _cooldown)
        {
            timer += Time.deltaTime;

            //Lerp the fill amount from 1 to 0 over the duration
            highlightImage.fillAmount = Mathf.Lerp(1, 0, timer / _cooldown);

            yield return null;
        }

        HideIcon();
        beingUsed = false;
        /* add the ui fading away here */
    }

    public void HideIcon()
    {
        canvasGroup.alpha = 0.0f;
    }
}
