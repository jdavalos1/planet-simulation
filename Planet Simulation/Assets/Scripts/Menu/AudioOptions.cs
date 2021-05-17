using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    public AudioManager manager;
    public Slider volumeSlider;
    private bool audioOn;

    void Start()
    {
        audioOn = true;
    }

    public void ToggleAudio()
    {
        if (audioOn)
        {
            volumeSlider.value = 0.0f;
            manager.ChangeVolume(volumeSlider.value);
        }
        else
        {
            manager.ChangeVolume(volumeSlider.value);
        }
        
        audioOn = !audioOn;
    }
    
    public void ChangeVolume()
    {
        manager.ChangeVolume(volumeSlider.value);
    }
}
