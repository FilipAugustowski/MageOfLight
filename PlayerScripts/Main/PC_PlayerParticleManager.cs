using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_PlayerParticleManager : MonoBehaviour
{
    EasyGameStudio.Jeremy.Dissolve[] playerCharacterDissolvers;

    public void Start()
    {
        playerCharacterDissolvers = GetComponentsInChildren<EasyGameStudio.Jeremy.Dissolve>();
    }

    public void HideCharacterForDash()
    {
        foreach(EasyGameStudio.Jeremy.Dissolve dissolver in playerCharacterDissolvers)
        {
            dissolver.hide();
        }

        Invoke(nameof(ShowCharacter), 1.5f);
    }

    public void ShowCharacter()
    {
        foreach (EasyGameStudio.Jeremy.Dissolve dissolver in playerCharacterDissolvers)
        {
            dissolver.show();
        }
    }
}
