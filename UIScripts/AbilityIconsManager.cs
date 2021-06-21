using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconsManager : MonoBehaviour
{
    public static AbilityIconsManager Instance;

    public Sprite fireballSprite;
    public Sprite dashSprite;
    public Sprite rootSprite;

    AbilityCooldownIcon[] cooldownIcons;

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

        cooldownIcons = GetComponentsInChildren<AbilityCooldownIcon>();
    }

    //public Sprite IconToDisplay(AbilityManager.Abilities _ability)
    //{
    //    if(_ability == AbilityManager.Abilities.FIREBALL)
    //    {
    //        return fireballSprite;
    //    }
    //    else if(_ability == AbilityManager.Abilities.DASH)
    //    {
    //        return dashSprite;
    //    }
    //    else if (_ability == AbilityManager.Abilities.ROOT)
    //    {
    //        return rootSprite;
    //    }

    //    Debug.Log("NO ability Icon input");
    //    return null; /* Should never hit this */
    //}

    //public void PutAbilityOnCooldownUI(float _cooldown, AbilityManager.Abilities _ability)
    //{
    //    foreach (AbilityCooldownIcon icon in cooldownIcons)
    //    {
    //        if(!icon.beingUsed)
    //        {
    //            icon.InitiateIconCooldownLogic(_cooldown, IconToDisplay(_ability));
    //            break;
    //        }
    //    }


    //}

    public void HideCooldowns()
    {
        foreach (AbilityCooldownIcon icon in cooldownIcons)
        {
            icon.HideIcon();
        }
    }

}
