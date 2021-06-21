using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreBook : MonoBehaviour
{
    public string soundToPlay;
    public GameObject learnParticleEffects;
    ParticleSystem[] particleSystems;
    Light[] lights;
    bool used = false;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        used = false;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (used)
        {
            foreach (Light light in lights)
            {
                light.intensity -= Time.deltaTime * 2;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(soundToPlay != "")
            {
                AudioManager.Instance.Play(soundToPlay);
            }

            learnParticleEffects.SetActive(true);
            used = true;
            FadeOut();
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
        Destroy(this.gameObject);
    }


}
