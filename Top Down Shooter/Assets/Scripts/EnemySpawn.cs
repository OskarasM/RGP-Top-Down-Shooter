using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject spawnArea;

    public float spawnRadius = 4f;
    public float safeDistance = 0.5f;
    public float spawnChance = 1.0f;
    public float enemySpawnChance =  0.5f;
    public int maxEnemies = 10;

    private int numEnemies = 0;
    private GameObject character;
    private Vector2 playerPosition;

    void Start()
    {
        spawnArea = GameObject.FindWithTag("SpawnArea");
        character = GameObject.FindWithTag("Player");
        enemyPrefab1 = GameObject.FindWithTag("Enemy1");
        enemyPrefab2 = GameObject.FindWithTag("Enemy2");
    }
    void Update()
    {
        if (numEnemies < maxEnemies && Random.value < spawnChance)
        { // Only spawn enemies if we haven't reached the maximum number yet
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        // Get the bounds of the spawn area game object
        Bounds spawnBounds = spawnArea.GetComponent<SpriteRenderer>().bounds;

        // Generate random x and y coordinates within the bounds of a subset of the spawn area
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float y = Random.Range(spawnBounds.min.y, spawnBounds.max.y);

        // Create a Vector3 with the generated x and y coordinates, and a z-coordinate of 0
        Vector3 spawnPosition = new Vector3(x, y, 0);

        // Check if the generated position is too close to the player
        while (Vector3.Distance(spawnPosition, character.transform.position) < safeDistance)
        {
            // If it is, regenerate the position until it's far enough from the player
            x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
            y = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
            spawnPosition = new Vector3(x, y, 0);
        }

        GameObject newEnemy;
        if (Random.value < enemySpawnChance)
        {
            newEnemy = Instantiate(enemyPrefab1, spawnPosition, Quaternion.identity);
        }
        else
        {
            newEnemy = Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
        }
        numEnemies++;
    }
}
