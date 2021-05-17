using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighitngControl : MonoBehaviour
{
    public Material skybox;
    
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
        manager = GameObject.Find("Audio Manager").GetComponent<AudioManager>(); // FindObjectOfType<AudioManager>();
        HandleDayMusic();
        // If it's night time we need to make sure the intensity starts at 0 and later updated

        UpdateSkybox();
    }
    // Update is called once per frame
    void Update()
    {
        currentRotation +=  speedofDay * degreesPerHour * Time.deltaTime;
        currentRotation %= 360.0f;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
        HandleDayMusic();
        //HandleSunIntensity();

        UpdateSkybox();
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
    public void ChangeHour(bool dayTime)
    {
        var newHour = dayTime ? 12 : 0;
        var newRotation = (newHour * degreesPerHour + 270.0f) % 360.0f;

        currentRotation = newRotation;
        transform.localRotation = Quaternion.Euler(currentRotation, 0, 0);
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
                manager.Stop("night_music");
                manager.Play("day_music", true);
                nightLocked = !nightLocked;
                dayLocked = !dayLocked;
            }
        }
        else
        {
            if (!nightLocked)
            {
                manager.Stop("day_music");
                manager.Play("night_music", true);
                dayLocked = !dayLocked;
                nightLocked = !nightLocked;
            }
        }
    }

    private void UpdateSkybox()
    {
        var currentHour = CurrentHour();
        float lightDimTime = (hoursPerDay / 4) * 2.5f;
        float skyboxBlendFactor = 0;

        if (currentHour >= dayTimeStart && currentHour <= lightDimTime)
        {
            skyboxBlendFactor = 1 / (lightDimTime - dayTimeStart);
            skyboxBlendFactor = skyboxBlendFactor * (currentHour - dayTimeStart);
        }
        if (currentHour <= nightTimeStart && currentHour >= lightDimTime)
        {
            skyboxBlendFactor = 1 / (nightTimeStart - lightDimTime);
            skyboxBlendFactor = skyboxBlendFactor * (nightTimeStart - currentHour);
        }

        skybox.SetFloat("_Blend", skyboxBlendFactor);
    }
}
