using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;


    public int numRooms;
    public float roomWidth;


    int randTreasure;

    private List<GameObject> rooms = new List<GameObject>();
    private List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private System.Random rand = new System.Random();


    public void Start()
    {
        // Generate the dungeon
        generateDungeon();

        // Move room so that the floor is at Y-0
        //transform.localPosition = new Vector3(0f, 3.45f, 0f);

        foreach (GameObject room in rooms)
        {
            EnemySpawner enemySpawner = room.GetComponentInChildren<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.spawnEnemies();
            }

            TreasureRoom treasureRoom = room.GetComponentInChildren<TreasureRoom>();
            if (treasureRoom != null)
            {
            }

            BossRoom bossRoom = room.GetComponentInChildren<BossRoom>();
            if (bossRoom != null)
            {
            }
        }
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(10);

    }

    private void generateDungeon()
    {
        // Add first room
        GameObject startingRoom = Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.identity);
        startingRoom.transform.parent = transform;
        rooms.Add(startingRoom);
        randTreasure = Random.Range(1, numRooms - 1);

        // Add remaining rooms
        for (int i = 1; i < numRooms; i++)
        {
            GameObject newRoom;

            if (i == numRooms - 1)
            {
                // This is the last room, so spawn the boss room with only one connection
                Vector3 direction = directions[rand.Next(0, directions.Count)];
                Vector3 newPosition = rooms[i - 1].transform.position + direction * roomWidth;
                newRoom = Instantiate(roomPrefabs[3], newPosition, Quaternion.identity);
                
                newRoom.transform.parent = transform;

                // Connect the boss room to the previous room
                connectRooms(rooms[i - 1], newRoom);
            }
            else
            {
                if (i == randTreasure)
                {
                    newRoom = Instantiate(roomPrefabs[2], Vector3.zero, Quaternion.identity);
                }
                else
                {
                    newRoom = Instantiate(roomPrefabs[1], Vector3.zero, Quaternion.identity);
                }
                newRoom.transform.parent = transform;

                bool roomPlaced = false;
                int numTries = 0;

                while (!roomPlaced && numTries < 100)
                {
                    // Choose a random direction
                    Vector3 direction = directions[rand.Next(0, directions.Count)];

                    // Calculate the position for the new room
                    Vector3 newPosition = rooms[i - 1].transform.position + direction * roomWidth;

                    // Check if there is already a room in that position
                    bool roomExists = false;
                    foreach (GameObject room in rooms)
                    {
                        if (Vector3.Distance(room.transform.position, newPosition) < roomWidth)
                        {
                            roomExists = true;
                            break;
                        }
                    }

                    // If there is no room in that position, place the new room and connect it to the previous room
                    if (!roomExists)
                    {
                        newRoom.transform.position = newPosition;
                        connectRooms(rooms[i - 1], newRoom);
                        roomPlaced = true;
                    }

                    numTries++;
                }

                if (!roomPlaced)
                {
                    Debug.LogError("Could not place room " + i);
                    Destroy(newRoom);
                }
            }

            rooms.Add(newRoom);
        }

        transform.position = new Vector3(0f, 3.45f, 0f);
    }

    private void connectRooms(GameObject roomA, GameObject roomB)
    {
        NavMeshLink link = roomA.GetComponent<NavMeshLink>();
        if (link == null)
        {
            link = roomA.AddComponent<NavMeshLink>();
        }
        link.endPoint = roomB.transform.position;

        link = roomB.GetComponent<NavMeshLink>();
        if (link == null)
        {
            link = roomB.AddComponent<NavMeshLink>();
        }
        link.startPoint = roomA.transform.position;
    }

    public void LoadNewDungeon()
    {
        // Destroy the current dungeon
        foreach (GameObject room in rooms)
        {
            Destroy(room);
        }
        rooms.Clear();

        // Generate a new dungeon
        generateDungeon();
    }

}
