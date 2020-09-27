using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused;

    public GameObject pauseButton;

    public GameObject pauseCanvas;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        pauseButton.SetActive(true);
    }
}