using UnityEngine;
using UnityEngine.UI;

public class TimeOfDayChange : MonoBehaviour
{
    public Dropdown timeOfDay;
    public LighitngControl lightingControl;
    void Start()
    {
        lightingControl = GameObject.Find("Lighting").GetComponent<LighitngControl>();
    }
    public void ChangeTime()
    {
        if (timeOfDay.value == 0) lightingControl.ChangeHour(true);
        else lightingControl.ChangeHour(false);
    }
}
