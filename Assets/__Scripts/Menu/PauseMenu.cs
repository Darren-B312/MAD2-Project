﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuPanel;

    void Update()
    {
        // if player hits escape -> show pause menu, escape again, hide pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindObjectOfType<GameController>().GetComponent<AudioSource>().Stop();
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // stop time when pause menu is active
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FindObjectOfType<GameController>().GetComponent<AudioSource>().Play();
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
