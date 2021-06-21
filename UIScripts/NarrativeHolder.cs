using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeHolder : MonoBehaviour
{
    /* Narrative holder is called by the narrative manager */
    /* Narrartive holder is passed in a narrative ID which is an ID associated with a monster. If its the 2nd monster killed then it will be associateed with the second Narrative entry in the narrative list */
    /* Whenever a new narrative is added you can see that there are two sentences passed into the narrative object, one sentence is for a monster noticing you and another is for a monster dying */
    /* To make new narratives for monsters just create a new narrative object with its narrative sentences. And make sure to set the ID of the monster to the same position as the narrative object in the narratives list */
    /* New types of narratives for all monsters can be added in the narrative script */


    List<Narrative> narratives;


    void Start()
    {
        narratives = new List<Narrative>();

        narratives.Add(new Narrative(
            "Something gnarled and ugly crept towards me. I felt it preying on my deepest fears, feeding on my wildest anxieties.",
            "I had succeeded in killing the first demon. In it's place light shone... I walked towards it."));
        narratives.Add(new Narrative(
            " ",
            " "));
        narratives.Add(new Narrative(
            " ",
            " "));
        narratives.Add(new Narrative(
            " ",
            " "));
        narratives.Add(new Narrative(
            " ",
            " "));

    }

    public string GetNarrativeText(int _narrativeID, NarrativeManager.NarrativeTriggers _narrativeTrigger)
    {
        return narratives[_narrativeID].ReturnAskedForText(_narrativeTrigger);
    }


}
