using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSpotLight : MonoBehaviour
{
    public bool lightOn;
    public Light spotlight;

    void Start()
    {
        spotlight.enabled = lightOn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightOn = !lightOn;
            spotlight.enabled = lightOn;
        }
    }
}
