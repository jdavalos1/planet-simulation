using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play ()
    {
        SceneManager.LoadScene("terrainGeneration");
    }

    public void Quit ()
    {
        Debug.Log("Quit Successfully");
        Application.Quit();
    }
}
