using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_Select"); // Replace "GameScene" with the actual scene name for your game.
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options"); // Replace "OptionsScene" with the scene name for your options menu.
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits"); // Replace "CreditsScene" with the scene name for your credits menu.
    }

    public void lev1()
    {
        SceneManager.LoadScene("Level1"); // Replace "CreditsScene" with the scene name for your credits menu.
    }

    public void lev2()
    {
        SceneManager.LoadScene("Level2"); // Replace "CreditsScene" with the scene name for your credits menu.
    }

    public void lev3()
    {
        SceneManager.LoadScene("Level3"); // Replace "CreditsScene" with the scene name for your credits menu.
    }
}
