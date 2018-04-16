using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{
    PlayerProperties pp;
    public float minutesInDay = 1.0f;
    public int numberOfDays = 1;
    public float timer;
    float percentageOfDay;
    float turnSpeed;
    Spawner spawner;
    // Use this for initialization
    void Start()
    {
        pp = FindObjectOfType<PlayerProperties>();
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        checkTime(); //Function to update time of day
        if (timer == 0.0f) //If passed midnight
        {
            numberOfDays++; //Increase number of days
            pp.daysSurvived++;

        }
        UpdateLights(); //Change light intensity based on time of day
       
        turnSpeed = 360.0f / (minutesInDay * 60.0f) * Time.deltaTime; //Set rotation speed
        transform.RotateAround(transform.position, transform.right, turnSpeed); //Rotate on axis

    }

    void UpdateLights()
    {
        Light l = GetComponent<Light>();
        if (isNight())
        {
            if (l.intensity > 0.0f) //If light is on at night time
            {
                l.intensity -= 0.05f; //Gradually reduce light intensity to simulate a sunset
            }
        }
        else
        {
            if (l.intensity < 1.0f) //If light isn't fully switched on
            {
                l.intensity += 0.05f; //Gradually increase light intensity to simulate sunrise
            }
        }
    }
    public bool isNight()
    {
        bool c = false;
        if (percentageOfDay > 0.5f) //If more than half the day has passed, it is night time
        {
            c = true;
        }
        return c;
    }

    void checkTime()
    {
        timer += Time.deltaTime; //Increase time
        percentageOfDay = timer / (minutesInDay * 60.0f); //Set how long into the day it is
        if (timer > (minutesInDay * 60.0f)) //If timer is larger than how many minutes in a day
        {
            timer = 0.0f; //Reset timer
        }
    }
}
