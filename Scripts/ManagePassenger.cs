using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePassenger : MonoBehaviour
{
    public Destination destination;
    internal GameObject currentCitizen;
    public static string citMessage = "";
    internal static string badTaxi = "Bad Taxi !", thanks = "Thanks !";

    // Start is called before the first frame update
    public void SetUpCitizen()
    {
        if (currentCitizen)
        {
            //play get in anim
            currentCitizen.SetActive(false);
            currentCitizen.transform.SetParent(gameObject.transform);
            currentCitizen.tag = "current_citizen";
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("citizen"))
            {
                if (g.CompareTag("citizen"))
                    Destroy(g);
            }
        }
    }

    // called via button
    // needs to pass in message
    public void DropPassenger()
    {
        if (currentCitizen)
        {
            currentCitizen.transform.SetParent(null);
            currentCitizen.tag = "citizen";
            var t = currentCitizen.transform.GetChild(1).gameObject;
                t.transform.GetChild(0).GetComponent<Text>().text = citMessage;
                t.SetActive(true);

            currentCitizen.transform.GetChild(0).gameObject.SetActive(false);
            currentCitizen.SetActive(true);

            destination.gameObject.SetActive(false);
            //play drop anim
        }
    }
}
