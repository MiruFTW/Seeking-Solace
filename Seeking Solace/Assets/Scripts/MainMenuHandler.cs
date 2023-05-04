using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{

    public GameObject panel;

    public void StartGame()
    {
        panel.SetActive(true);
        StartCoroutine(wait());
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

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Game");
    }
}
