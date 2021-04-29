using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    public float hoursInClock;
    const float REAL_SECONDS_PER_INGAME_DAY = 60f;
    
    private LighitngControl lightController;
    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private float day;

    private void Start()
    {
        clockHourHandTransform = transform.Find("HourHand");
        clockMinuteHandTransform = transform.Find("MinuteHand");
        lightController = FindObjectOfType<LighitngControl>();
        UpdateClock();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateClock();
    }

    void UpdateClock()
    {
        var currentTime = lightController.CurrentHour();
        // Normalize [0, 12] then find angle by 360 / hours in clock 
        float currentHourAngle = ((currentTime % hoursInClock) * 360 / hoursInClock);
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, -currentHourAngle);
    }
}
