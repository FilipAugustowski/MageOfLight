using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterERange : MonoBehaviour
{
    PC_PlayerVitals playerVitals;
    EC_EnemyVitals vitals;

    bool hitSomething = false;
    bool hasDestination = false;
    public Vector3 start;
    public Vector3 target;
    public Vector3 dir;
    float prevy = 0f;
    float timeTillDie = 3.0f;
    public float[] firstNums = new float[2];
    public float[] scdNums = new float[2];
    public Vector2 begin, end;

    int damage = 25;
    float hitForce = 6.0f;

    public float fireballSpeed = 10.0f;
    public float speed = 1f;
    public GameObject collisionEffect;
    public ParticleSystem[] nonCollisionEffects;
    public float height; /* represents "k" in parabola formula*/
    public float width; /* represents "a" in parabola */
    public float dist;
    public float x_cord;
    private float y1, y2;
    public Vector3 important;

    float spot = 0;
    float count = 0.0f;

    public Vector3 vertex;
    bool hasExploded = false;

    //public float 
    void OnEnable()
    {
        collisionEffect.SetActive(false);
        Invoke("Destroy", 5.0f);
        playerVitals = GameObject.FindGameObjectWithTag("Player").GetComponent<PC_PlayerVitals>();
    }

    /* Method override for randomized height and width */
    public void BeginTravel(Vector3 start, Vector3 target, float height, EC_EnemyVitals vitals)
    {
        this.vitals = vitals;
        this.start = start;
        this.target = target;
        vertex = start + (target - start) / 2 + Vector3.up * height;
        hasDestination = true;
    }

    /* Method override for set height and width*/
    public void BeginTravel(Vector3 start, Vector3 target, float k, float a)
    {
        this.start = start;
        this.target = target;
        //this.vitals = vitals;
        width = a;
        height = k;

        processVals();
        


        hasDestination = true;
    }

    void Update()
    {
        if (hasDestination)
        {
            //transform.position += SampleHeight();
            if (count < 1.0f)
            {
                count += speed * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(start, vertex, count);
                Vector3 m2 = Vector3.Lerp(vertex, target, count);
                transform.position = Vector3.Lerp(m1, m2, count);
            }
            else
            {
                foreach(ParticleSystem ps in nonCollisionEffects)
                {
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
                if (!hasExploded)
                {
                    collisionEffect.SetActive(true);
                    hasExploded = true;

                    float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

                    AOEDamage(playerVitals, distance);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!hasExploded)
            {
                float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);


                Debug.Log("On Trigger enter called AOE");
                AOEDamage(playerVitals, distance);

                //other.gameObject.GetComponent<HandleDamage>().TakeDamage(25, this.transform.position);
                collisionEffect.SetActive(true);
                TurnOffNonCollisionEffects();


            }
            hasDestination = false;
            hitSomething = true;

            transform.parent = other.gameObject.transform;

            //transform.position += destination * fireballSpeed * Time.deltaTime * 5;
            Invoke(nameof(Destroy), timeTillDie);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WorldObject"))
        {
            TurnOffNonCollisionEffects();
            collisionEffect.SetActive(true);
            Invoke(nameof(Destroy), timeTillDie);
        }
    }
    void AOEDamage(PC_PlayerVitals player, float dist)
    {
        float newdmg = 25f * Mathf.Clamp((5f - dist) / 5f, 0f, 1f);

        player.HandleDamage(newdmg, (int)hitForce, vitals, false);
        //player.TakeDamage((int)newdmg, this.transform.position);

    }


    void TurnOffNonCollisionEffects()
    {
        foreach (ParticleSystem particleSystem in nonCollisionEffects)
        {
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
    void processVals()
    {
        dist = Mathf.Sqrt(Mathf.Pow(start.x - target.x, 2) + Mathf.Pow(start.z - target.z, 2));
        dir = target - start;
        y1 = start.y;
        y2 = target.y;
        x_cord = CalculateH();



    }
    float CalculateH()
    {
        begin = new Vector2(0f, 0f);
        end = new Vector2(dist, dir.y);



        firstNums[0] = begin.x + Mathf.Sqrt((begin.y - height) / width);
        firstNums[1] = begin.x - Mathf.Sqrt((begin.y - height) / width);
        scdNums[0] = end.x + Mathf.Sqrt((end.y - height) / width);
        scdNums[1] = end.x - Mathf.Sqrt((end.y - height) / width);
        foreach(float num in firstNums)
        {
            if (scdNums.Contains(num))
            {
                return num;
            }
        }
        foreach(float num1 in scdNums)
        {
            if(num1 < dir.x)
            {
                return num1;
            }
        }
        return 0f;


    }
    Vector3 SampleHeight()
    {
        //Vector3 thing = transform.position;
        begin = new Vector2(0f, 0f);
        end = new Vector2(dist, dir.y);
        float newy = width * Mathf.Pow((spot - x_cord), 2) + height;
        newy -= prevy;
        prevy = newy;
        
        float newx = dir.x / dist;
        float newz = dir.z / dist;
        Vector3 nextVector = new Vector3(newx, newy, newz);
        important = nextVector;
        spot += fireballSpeed/1000f;
        return nextVector;






    }
    
}
