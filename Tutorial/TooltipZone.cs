using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipZone : MonoBehaviour
{
    public string toolTipMessage = "";

    public bool beenHit = false;
    public bool hasTask;
    public bool taskCompleted;
    public GameObject exitObject;

    void Start()
    {
        if(hasTask)
        {
            exitObject.SetActive(false);
            foreach (EasyGameStudio.Jeremy.Dissolve dissolve in exitObject.GetComponentsInChildren<EasyGameStudio.Jeremy.Dissolve>())
            {
                dissolve.hide();
            }

        }
    }

   void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.tag == "Player")
        {
            PC_UIManager.Instance.DisplayTooltip(toolTipMessage);
            beenHit = true;
        }
   }

   void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PC_UIManager.Instance.tooltipText.text == toolTipMessage) PC_UIManager.Instance.ClearTooltip();
            GetComponent<Collider>().enabled = false;
        }
    }

   void Update()
    {
        if (hasTask) if (taskCompleted) 
        {
            exitObject.SetActive(true);
            
            foreach(EasyGameStudio.Jeremy.Dissolve dissolve in exitObject.GetComponentsInChildren<EasyGameStudio.Jeremy.Dissolve>())
                {
                    dissolve.show();
                }

            if (PC_UIManager.Instance.tooltipText.text == toolTipMessage) PC_UIManager.Instance.ClearTooltip();
            gameObject.SetActive(false);
        }
    }


}
