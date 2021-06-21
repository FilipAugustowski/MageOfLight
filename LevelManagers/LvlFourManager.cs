using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlFourManager : LvlManager
{
    protected override void Start()
    {
        SetupLvlManager(4);

        transformationControl.ActivateLevelTransformation(3);


        SetNextObjective();

        AudioManager.Instance.Play("OceanBreeze");

        AudioManager.Instance.Play("Level4Music");

        AudioManager.Instance.Play("Level_4_Start");
        //PauseGameForNarrative();
    }
}
