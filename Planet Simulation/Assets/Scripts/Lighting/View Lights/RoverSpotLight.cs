using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSpotLight : MonoBehaviour
{
    public bool leftLightOn;
    public bool rightLightOn;

    public Light[] spotlight;

    void Start()
    {
        spotlight[0].enabled = leftLightOn;
        spotlight[1].enabled = rightLightOn;
    }

    void Update()
    {
        // Turn on the lights based on the key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftLightOn = !leftLightOn;
            spotlight[0].enabled = leftLightOn;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rightLightOn = !rightLightOn;
            spotlight[1].enabled = rightLightOn;
        }
    }
}
