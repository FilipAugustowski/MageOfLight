using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminatedObject : MonoBehaviour
{
    private Transform startMarker;
    public Vector3 endMarker = new Vector3(0, 0, 0);
    public bool Regrow= false;
    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public bool needsTravelParticles = true;

    void Start()
    {
        startMarker = transform;
        // Keep a note of the time the movement started.
        startTime = Time.time;
        endMarker = new Vector3(startMarker.position.x, endMarker.y, startMarker.position.z);
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker);
    }

    // Move to the target end position.
    void FixedUpdate()
    {
        if (Regrow == true)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker, fractionOfJourney);

            if(fractionOfJourney >= .98)
            {
                Regrow = false;
            }
        }
       
    }
}

