using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    PC_UIManager uiManager;
    public static NarrativeManager Instance;
    LvlManager levelManager;
    public Text narrativeText;
    Vector3 narrativeTextStartingPos;
    string lastTextSet;
    NarrativeHolder narrativeHolder;
    float fadeInTimeNorm = 1.0f;
    float pauseFadeInTime = 3.0f;
    float fadeOutTime = 1.0f;
    float averageReadTime = 8.0f;
    public bool doStartingNarrative;

    bool overrideFadeIn = false;
    bool overrideFadeOut = false;
    //bool hidingText = false;

    /* Starting Narrative Stuff */
    int currStartingNarrative;
    int startingNarrativeSequencesLength;
    List<StartingNarrativeSequencesMethods> startingNarrativeSequences;
    delegate void StartingNarrativeSequencesMethods();


    void Awake() /* add dont destroy on load */
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        uiManager = FindObjectOfType<PC_UIManager>();

    }

    void Start()
    {
        levelManager = LvlManager.Instance;
        narrativeHolder = GetComponentInChildren<NarrativeHolder>();
        /* Add save position of canvas object for narative text, starting text should be centered */
        narrativeTextStartingPos = narrativeText.rectTransform.anchoredPosition;

        if (doStartingNarrative)
        {
            narrativeText.rectTransform.anchoredPosition = new Vector3(0, -420, 0); /* -260 sets to the middle since narrative text is anchored to the top of the screen */
            currStartingNarrative = 0;
            uiManager.HideNonNarrativeUI();
            CreateStartingNarratuveSequence();
            startingNarrativeSequences[currStartingNarrative]();
        }

    }




    public void ClearNarrativeText()
    {
        narrativeText.text = "";
    }

    public enum NarrativeTriggers
    {
        MonsterEncounteredPlayer,
        MonsterKilled,
        PlayerKilled,
        ColliderTrigger,
        KilledAllMonsters,
        NewSpellAcquired
    }

    public void RecieveNarrative(int _narrativeID, NarrativeTriggers _narrativeTrigger) /* Non monster related events are denoted by a negative number */
    {
        if(_narrativeID != -1)
        {
            /* play sound here */
            SetNarrativeText(narrativeHolder.GetNarrativeText(_narrativeID, _narrativeTrigger), fadeInTimeNorm);
            HideNarrative(fadeInTimeNorm + averageReadTime);
        }
    }    

    public void RecieveAnyNarrative(string _text, float _readTime = 8, bool _timer = true)
    {
        SetNarrativeText(_text, fadeInTimeNorm);
        if (_timer)
        {
            HideNarrative(fadeInTimeNorm + averageReadTime);
        }
    }


    void SetNarrativeText(string _narrativeText, float _fadeInTime)
    {
        overrideFadeOut = true;
        narrativeText.text = _narrativeText;
        StartCoroutine(FadeTextToFullAlpha(_fadeInTime, narrativeText));
    }

    void HideNarrative(float _fadeOutTime)
    {
        overrideFadeOut = false;
        StartCoroutine(FadeTextToZeroAlpha(_fadeOutTime, narrativeText));
    }

    #region Tutorial and Starting Narrative

    void CreateStartingNarratuveSequence()
    {
        startingNarrativeSequences = new List<StartingNarrativeSequencesMethods>();
        startingNarrativeSequences.Add(Intro);
        startingNarrativeSequences.Add(SpellTutorial);
        startingNarrativeSequences.Add(MeleeTutorial);
        startingNarrativeSequences.Add(DashTutorial);
        startingNarrativeSequencesLength = startingNarrativeSequences.Count; /* used for comparison on when to stop displaying proceding start narratives */
    }


    /* These starting narratives can probably put in the schema of the other narratives */
    public void Intro()
    {
        levelManager.PauseGameForNarrative();
        string narrative = "I woke in Azriel's prison, surrounded by darkness.";
        overrideFadeOut = true;
        SetNarrativeText(narrative, pauseFadeInTime);
    }

    public void SpellTutorial()
    {
        levelManager.PauseGameForNarrative();
        string narrative = "Azriel's minions descended upon me and I felt his influence. His powers were taking over my mind. Hold [Left Click] to cast Mental Fire or tap [Left Click] to quick cast.";
        overrideFadeOut = true;
        SetNarrativeText(narrative, pauseFadeInTime);
    }

    public void MeleeTutorial()
    {
        levelManager.PauseGameForNarrative();
        string narrative = "Press [ctrl] to go into combat mode, Hold [Right Click] for a Heavy attack, Tap [Right Click] for light attack";
        overrideFadeOut = true;
        SetNarrativeText(narrative, pauseFadeInTime);
    }

    public void DashTutorial()
    {
        levelManager.PauseGameForNarrative();
        string narrative = "Dash or Sprint with shift.";
        overrideFadeOut = true;
        SetNarrativeText(narrative, pauseFadeInTime);
        //Invoke(nameof(ClearNarrativeText), 4.0f);
    }

    public void AllMonstersKilled()
    {
        narrativeText.text = "I felt the portal opening… Azriel’s dominion lay somewhere beyond.";
        StartCoroutine(FadeTextToFullAlpha(pauseFadeInTime, narrativeText));
        Invoke(nameof(ClearNarrativeText), 4.0f);

    }

    public void SignalPlayerFinishedDialogue()
    {
        currStartingNarrative++;
        if (CheckContinueStartingNarratives())
        {
            HideNarrative(fadeOutTime);
            ContinueStartingNarratives();
        }
        else
        {
            overrideFadeIn = true;
            overrideFadeOut = false;
            HideNarrative(fadeOutTime);
            narrativeText.text = "";
            narrativeText.rectTransform.anchoredPosition = narrativeTextStartingPos;
            uiManager.ReenableNonNarrativeUI();
            AbilityIconsManager.Instance.HideCooldowns();
            levelManager.ResumeGame();

        }
    }

    void ContinueStartingNarratives()
    {
        startingNarrativeSequences[currStartingNarrative]();
    }

    bool CheckContinueStartingNarratives()
    {
        if (currStartingNarrative < startingNarrativeSequencesLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    /* Taken From https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/ */
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f && !overrideFadeIn)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.unscaledDeltaTime / t));
            yield return null;
        }

        overrideFadeOut = false;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f && !overrideFadeOut)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.unscaledDeltaTime / t));
            yield return null;
        }

        overrideFadeIn = false;
    }
}

