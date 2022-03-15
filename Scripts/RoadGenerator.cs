using System.Collections;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    public GameObject road;
    public GameObject[] houses;
    public GameObject[] trees;
    
    public GameObject citizenAsset;
    private GameObject citizen;
    public Sprite[] sreCitizen;
    internal bool roadCreated = false;

    

    internal static bool passengerCreatedSoon = false;
    IEnumerator PassengerCreated()
    {
        passengerCreatedSoon = true;
        yield return new WaitForSecondsRealtime(5);
        passengerCreatedSoon = false;
    }
    private void OnEnable()
    {
        if (Random.Range(0, 100) < 35)
        {
            if (!Manager.isHoldingPassenger && !passengerCreatedSoon)
            {
                StartCoroutine(PassengerCreated());
                CreatePassenger();
            }
        }
    }

    public void CreatePassenger()
    {
        citizen = Instantiate(citizenAsset, new Vector3(44, 6, transform.position.z), Quaternion.Euler(90, 0, 0));
        citizen.tag = "citizen";
        citizen.GetComponent<SpriteRenderer>().sprite = sreCitizen[Random.Range(0, 4)];
    }


    public void CreateHouse()
    {
        if(Random.Range(0, 2) < 1)
        {
            int n = Random.Range(0, 2);
            if (n == 0)// if left side of screen , turn sprite for correct road side.
                Instantiate(houses[n], new Vector3(-115, 0, transform.position.z + Random.Range(-20, 20)), Quaternion.Euler(90, 180, 0));
            else
                Instantiate(houses[n], new Vector3(-115, 0, transform.position.z + Random.Range(-20, 20)), Quaternion.Euler(90, 0, 0));
        }
        else
        {
            CreateTrees(-150, -70);
        }
        if (Random.Range(0, 2) < 1)
        {
            Instantiate(houses[Random.Range(0, 2)],
                new Vector3(110, 0, transform.position.z + Random.Range(-20, 20)),
                Quaternion.Euler(90, 0, 0)
                );
        }
        else
        {
            CreateTrees(70, 150);
        }
    }
    
    public void CreateTrees(float xmin, float xmax)
    {
        float zpos = -80;
        print("ypos :" + zpos);
        for(int i = 0; i < 5;)
        {
            var xpos = Random.Range(xmin, xmax);
            if(Random.Range(0,2) == 0)
            {
                Instantiate(trees[Random.Range(0, 3)], new Vector3(xpos, 0, transform.position.z + zpos), Quaternion.Euler(90, 0, 0));
            }
            i++;
            zpos += 36;
        }
    }

    public void CreateRoad()
    {
        if (!roadCreated)
        {
            Instantiate(road, new Vector3(0,0, transform.position.z + 190),  Quaternion.Euler(90, 0, 0));
            CreateHouse();
            roadCreated = true;
        }
    }
}
