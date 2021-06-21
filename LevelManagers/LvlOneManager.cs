using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlOneManager : LvlManager
{
    /* There is a lot of hard code here because a lot of stuff is unique to each level */
    /* Keeps track of information and manages certain things in scene 1 */
    bool firstMonsterKilled = false;

    protected override void Start()
    {
        /* Need to get access to these here because doing it in awake is a race condition */
        SetupLvlManager(1);
        SetNextObjective();

        AudioManager.Instance.Play("Level1Music");
        //AudioManager.Instance.Play("Level1Intro", true);
    }

    public override void NotifyInCombat()
    {
        if(!playerInCombat)
        {
            AudioManager.Instance.Play("FirstMonsterApproach", true);
            playerInCombat = true;
        }
    }

    void PlayFirstLightTutorial()
    {
        AudioManager.Instance.Play("Level1LightTutorial", true);
    }
}
