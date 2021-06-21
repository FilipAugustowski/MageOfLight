using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Enemy Actions/Attack Action")]
public class EC_EnemyAttackAction : EC_EnemyAction
{
    public int attackScore = 3; /* Gives weight to which attack is chosen */
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;

    public float damage = 15;
    public int force = 15;

    public bool hasSpell = false;
    public EC_Spell spell;
    public float spellCD = 10.0f;
}
