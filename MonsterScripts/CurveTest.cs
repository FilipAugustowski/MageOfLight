using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTest : MonoBehaviour
{
    EC_EnemyVitals vitals;
    public GameObject target;
    public GameObject spawn;
    public float height = 10f;
    public float width = -0.5f;

    Vector3 origin;
    Vector3 dest;
    public GameObject fireball;
    // Start is called before the first frame update
    void Start()
    {
        origin = spawn.transform.position;
        dest = target.transform.position;
        vitals = GetComponent<EC_EnemyVitals>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void Awake()
    {
        StartCoroutine("LaunchFireball");
    }
    IEnumerator LaunchFireball()
    {
        while (true)
        {
            GameObject rangedAttack = Instantiate(fireball, origin, Quaternion.identity);
            rangedAttack.GetComponent<MonsterERange>().BeginTravel(origin, dest, height,vitals);
            yield return new WaitForSeconds(5f);

        }
    }
}
