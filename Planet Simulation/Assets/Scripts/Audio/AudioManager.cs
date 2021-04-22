using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Need to loop the initial source
        Play("noise", true);
    }


    public void Play(string name, bool loop=false)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;

        s.source.loop = loop;
        s.source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
