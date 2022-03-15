using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{ 
    public GameObject dropPassengerUI;
    private bool isCounting = false;
    //public static bool missionCompleted = false;
    private void OnTriggerEnter(Collider other)
    {
        isCounting = true;
        StartCoroutine(Counter());   
    }
    private void OnTriggerExit(Collider other)
    {
        isCounting = false;
        dropPassengerUI.SetActive(false);
    }
    int indexer;
    IEnumerator Counter()
    {
        indexer = 0;
        for(; indexer < 6; )
        {
            if (!isCounting)
            {
                indexer = 0;
                yield break; ;   
            }
            else
            {
                indexer++;
                if (indexer == 5)
                {
                    dropPassengerUI.SetActive(true);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
