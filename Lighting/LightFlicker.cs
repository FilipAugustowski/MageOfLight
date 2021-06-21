using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float min, max, range;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
    }
    void Flicker()
    {
        float inten = GetComponent<Light>().intensity;
        float num = Random.Range(-range, range);
        inten += num;
        if (inten > max)
        {
            inten = max;

        }
        else if (inten < min)
        {
            inten = min;
        }
        GetComponent<Light>().intensity = inten;

    }
}

