using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighitngControl : MonoBehaviour
{
    // Day Attributes
    [Min(0.0f)]
    public float startTime;
    [Min(0.0f)]
    public float hoursPerDay;
    [Min(0.0f)]
    public float speedofDay;
    public Light sunlight;

    // Variables to declare time starts
    [Min(0)]
    public int dayTimeStart;
    [Min(0)]
    public int nightTimeStart;

    private float degreesPerHour;
    private float currentRotation;
    private AudioManager manager;
    private bool dayLocked;
    private bool nightLocked;

    void Awake()
    {
        degreesPerHour = 360 / hoursPerDay;
        // 90 deg -> midday; 270 deg -> midnight then mod to get excess degs
        currentRotation = (startTime * degreesPerHour + 270.0f) % 360.0f;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
    }
    void Start()
    {
        var currentHour = CurrentHour();
        dayLocked = !(currentHour >= dayTimeStart && currentHour < nightTimeStart);
        nightLocked = !dayLocked;
        manager = FindObjectOfType<AudioManager>();
        HandleDayMusic();
    }
    // Update is called once per frame
    void Update()
    {
        currentRotation +=  speedofDay * degreesPerHour * Time.deltaTime;
        currentRotation %= 360.0f;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
        HandleDayMusic();
        HandleSunIntensity();
    }
    /**
     * Return the current time in relation to the world time
     */
    public float CurrentHour()
    {
        // 90 deg -> midday; 270 -> deg
        var dayAngle = (currentRotation + 90) % 360;
        return (dayAngle / 360) * hoursPerDay;
    }

    /**
     * Handle the music during the night and day
     */
    private void HandleDayMusic()
    {
        var currentHour = (int)CurrentHour();
        // Check night of day time and lock the current song being played
        if(currentHour >= dayTimeStart && currentHour <= nightTimeStart)
        {
            if (!dayLocked)
            {
                manager.Stop("noise_night");
                manager.Play("noise_day", true);
                nightLocked = !nightLocked;
                dayLocked = !dayLocked;
            }
        }
        else
        {
            if (!nightLocked)
            {
                manager.Stop("noise_day");
                manager.Play("noise_night", true);
                dayLocked = !dayLocked;
                nightLocked = !nightLocked;
            }
        }
    }
    private void HandleSunIntensity()
    {
        var currentHour = CurrentHour();
        var midday = hoursPerDay / 2;

        // If daytime is increasing increase intensity
        // else decrease it
        if (currentHour >= dayTimeStart && currentHour < midday)
        {
            Debug.Log("Day Time");
            // Obtain the hourly increase
            var hourIntInc = 1 / (midday - dayTimeStart);

            // Change the intensity based on curent hour (inc)
            sunlight.intensity = (currentHour - dayTimeStart) * hourIntInc;
        }
        else if(currentHour >= hoursPerDay / 2 && currentHour < nightTimeStart)
        {
            // Obtain hourly decrease
            var hourIntDec = 1 / (nightTimeStart - midday);
            // Change intensity based on current hour (dec)
            sunlight.intensity = (nightTimeStart - currentHour) * hourIntDec;
        }
    }
}
