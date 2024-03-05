//Inspo from Brackeys https://www.youtube.com/watch?v=JivuXdrIHK0&t=31s&ab_channel=Brackeys 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject pauseMenu;
    public GameObject crosshair;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Paused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        Debug.Log("pressed");
        crosshair.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause() {
        crosshair.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ToMenu() {
        SceneManager.LoadScene("HomeScreen");
    }

    public void hey() {
        Debug.Log("hey");
    }

    public void Quit() {
        Application.Quit();
    }
}
