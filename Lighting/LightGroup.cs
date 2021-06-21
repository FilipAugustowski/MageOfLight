using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGroup : MonoBehaviour
{
    public Light[] myLights;

    float maxLightIntensity;
    float minLightIntesity = 0;
    public float lightStep = 50;
    public float startingLightIntensity = 0;

    void Awake()
    {
        myLights = GetComponentsInChildren<Light>();

        maxLightIntensity = myLights[0].intensity;

    }

    public void TurnOn()
    {
        foreach(Light light in myLights)
        {
            light.enabled = true;
            StartCoroutine(IncreaseLightGradual(light));
        }
    }

    IEnumerator IncreaseLightGradual(Light _light)
    {
        while(_light.intensity < maxLightIntensity)
        {
            _light.intensity += lightStep * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator DecreaseLightGradual(Light _light)
    {
        while (_light.intensity > minLightIntesity)
        {
            _light.intensity -= lightStep * Time.deltaTime;

            yield return null;
        }

        _light.enabled = false;
    }

    public void TurnOff()
    {
        foreach (Light light in myLights)
        {
            light.enabled = false;
            light.intensity = startingLightIntensity;
        }
    }
}
