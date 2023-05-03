using UnityEngine;
using UnityEngine.UI;

public class GameDataHandler : MonoBehaviour
{
    public static GameDataHandler instance;

    public float playerHealth;
    public int playerDamage;
    public float playerRange;
    public int currentLevel;
    public Slider volumeSlider;



    Character player;

    DamageDealer damage;


    private void Awake()
    {
        // Check if an instance of this script already exists
        if (instance == null)
        {
            // If not, set this as the instance
            instance = this;

            // Make sure this game object persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this game object
            Destroy(gameObject);
        }

        GameObject playerObj = GameObject.Find("PlayerObj");
        player = playerObj.GetComponent<Character>();
        GameObject sword = GameObject.Find("sword_rare");
        damage = sword.GetComponentInChildren<DamageDealer>();

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0f);
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

    }

    public void SaveGameData()
    {
        // Save the game data using PlayerPrefs
        PlayerPrefs.SetFloat("PlayerHealth", player.health);
        PlayerPrefs.SetInt("PlayerDamage", damage.weaponDamage);
        PlayerPrefs.SetFloat("PlayerRange", damage.weaponLength);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    private void OnVolumeChanged(float volume)
    {
        // Save the volume value to PlayerPrefs
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        // Load the game data from PlayerPrefs
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth", player.health);
        playerDamage = PlayerPrefs.GetInt("PlayerDamage", damage.weaponDamage);
        playerRange = PlayerPrefs.GetFloat("PlayerRange", damage.weaponLength);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);

    }
}
