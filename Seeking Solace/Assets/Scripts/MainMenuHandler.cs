using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("Game Start");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
        Debug.Log("Settings Start");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Menu Start");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("Menu");
    }
}
