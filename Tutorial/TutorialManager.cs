using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public TooltipZone meleeZone;
    public TooltipZone spellZone;

    public EC_EnemyVitals meleeDummy;
    public EC_EnemyVitals spellDummy;

    void Update()
    {
        if(meleeDummy.timesHit >= 9)
        {
            meleeZone.taskCompleted = true;
        }
        if(spellDummy.timesHit >= 3)
        {
            spellZone.taskCompleted = true;
        }
    }




}
