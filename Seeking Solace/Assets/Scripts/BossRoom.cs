using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    public bool bossRoomCompleted = false;
    public int enemiesLeft;

    public GameObject levelScreen;

    public GameObject levelCompleteScreen;

    public List<GameObject> enemies = new List<GameObject>();

    Room room;


    private void Start()
    {
        room = GetComponent<Room>();
    }

    private void Update()
    {
        if (room.roomCompleted == true)
        {
            if (bossRoomCompleted == false)
            {
                Time.timeScale = 0f;
                levelCompleteScreen.SetActive(true);
                bossRoomCompleted = true;
            }

        }
    }

}