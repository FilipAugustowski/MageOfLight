using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingParticle : MonoBehaviour
{
    float startTime;
    public float speed = 3.0f;
    private float journeyLength;
    Vector3 startPos;
    Vector3 endPos;
    float minDistance = 0.3f;
    float collisionFadeOut = 1.5f;
    public GameObject collision;
    
    void OnEnable()
    {
        collision.SetActive(false);
        startPos = transform.position;
        collision.GetComponent<RFX4_EffectSettings>().FadeoutTime = collisionFadeOut;
    }

    public void Init(Vector3 _endPos, int _monsterID)
    {
        journeyLength = Vector3.Distance(startPos, _endPos) - 0.5f;
        endPos = _endPos;

        StartCoroutine(TravelToObject(_monsterID));
    }

    IEnumerator TravelToObject(int _monsterID)
    {
        startTime = Time.time;
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;
        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        while (Vector3.Distance(this.transform.position, endPos) > minDistance)
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;
            yield return null;
        }

        /* Set the explosion active and destroy it after fade out */
        if(collision) collision.SetActive(true);
        /* Tell lighting manager to activate object */
        LightManager.Instance.IlluminateFromParticle(_monsterID);

        Invoke(nameof(Die), collisionFadeOut);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
