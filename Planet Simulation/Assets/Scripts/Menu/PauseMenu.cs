using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject[] inGameUIs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Array.ForEach(inGameUIs, go => go.SetActive(true));
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        Array.ForEach(inGameUIs, go => go.SetActive(false));
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Quit to Main Menu!");
    }
}