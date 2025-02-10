using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private int waveNumber = 0;
    private float spawnRange = 9f;
    [SerializeField]
    private int bossRound = 4;
    private MainManager mainManager;
    private Coroutine waitAndSpawnRoutine;

    public GameObject[] enemyPrefabs;
    public GameObject[] collectiblePrefabs;
    public GameObject bossPrefab;


    // Start is called before the first frame update
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        SpawnCollectible();
        SpawnEnemyWave((waveNumber / 4) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainManager == null || !MainManager.isGameRunning)
        {
            return;
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            mainManager.AddScore(10);
            if (waitAndSpawnRoutine != null)
            {
                StopCoroutine(waitAndSpawnRoutine);
                waitAndSpawnRoutine = null;
            }
            DestroyAllCollectibles();

            waveNumber += 1;

            if (waveNumber % bossRound == 0)
            {
                SpawnEnemyWave((waveNumber / 4));
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave((waveNumber / 4) + 1);
            }
        }
    }

    /// <summary>
    /// Spawns wave of enemies on the scene in random positions.
    /// </summary>
    /// <param name="enemiesToSpawn">Number of enemies to be spawned.</param>
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPoint(), enemyPrefabs[randomEnemy].transform.rotation, transform);
        }
    }

    /// <summary>
    /// Spawns boss that does scary things.
    /// </summary>
    /// <param name="currentRound">Current round.</param>
    private void SpawnBossWave(int currentRound)
    {
        GameObject boss = Instantiate(bossPrefab,
            GenerateSpawnPoint(),
            bossPrefab.transform.rotation);
    }


    /// <summary>
    /// Spawns powerup in random position.
    /// </summary>
    private void SpawnCollectible()
    {
        int randomPowerup = Random.Range(0, collectiblePrefabs.Length);
        Instantiate(collectiblePrefabs[randomPowerup],
            GenerateSpawnPoint(),
            collectiblePrefabs[randomPowerup].transform.rotation, transform);
    }

    public void OnResetPowerup()
    {
        if (waitAndSpawnRoutine != null)
        {
            StopCoroutine(waitAndSpawnRoutine);     
        }
        waitAndSpawnRoutine = StartCoroutine(WaitAndSpawnCollectibleRoutine());
    }

    private IEnumerator WaitAndSpawnCollectibleRoutine()
    {
        yield return new WaitForSeconds(5f);
        SpawnCollectible();
        waitAndSpawnRoutine = null;
    }

    /// <summary>
    /// Destroys existing powerups from scene.
    /// </summary>
    private void DestroyAllCollectibles()
    {       
        Collectible[] collectibles = FindObjectsOfType<Collectible>();
        for (int i = 0; i < collectibles.Length; i++)
        {
            Destroy(collectibles[i].gameObject);
        }
    }

    /// <summary>
    /// Generates spawn point of an enemy object on the scene.
    /// </summary>
    private Vector3 GenerateSpawnPoint()
    {
        //ABSTRACTION
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0f, spawnPosZ);
        return randomPos;
    }
}
