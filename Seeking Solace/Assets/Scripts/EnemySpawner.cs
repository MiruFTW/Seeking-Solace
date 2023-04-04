using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int minEnemy;
    public int maxEnemy;

    public int spawnRadius;

    public void spawnEnemies()
    {
        // Choose a random number of enemies to spawn
        int numEnemies = Random.Range(minEnemy, maxEnemy);

        for (int i = 0; i < numEnemies; i++)
        {
            // Choose a random enemy prefab to spawn

            // Choose a random position within the spawn radius
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
            spawnPosition.y = enemyPrefab.transform.position.y;

            spawnPosition.x += 1.2f;
            spawnPosition.z += 2f;

            // Spawn the enemy and parent it to this transform
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = transform;
        }
    }
}