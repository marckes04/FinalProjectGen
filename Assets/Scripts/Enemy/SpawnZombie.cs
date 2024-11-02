using System.Collections;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public static SpawnZombie instance;
    public GameObject enemyPrefab; // Prefab of the enemy to be spawned
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 2f; // Time between spawns in seconds
    private int enemyCount = 0; // Counter to track how many enemies have been spawned
    public int maxEnemies = 1; // Maximum number of enemies to spawn

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Start initial spawning if necessary
        EnemyGeneration();
    }

    public void EnemyGeneration()
    {
        // Start the repeated spawning of enemies
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyCount >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Spawn the enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Increment the counter
        enemyCount++;
    }

    public void OnEnemyKilled()
    {
        // Decrease the enemy count and spawn a new enemy if necessary
        enemyCount--;

        if (enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}
