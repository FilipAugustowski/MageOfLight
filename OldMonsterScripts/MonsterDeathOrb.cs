using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is the script attached to the object that is spawned when a monster dies */
public class MonsterDeathOrb : MonoBehaviour
{
    //protected NarrativeManager playerNarrativeManager;
    //protected bool used = false;
    //public GameObject absorbtionParticlesObject;
    //protected RFX4_EffectSettings absorbtionParticles;
    //protected Transform playerTransform;

    //protected MonsterAI whoISpawnedFrom;

    //protected void OnEnable()
    //{
    //    playerNarrativeManager = NarrativeManager.Instance;
    //    UIManager.Instance.ChangeObjectiveIconToInteractble();
    //}

    //protected void OnTriggerStay(Collider other)
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
    //            Debug.Log("Missing Purge functionality");
    //            //if (playerAbilityManager.CanPurge())
    //            //{
    //            //    AudioManager.Instance.Play("LightTurnOn");
    //            //    used = true;
    //            //    playerAbilityManager.RecievePurgeMonster();
    //            //    //absorbtionParticles.IsVisible = true;
    //            //    //StartCoroutine(MoveAbsorbtionParticlesToPlayer());
    //            //    Invoke(nameof(DestroyOrb), 1.5f);
    //            //}
    //        }
    //    }
    //}

    //protected IEnumerator MoveAbsorbtionParticlesToPlayer(float _timer = 0)
    //{
    //    _timer += Time.time;

    //    while(_timer < 1.5f)
    //    {
    //        absorbtionParticlesObject.transform.position = Vector3.Lerp(transform.position, playerTransform.position, 3.0f);

    //        yield return null;
    //    }

    //}

    //protected void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerNarrativeManager.ClearTooltip();
    //    }
    //}

    //protected virtual void DestroyOrb()
    //{
    //    playerNarrativeManager.ClearTooltip();
    //    whoISpawnedFrom.SignalLightingManagerPurged(transform.position); 
    //    Destroy(this.gameObject);
    //}

    //public void PassInLightingLogic(MonsterAI _monsterAI)
    //{
    //    whoISpawnedFrom = _monsterAI;
    //}

}
