using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;


    public int numRooms;
    public float roomWidth;

    private List<GameObject> rooms = new List<GameObject>();
    private List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private System.Random rand = new System.Random();


    private void Start()
    {
        // Generate the dungeon
        generateDungeon();

        // Set the scale of the parent object to transform the rooms to a 16:9 aspect ratio
        //transform.localScale = new Vector3(1.6f, 1f, 0.9f);
        transform.localPosition = new Vector3(0f, 3.45f, 0f);

        foreach (GameObject room in rooms)
        {
            EnemySpawner enemySpawner = room.GetComponentInChildren<EnemySpawner>();
            if (enemySpawner != null)
            {
                enemySpawner.spawnEnemies();
            }
        }

        //var enemyScript = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;

        //enemyScript.spawnEnemies();


    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(10);

    }

    private void generateDungeon()
    {
        // Add first room
        GameObject firstRoom = Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.identity);
        firstRoom.transform.parent = transform;
        rooms.Add(firstRoom);

        // Add remaining rooms
        for (int i = 1; i < numRooms; i++)
        {
            GameObject newRoom = Instantiate(roomPrefabs[rand.Next(1, roomPrefabs.Length)], Vector3.zero, Quaternion.identity);
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
                    roomPlaced = true;
                }

                numTries++;
            }

            if (!roomPlaced)
            {
                Debug.LogError("Could not place room " + i);
                Destroy(newRoom);
            }
            else
            {
                rooms.Add(newRoom);
            }
        }
    }
}
