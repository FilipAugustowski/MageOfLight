using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_RootSpell : PC_SpellObject
{
    float timeTillDie = 0.3f;
    public GameObject RootEffect;

    float rootTime = 5f;

    int enemiesHit;
    public override void ActivateSpell()
    {
        enemiesHit = 0;
        Invoke(nameof(Destroy), .5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ++enemiesHit;
            other.gameObject.GetComponent<EC_EnemyVitals>().HandleRoot(rootTime);
            GameObject root = Instantiate(RootEffect, other.gameObject.transform.position, Quaternion.identity);
            root.transform.parent = other.gameObject.transform;
            root.GetComponent<RootObjectEffect>().Init(rootTime);
        }
    }

    void Destroy()
    {
        if (enemiesHit == 0) PC_UIManager.Instance.DisplayTooltip("No Enemies in Range", true, 6.0f);
        Destroy(this.gameObject);
    }

    void HideEffects()
    {
        GetComponentInChildren<RFX4_EffectSettings>().IsVisible = false;
    }
}
