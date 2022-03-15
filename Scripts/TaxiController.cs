using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaxiController : MonoBehaviour
{
    
    
    internal GameObject leftTier, rightTier;
    public Transform spawnArea;
    private Rigidbody rigidBody;
    internal Vector3 startPoint;
    private Vector3 lTierDegree;
    private Vector3 tVelocity;
    public LayerMask lmask;
    

    private void Start()
    {
        lTierDegree = new Vector3(0, 0, 36.5f);
        leftTier = transform.GetChild(0).gameObject;
        rightTier = transform.GetChild(1).gameObject;
        rigidBody = GetComponent<Rigidbody>();
    }

    internal static bool isAccerelating;
    internal float speed;
    internal float maxSpeed;
    internal float targetSteer;
    float multipler = 20f;
    readonly float divider = 0.4f;
    public float breakeThreshold;
    public float speedThreshold;
    float breakeAmount = 0;
    bool breaked = false;
    private bool atInfo;

    void MoveTaxi()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isAccerelating = true;
            maxSpeed = 150;
            multipler = 25 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isAccerelating = false;
            maxSpeed = speed;
            multipler = 5 * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            maxSpeed = -20;
            multipler = 20 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            maxSpeed = 0;
            multipler = 5 * Time.deltaTime;
        }


        if (( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ) && speed != 0)
        {
            targetSteer = speed < 0 ? speed * divider : speed * -divider;
            leftTier.transform.localEulerAngles = speed < 0 ? -lTierDegree : lTierDegree;
            rightTier.transform.localEulerAngles = speed < 0 ? -lTierDegree : lTierDegree;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftTier.transform.localEulerAngles = Vector3.zero;
            rightTier.transform.localEulerAngles = Vector3.zero;
            targetSteer = 0;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && speed!=0)
        {
            leftTier.transform.localEulerAngles = speed < 0 ? lTierDegree : -lTierDegree;
            rightTier.transform.localEulerAngles = speed < 0 ? lTierDegree : -lTierDegree;
            targetSteer = speed < 0 ? speed * -divider : speed * divider;

        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            leftTier.transform.localEulerAngles = Vector3.zero;
            rightTier.transform.localEulerAngles = Vector3.zero;
            targetSteer = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            breakeAmount = Mathf.Lerp(breakeAmount, 100, 2f*Time.deltaTime);
            if (breakeAmount > breakeThreshold && !breaked && Manager.isHoldingPassenger)
            {   
                // decrease comfor if pressed breake too long (hard press)
                Debug.LogWarning("Comfort decreased :");
                Manager.comfort -= 10;
                breaked = true;
            }
            maxSpeed = 0;
            multipler = 100 * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            breakeAmount = 0;
            maxSpeed = 0;
            multipler = 5 * Time.deltaTime;
            breaked = false;
        }

        speed = Mathf.MoveTowards(speed, maxSpeed, multipler);

        tVelocity = new Vector3(targetSteer, rigidBody.velocity.y, speed);
        rigidBody.velocity = tVelocity;
        spawnArea.transform.position = new Vector3(0, 0, transform.position.z);
    }

    private void Update()
    {
        
        MoveTaxi();
        RaycasterForRoad();
        if (speed > speedThreshold && Manager.isHoldingPassenger)
        {
            //decrease comfot every seconds above 100 km/h speed
            Manager.comfort -= 4*Time.deltaTime;
        }
    }
    void RaycasterForRoad()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, 500,lmask))
        {
            
            if (hit.collider.name == "ray_target")
            {
                hit.collider.GetComponentInParent<RoadGenerator>().CreateRoad();
            }
            Destroy(hit.collider);
            //Debug.DrawRay(transform.position, transform.position + hit.transform.position, Color.red);
        }

        


    }

}
