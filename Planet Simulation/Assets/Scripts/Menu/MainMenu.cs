using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager Manager;

    void Awake()
    {
        Manager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        Manager.Play("intro");
    }
    public void Play ()
    {
        Manager.Stop("intro");
        SceneManager.LoadScene("terrainGeneration");
    }

    public void Quit ()
    {
        Debug.Log("Quit Successfully");
        Application.Quit();
    }
}
