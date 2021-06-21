using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeContinueBtn : MonoBehaviour
{
    PC_UIManager uiManager;

    void Start()
    {
        uiManager = GetComponentInParent<PC_UIManager>();
    }

    public void SignalUIManagerContinueClicked()
    {
        uiManager.DialogueContinueBtnPressed();
    }

}
