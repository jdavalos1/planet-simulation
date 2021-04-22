using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        foreach(var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name, bool loop = false)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Audio Manager: Cannot find " + name + " as sound.");
            return;
        }

        s.source.loop = loop;
        s.source.Play();
    }

    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Audio Manager: Cannot find " + name + " as sound.");
            return;
        }

        s.source.Stop();
    }

    public void ChangeVolume(float volume)
    {
        foreach(var s in sounds)
        {
            s.volume = volume;
        }
    }
}
