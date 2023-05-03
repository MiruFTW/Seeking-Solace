using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    GameDataHandler gdh;
    private void Start()
    {
        gdh = GetComponent<GameDataHandler>();
        DungeonGenerator dungeonGenerator = GetComponent<DungeonGenerator>();
        if (dungeonGenerator != null)
        {
            dungeonGenerator.Start();
        }
        
        gdh.LoadGameData();
    }

    private void OnDisable()
    {
        gdh.SaveGameData();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
