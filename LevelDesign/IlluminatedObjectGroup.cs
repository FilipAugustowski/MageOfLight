using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminatedObjectGroup : MonoBehaviour
{
    /* There should be the same number of lights as monsters */
    /* STARTING LIGHT IS NOT INCLUDED HERE */

    public IlluminatedObject[] IlluminatedObjects;
    public int id;

    void Start()
    {
        

        IlluminatedObjects = GetComponentsInChildren<IlluminatedObject>();

        //foreach (IlluminatedObject obj in IlluminatedObjects)
        //{
        //        obj.gameObject.SetActive(false);
        //}
    }

    public void Illuminate()

    {
        foreach (IlluminatedObject obj in IlluminatedObjects)
        {
            
             //obj.gameObject.SetActive(true);
            obj.Regrow = true;

        }
    }
}