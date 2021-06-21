using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoul : MonsterDeathOrb
{
    //NarrativeManager playerNarrativeManager;
    //bool used = false;
    //public GameObject absorbtionParticlesObject;
    //RFX4_EffectSettings absorbtionParticles;
    //Transform playerTransform;
    //MonsterAI whoISpawnedFrom;


    //void OnEnable()
    //{
    //    playerNarrativeManager = NarrativeManager.Instance;
    //    UIManager.Instance.ChangeObjectiveIconToInteractble();
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player" && !used)
    //    {
    //        /* Tell the player that in order to purge a monster you have to press F */
    //        //playerNarrativeManager = other.GetComponentInChildren<NarrativeManager>();
    //        playerTransform = other.GetComponent<Transform>();
    //        playerNarrativeManager.DisplayTooltip("Hold[F] to fill the space with light.");
    //        AbilityManager playerAbilityManager = other.GetComponent<AbilityManager>();

    //        if (Input.GetKey(KeyCode.F))
    //        {
    //            if (playerAbilityManager.CanPurge())
    //            {
    //                AudioManager.Instance.Play("LightTurnOn");
    //                used = true;
    //                playerAbilityManager.RecievePurgeMonster();
    //                //absorbtionParticles.IsVisible = true;
    //                //StartCoroutine(MoveAbsorbtionParticlesToPlayer());
    //                Invoke(nameof(DestroyOrb), 1.5f);
    //            }
    //        }
    //    }
    //}

    //IEnumerator MoveAbsorbtionParticlesToPlayer(float _timer = 0)
    //{
    //    _timer += Time.time;

    //    while (_timer < 1.5f)
    //    {
    //        absorbtionParticlesObject.transform.position = Vector3.Lerp(transform.position, playerTransform.position, 3.0f);

    //        yield return null;
    //    }

    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerNarrativeManager.ClearTooltip();
    //    }
    //}

    //protected override void DestroyOrb()
    //{
    //    UIManager.Instance.BeginFadeOut();
    //    //AudioManager.Instance.Play("WinGame");
    //    playerNarrativeManager.ClearTooltip();
    //    whoISpawnedFrom.SignalLightingManagerPurged(transform.position);
    //    Destroy(this.gameObject);
    //}

    //public void PassInLightingLogic(MonsterAI _monsterAI)
    //{
    //    whoISpawnedFrom = _monsterAI;
    //}

}

