using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{

    public static CastingBar Instance;
    CanvasGroup canvasGroup;
    public Image barImage;
    public Image abilityIconImage;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //public void UpdateCastBarFill(AbilityManager.Abilities _ability, float _timeCasting, float _abilityCastTime)
    //{
    //    canvasGroup.alpha = 1.0f;
    //    abilityIconImage.sprite = AbilityIconsManager.Instance.IconToDisplay(_ability);
    //    barImage.fillAmount = Mathf.Lerp(1, 0, _timeCasting/ _abilityCastTime);
    //}

    //public void HideCastBar()
    //{
    //    canvasGroup.alpha = 0.0f;
    //}

}
