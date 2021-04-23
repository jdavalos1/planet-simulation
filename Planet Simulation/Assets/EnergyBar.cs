using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Slider energySlider;

    void Update()
    {
        energySlider.value += -.6f;
    }

}
