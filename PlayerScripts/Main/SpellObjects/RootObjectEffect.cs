using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObjectEffect : MonoBehaviour
{
    RFX4_EffectSettings rootEffect;

    float rootTime; /* Should create a static instance of all ability values */
    float effectFadeOutTime = 0.5f;

    public void Init(float _rootTime)
    {
        rootEffect = GetComponent<RFX4_EffectSettings>();
        rootTime = _rootTime;
        rootEffect.FadeoutTime = effectFadeOutTime;
        rootEffect.IsVisible = true;

        Invoke(nameof(TurnOffAndDestroy), (rootTime - effectFadeOutTime));
    }

    void TurnOffAndDestroy()
    {
        rootEffect.IsVisible = false;
        Invoke(nameof(DestroyEffect), effectFadeOutTime);
    }

    void DestroyEffect()
    {
        Destroy(this);
    }
}
