using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Spell")]

public class PC_Spell : ScriptableObject
{
    public bool hasQuickCast;
    public string castingAnimation;
    public int castingAnimLayer;
    public int regularCastHitStagger;
    public string quickCastAnimation;
    public int quickCastAnimLayer;
    public int quickCastHitStagger;
    public string castSound;
    public string hitSound;
    public int quickManaCost;
    public int normalManaCost;
    public float cooldown;
    public bool onCooldown;
    public Sprite spellSprite;

    public GameObject quickCastSpellObject;
    public GameObject normalCastSpellObject;



    public void PutOnCooldown()
    {
        onCooldown = true;
    }

    public IEnumerator ResetCooldown() /* Some timer should always be input as 0 */
    {
        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
    }

    public bool CheckIfQuickCastable(float _currMana)
    {
        if (_currMana >= quickManaCost && hasQuickCast)
        {
            return true;
        }
        else return false;
    }

    public bool CheckIfNormalCastable(float _currMana)
    {
        if (_currMana >= normalManaCost && !onCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateNormalCastSpell(Vector3 _spellSpawnPos)
    {
        /* Rotate player to where the camera is aiming */
        GameObject clone = GameObject.Instantiate(normalCastSpellObject, _spellSpawnPos, Quaternion.identity);
        clone.GetComponent<PC_SpellObject>().ActivateSpell();
    }

    /* The activate spell function is called from */
    public void ActivateQuickCastSpell(Vector3 _spellSpawnPos)
    {
        /* Rotate player to where the camera is aiming */
        GameObject clone = GameObject.Instantiate(quickCastSpellObject, _spellSpawnPos, Quaternion.identity);
        clone.GetComponent<PC_SpellObject>().ActivateSpell();
    }

    void PlaySound()
    {
        Debug.Log("Spell sound not implemented");
    }
}
