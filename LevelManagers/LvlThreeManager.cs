using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlThreeManager : LvlManager
{
    protected override void Start()
    {
        SetupLvlManager(3);

        transformationControl.ActivateLevelTransformation(2);


        SetNextObjective();

        AudioManager.Instance.Play("Level3Music");

        AudioManager.Instance.Play("Level_3_Start");

        //PauseGameForNarrative();
    }
}
