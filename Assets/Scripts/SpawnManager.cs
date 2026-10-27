using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;

    private readonly float xEnemySpawn = 21.0f;
    private readonly float ySpawnRange = 6.5f;
    private readonly float xPowerupRange = 17.5f;
    private readonly float zSpawn = -0.55f;

    private readonly float powerUpSpawnTime = 10.0f;
    private readonly float enemySpawnTime = 3.0f;
    private readonly float startDelay = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomEnemy), startDelay, enemySpawnTime);
        InvokeRepeating(nameof(SpawnPowerup), startDelay, powerUpSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomEnemy()
    {
        if (UIManager.isGameActive)
        {
            float yRandom = Random.Range(-ySpawnRange, ySpawnRange);
            int randomIndex = Random.Range(0, enemies.Length);

            Vector3 spawnPos = new(xEnemySpawn, yRandom, zSpawn);
            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].transform.rotation);
        }

    }

    void SpawnPowerup()
    {
        if (UIManager.isGameActive)
        {
            float xRandom = Random.Range(-xPowerupRange, 0.0f);
            float yRandom = Random.Range(-ySpawnRange, ySpawnRange);

            Vector3 spawnPos = new(xRandom, yRandom, zSpawn);
            Instantiate(powerup, spawnPos, powerup.transform.rotation);
        }
    }
}
