using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCarControllers : MonoBehaviour
{
    
    public static string car = "Car";
    public int carMin, carMax;
    Rigidbody rb;
    [SerializeField]
    Vector3 tVelocity;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(carMin , carMax);
        rb = GetComponent<Rigidbody>();
    }
    internal float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(car))
        {
            if (Manager.isHoldingPassenger)
                Manager.comfort -= 20;
            Manager.durability -= 30;
            if (Manager.durability < 0)
                Manager.durability = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag(car) && !other.isTrigger) {
            
            speed = Mathf.MoveTowards(
                speed,
                // if = 0 then stop else slow down
                other.GetComponent<TaxiController>().speed == 0 ? 
                    0f : other.GetComponent<TaxiController>().speed - 10,
                170 * Time.deltaTime);
        }
        else if( other.CompareTag(SpawnOtherCars.fcars)&& !other.isTrigger
            || other.CompareTag(SpawnOtherCars.bcars) && !other.isTrigger)
        {
            speed = Mathf.MoveTowards(
                speed,
                // if = 0 then stop else slow down
                other.GetComponent<OtherCarControllers>().speed == 0 ? 
                    0f : other.GetComponent<OtherCarControllers>().speed -10,
                170 * Time.deltaTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(SpawnOtherCars.fcars) && !other.isTrigger
            || other.CompareTag(SpawnOtherCars.bcars) && !other.isTrigger)
        {
            speed = Random.Range(carMin, 80);
        }
        else if(other.CompareTag(car) && !other.isTrigger)
        {
            speed = Random.Range(carMin, 80f);
        }
    }
    // Update is called once per frame
    void Update()
    {   //if game started
        if (Manager.isGameStarted)
        {
            tVelocity = new Vector3(rb.velocity.x, 0, speed);
            rb.velocity = tVelocity;
        }
    }
}
