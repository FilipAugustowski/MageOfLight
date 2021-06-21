using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Should be placed on gate or portal where the player will end the level. This basically loads them into the next scene */

public class EndPoint : MonoBehaviour 
{
    public static EndPoint Instance;

    RFX4_EffectSettings particleEffects;
    bool canEnd = false;

    public GameObject levelCinematic;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        particleEffects = GetComponentInChildren<RFX4_EffectSettings>(); /* Make sure that there is only one object as a child for the end point for each level */
        particleEffects.gameObject.SetActive(false);
    }

    public void ActivateExit()
    {
        particleEffects.gameObject.SetActive(true);
        particleEffects.IsVisible = true;
        canEnd = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(canEnd)
            {
                /* Play next level in the order defined by the scene management in build settings */

                PC_UIManager.Instance.HideNonNarrativeUI();
                LvlManager.Instance.DestroyAllMobs();
                levelCinematic.SetActive(true);
                levelCinematic.GetComponent<TimelineManager>().ActivateCinematic();
            }
            else
            {
                PC_UIManager.Instance.DisplayTooltip("Not enough Monsters killed!", true, 5.0f);
            }
        }
    }
}
