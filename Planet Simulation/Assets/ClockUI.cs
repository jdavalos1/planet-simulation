using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [Min(0.0f)]
    public float hoursInClock;
    [Min(0.0f)]
    public float minsPerHour;

    private float degreesPerMin;
    private LighitngControl lightController;
    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;

    private void Start()
    {
        clockHourHandTransform = transform.Find("HourHand");
        clockMinuteHandTransform = transform.Find("MinuteHand");
        lightController = FindObjectOfType<LighitngControl>();
        degreesPerMin = 360 / minsPerHour;
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

        float currentMinuteAngle = (currentTime % 1.0f) * minsPerHour * degreesPerMin;
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -currentMinuteAngle);
    }
}
