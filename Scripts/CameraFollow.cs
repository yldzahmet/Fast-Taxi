using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    
    public GameObject taxi;
    Vector3 offsetVector;
    // Use this for initialization
    private void Start()
    {
        offsetVector = transform.position - taxi.transform.position;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(0, 50, taxi.transform.position.z + offsetVector.z);
    }
}
