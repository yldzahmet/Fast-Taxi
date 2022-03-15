using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOtherCars : MonoBehaviour
{
    public GameObject otherCar;
    public Sprite[] cars;
    public Vector3[] carPoints = new Vector3[8]; // probable spawn points
    private List<int> spawnPoints = new List<int>(); // spawn points uniqely generated
    internal static int frontCars = 0;
    internal static int  backCars = 0;
    internal static string frontFile = "front_spawn_area", backFile = "back_spawn_area"; // object names
    internal static string fcars = "fcars", bcars = "bcars";// car tags

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(fcars) && gameObject.name == frontFile )
        {
            Destroy(other.gameObject);
            frontCars -= 1;
        }
        else if (other.CompareTag(bcars) && gameObject.name == backFile)
        {
            Destroy(other.gameObject);
            backCars -= 1;
        }
    }

    /// <summary>
    /// Creates maximum 8 cars.
    /// </summary>
    /// <param name="number">Number of cars to instantiate.</param>
    public void CreateOtherCar(int number)
    {
        //number of cars that will spawn which its maximum number determined by parameter 
        var n = Random.Range(4, number);
        int p;
        //generate random points and save it fo list
        for (int i=0;i<n; i++)
        {
            do
            {
                p = Random.Range(0, 8);
            }
            while (spawnPoints.Contains(p));
            spawnPoints.Add(p);
        }
        for (int i = 0; i < n; i++)
        {
            //instantiate every object with random different sprites
            GameObject gobject =
            Instantiate(
            otherCar,
            transform.position + carPoints[spawnPoints[i]],
            Quaternion.Euler(90, 0, 0)
                );
            gobject.GetComponent<SpriteRenderer>().sprite = cars[Random.Range(0, 6)];
            // give a tag to car for its location
            if (gameObject.name == frontFile)
            {
                gobject.tag = "fcars";
                frontCars += 1;
            }
            else if (gameObject.name == backFile)
            {
                gobject.tag = "bcars";
                backCars += 1;
            }
        }   
        spawnPoints.Clear();
    }

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateOtherCarsEnum());
    }
    IEnumerator CreateOtherCarsEnum()
    {
        do
        {
            if (gameObject.name == frontFile && frontCars < 1 
                || gameObject.name == backFile && backCars < 1)
                CreateOtherCar(8);
            yield return new WaitForSecondsRealtime(1);
        }
        while (true);
    }
    // Update is called once per frame
    
}
