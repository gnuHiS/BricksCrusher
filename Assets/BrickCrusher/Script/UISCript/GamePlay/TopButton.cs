using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopButton : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField]
    GameObject pauseCanvas;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        gameIsPaused = true;
    }
    public void PauseGameButton()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        gameIsPaused = true;
        pauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameIsPaused = false;
        pauseCanvas.SetActive(false);
    }
}
