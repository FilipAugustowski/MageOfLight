using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    Light[] lights;
    bool used;
    BoxCollider collider;
    public bool isSpellBook;
    public PC_Spell spell;
    public GameObject learnParticleEffects;

    public bool hasMessage = false;
    public string toolTipMessage = "";

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        if(isSpellBook)
        {
            learnParticleEffects.SetActive(false);
        }
        lights = GetComponentsInChildren<Light>();
        used = false;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            if (LvlManager.Instance.myLvl == 1) AudioManager.Instance.Play("Level_1_AfterSpellBook", true);
            else if (LvlManager.Instance.myLvl == 2) AudioManager.Instance.Play("Level_2_RootSpellBook", true);
            else if (LvlManager.Instance.myLvl == 3) AudioManager.Instance.Play("Level_3_Spellbook", true);
            collider.enabled = false;
            if(isSpellBook) learnParticleEffects.SetActive(true);
            other.gameObject.GetComponent<PC_SpellHandler>().AddSpell(spell);
            used = true;
            FadeOut();

            if(hasMessage) PC_UIManager.Instance.DisplayTooltip(toolTipMessage);

        }
    }

    void Update()
    {
        if(used)
        {
            foreach(Light light in lights)
            {
                light.intensity -= Time.deltaTime * 2;
            }
        }
    }

    void FadeOut()
    {

        GetComponent<MeshRenderer>().enabled = false;
        Invoke(nameof(Destroy), 7.0f);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void Destroy()
    {
        if (PC_UIManager.Instance.tooltipText.text == toolTipMessage) PC_UIManager.Instance.ClearTooltip();
        Destroy(this.gameObject);
    }


}


