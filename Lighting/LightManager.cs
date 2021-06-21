using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* There should be the same number of lights AS MONSTERS WITH IDs, if a monster does not have an ID it will not call on the lightmanager, nor will it call the level manager */
/* STARTING LIGHT/BAKED LIGHTS ARE NOT INCLUDED HERE */

public class LightManager : MonoBehaviour
{
    public LightGroup[] lightGroups;
    public IlluminatedObjectGroup[] IlluminatedGroups; /* these are made public because we have to define an order */
    public static LightManager Instance;

    public GameObject lightParticlePrefab;
    public GameObject prefabNoParticle;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //lightGroups = GetComponentsInChildren<LightGroup>();

        foreach(LightGroup lightGroup in lightGroups)
        {
            lightGroup.TurnOff();
        }
        FindandTurnOff();
    }

    public void Illuminate(int _monsterID, Vector3 _orbPos)
    {
        if(_monsterID < IlluminatedGroups.Length)
        {
            foreach(IlluminatedObject illuminatedObject in IlluminatedGroups[_monsterID].IlluminatedObjects)
            {

                if(illuminatedObject.needsTravelParticles)
                {
                    GameObject newParticle = Instantiate(lightParticlePrefab, _orbPos, Quaternion.identity);
                    newParticle.GetComponent<LightingParticle>().Init(illuminatedObject.transform.position, _monsterID);
                }
                else
                {

                }
            }
        }
        if (_monsterID < lightGroups.Length)
        {
            foreach (Light light in lightGroups[_monsterID].myLights)
            {
                GameObject newParticle = Instantiate(lightParticlePrefab, _orbPos, Quaternion.identity);
                newParticle.GetComponent<LightingParticle>().Init(light.transform.position, _monsterID);
            }
        }

    }

    public void IlluminateFromParticle(int _monsterID)
    {
        int length = IlluminatedGroups.Length;
        if (_monsterID < length)
        {
            IlluminatedGroups[_monsterID].gameObject.SetActive(true);
            IlluminatedGroups[_monsterID].Illuminate();
        }

        if (_monsterID < lightGroups.Length)
        {
            lightGroups[_monsterID].TurnOn();
        }
    }

    //IEnumerator ParticlesMoveToLights()
    //{

    //}

    void GrowAndLights(int _monsterID)
    {
        int length = IlluminatedGroups.Length;

        if (_monsterID < length)
        {
            IlluminatedGroups[_monsterID].gameObject.SetActive(true);
            IlluminatedGroups[_monsterID].Illuminate();
            lightGroups[_monsterID].TurnOn();

        }
    }
    void FindandTurnOff() /* should add turn off code to illuminatedobject groups */
    {
        IlluminatedGroups = GetComponentsInChildren<IlluminatedObjectGroup>();
    }

}
