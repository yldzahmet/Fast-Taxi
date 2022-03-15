using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource loop;
    public AudioSource engineSound;

    internal float speed;
    internal float pitch;
    // Start is called before the first frame update
    TaxiController taxicontroller;
    void Start()
    {
        taxicontroller = GetComponent<TaxiController>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = taxicontroller.speed;
        pitch = speed / 45f;

        if (speed == 0)
            loop.mute = true;
        else
            loop.mute = false;

        if (pitch <= 0.3f)
            loop.pitch = .3f;
        else
            loop.pitch = pitch;
    }
}
