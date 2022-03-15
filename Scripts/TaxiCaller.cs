using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiCaller : MonoBehaviour
{
    public GameObject textBubble;
    public GameObject acception_uý;
    public GameObject alertPanel;
    internal GameObject thisObject;
    public static bool rejected = false;
    public static bool accepted = false;
    Collider col;


    private void OnTriggerEnter(Collider collider)
    {
            if (collider.gameObject.name == "cars_0")
            {
                col = collider;
                rejected = false;
                StartCoroutine(WaitingTaxiStop());
                collider.GetComponent<ManagePassenger>().currentCitizen = transform.parent.gameObject;
                if (textBubble)
                    textBubble.SetActive(false);
                else
                {
                    textBubble = transform.parent.gameObject.transform.GetChild(1).gameObject;
                }
            }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "cars_0")
        {
            rejected = true;
            StopCoroutine(WaitingTaxiStop());
            if (acception_uý.activeSelf)
            {
                acception_uý.SetActive(false);
            }
            
            if(textBubble)
                textBubble.SetActive(true);
            else
            {
                textBubble = transform.parent.gameObject.transform.GetChild(1).gameObject;
            }
            if(collider)
                collider.GetComponent<ManagePassenger>().currentCitizen = null;
        }
    }

    private int indexer;
    internal IEnumerator WaitingTaxiStop()
    {
        indexer = 0;
        for(; indexer < 6 ;)
        {
            yield return new WaitForSeconds(0.5f);
            if (rejected)
            {
                yield break;
            }
            else
            {
                indexer++;
                if(indexer == 5)
                {
                    if(!Manager.alerted)
                        acception_uý.SetActive(true);
                    else
                    {
                        alertPanel.SetActive(true);
                    }
                }
            }
            
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
