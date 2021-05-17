using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioInGame : MonoBehaviour
{
    public AudioManager[] managers;
    public Slider volumeSlider;

    public void ChangeVolume()
    {
        foreach(var manager in managers)
        {
            manager.ChangeVolume(volumeSlider.value);
        }
    }
}
