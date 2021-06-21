using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/EnemySpell")]


/* Add spells to attack actions and design the spells */
/* Finish off day by getting mob C back in and trying to apply falling */

public class EC_Spell : ScriptableObject
{
    public GameObject spellObject;

    public void ActivateSpellObject(Vector3 _spellSpawnPos)
    {
        GameObject clone = GameObject.Instantiate(spellObject, _spellSpawnPos, Quaternion.identity);
        clone.GetComponent<EC_SpellObject>().ActivateSpell();
    }
}
