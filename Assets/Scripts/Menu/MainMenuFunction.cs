using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        // Load the instructions scene
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        // Load the credits scene
        SceneManager.LoadScene(3);
    }
    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
