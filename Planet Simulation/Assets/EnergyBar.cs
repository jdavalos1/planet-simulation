using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Slider energySlider;
    public float depletionRatio;

    void Update()
    {
        energySlider.value -= (depletionRatio * Time.deltaTime);
    }

}
