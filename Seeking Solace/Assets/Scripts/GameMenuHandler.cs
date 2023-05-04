using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuHandler : MonoBehaviour
{

    public AudioSource audioSource;

    public GameObject playerObj;

    public GameObject gameOverScreen;

    public GameObject levelCompleteScreen;

    public GameObject bossRoom;

    BossRoom room;

    Character character;

    public void Start()
    {
    }

    void Update()
    {
    }

    public void GameOver()
    {
        audioSource.Play();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void levelComplete()
    {
        
    }

}
