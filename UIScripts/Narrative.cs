using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* There are only two narratives per monster currently programmed, one can add more by making a new string for example string monsterScaredOfPlayer, add the string defintion into the Narrative constructor and add a case for it 
 * in ReturnAskedForText(NarrativeManager.Narrativetrigger _trigger). */ 

public class Narrative
{
    string encounterText;
    string monsterKilled;

    public Narrative(string _encounterText, string _killText)
    {
        encounterText = _encounterText;
        monsterKilled = _killText;
    }

    public string ReturnAskedForText(NarrativeManager.NarrativeTriggers _trigger)
    {
        switch (_trigger)
        {
            case NarrativeManager.NarrativeTriggers.MonsterEncounteredPlayer:
                return encounterText;
            case NarrativeManager.NarrativeTriggers.MonsterKilled:
                return monsterKilled;
        }

        return "error";

    }
}
