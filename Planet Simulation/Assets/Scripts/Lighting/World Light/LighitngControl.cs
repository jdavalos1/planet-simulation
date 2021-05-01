using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighitngControl : MonoBehaviour
{

    [Min(0.0f)]
    public float startTime;
    [Min(0.0f)]
    public float hoursPerDay;
    [Min(0.0f)]
    public float speedofDay;

    //public Direction sunrise;
    private float degreesPerHour;
    private float currentRotation;
    private GameObject sunLight;
    private GameObject nightLight;
    private AudioManager manager;

    void Awake()
    {
        manager = FindObjectOfType<AudioManager>();
        degreesPerHour = 360 / hoursPerDay;

        // 90 deg -> midday; 270 deg -> midnight then mod to get excess degs
        currentRotation = (startTime * degreesPerHour + 270.0f) % 360.0f;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);

        sunLight = GameObject.Find("Lighting/Day Light");
        nightLight = GameObject.Find("Lighting/Night Light");
        HandleDayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation +=  speedofDay * degreesPerHour * Time.deltaTime;
        currentRotation %= 360.0f;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
        HandleDayMusic();
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

    private void HandleDayMusic()
    {
        // 
        if (transform.eulerAngles.x > 170f && transform.eulerAngles.x < 345f)
        {
            manager.Stop("noise_day");
            manager.Play("noise_night", true);
        }
        else
        {
            manager.Stop("noise_night");
            manager.Play("noise_day", true);
        }

/*        if (sunLight.activeSelf)
        {
            if (transform.eulerAngles.x > 170f && transform.eulerAngles.x < 345f)
            {
                sunLight.SetActive(false);
                nightLight.SetActive(true);
                manager.Stop("noise_day");
                manager.Play("noise_night", true);
            }
        }
        else
        {
            if(transform.eulerAngles.x <= 170f || transform.eulerAngles.x >= 345f)
            {
                sunLight.SetActive(true);
                nightLight.SetActive(false);
                manager.Stop("noise_night");
                manager.Play("noise_day", true);
            }
        }
*/    }
}


[SerializeField]
public enum Direction
{
    North,
    South,
    East,
    West,
    None
}
