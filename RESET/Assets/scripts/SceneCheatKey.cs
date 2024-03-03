using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneCheatKey : MonoBehaviour
{
    // Initialize the list of scene names to cycle through with "Level1", "Level2", and "Level3".
    public List<string> scenes = new List<string> { "Level1", "Level2", "Level3" };
    // Tracking the current index of the scene list.
    private int currentIndex = 0;

    void Update()
    {
        // When the 'S' key is pressed, load the next scene in the list.
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Load the next scene.
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Increment the current index and wrap around if it exceeds the list count.
        currentIndex = (currentIndex + 1) % scenes.Count;

        // Load the scene at the new index.
        SceneManager.LoadScene(scenes[currentIndex]);
    }
}