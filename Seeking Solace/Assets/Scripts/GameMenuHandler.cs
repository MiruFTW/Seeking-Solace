using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuHandler : MonoBehaviour
{

    public AudioSource audioSource;

    public GameObject playerObj;

    public GameObject gameOverScreen;

    Character character;

    public void GameOver()
    {
        audioSource.Play();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}
