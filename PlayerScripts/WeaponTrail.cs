using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        DeactivateTrail();
    }

    public void DeactivateTrail()
    {
        foreach(ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
    }

    public void ActivateTrail()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }
}
