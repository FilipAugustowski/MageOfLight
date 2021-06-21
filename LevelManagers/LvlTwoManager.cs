using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlTwoManager : LvlManager
{
    int currMobPack;



    protected override void Start()
    {
        transformationControl.ActivateLevelTransformation(1);

        /* Need to get access to these here because doing it in awake is a race condition */
        SetupLvlManager(2);

        SetNextObjective();

        AudioManager.Instance.Play("Level2Music");

        currMobPack = 0;



        //playerManager.GetComponentInChildren<TransformationControl>().ActivateLevelTransformation(1);

        //FindMonsters();

        //PauseGameForNarrative();
        NarrativeManager.Instance.RecieveAnyNarrative("Press [Q] to Switch Spells. " +
            "Hold [Left Click] to cast a Root Spell. ", 8, true);


        //AudioManager.Instance.Play("SpellSwitchTutorial", true);
        //Invoke(nameof(PlayRootTutorial), 3.0f);
    }

   

    public override void NotifyInCombat()
    {
        if(!playerInCombat)
        {
            AudioManager.Instance.Play("Level2Group1Approach", true);
            playerInCombat = true;
        }
    }

    void PlayRootTutorial()
    {
        AudioManager.Instance.Play("RootSpellTutorial", true);

    }


}
