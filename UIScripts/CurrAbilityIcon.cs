using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrAbilityIcon : MonoBehaviour
{
    public static CurrAbilityIcon Instance;
    Sprite currSprite;
    Image image;

    public PC_SpellHandler spellHandler;

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

        image = GetComponent<Image>();
        spellHandler = FindObjectOfType<PC_SpellHandler>();

    }

    void Start()
    {
        //currSprite = spellHandler.GetCurrentSpellSprite();
        //image.sprite = currSprite;
    }

    public void UpdateCurrentSpellSprite()
    {
        currSprite = spellHandler.GetCurrentSpellSprite();
        image.sprite = currSprite;

    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    currSprite = AbilityIconsManager.Instance.IconToDisplay(AbilityManager.Abilities.FIREBALL);
    //    image = GetComponent<Image>();
    //    image.sprite = currSprite;
    //}

    //public void ChangeCurrAbilityIcon(AbilityManager.Abilities _ability)
    //{
    //    image.sprite = AbilityIconsManager.Instance.IconToDisplay(_ability);
    //}
}
