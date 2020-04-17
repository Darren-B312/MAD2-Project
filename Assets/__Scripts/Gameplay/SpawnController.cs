using System.Collections.Generic;
using UnityEngine;

// Manage waves/spawning of enemies
public class SpawnController : MonoBehaviour
{
    public delegate void EnemySpawned();
    public static event EnemySpawned EnemySpawnedEvent;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float spawnInterval = 1f;

    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;
    private const string SPAWN_ENEMY_METHOD = "SpawnEnemy";

    void Start()
    {
        enemyParent = GameObject.Find("Enemies");
        if (!enemyParent)
        {
            enemyParent = new GameObject("Enemies");
        }

        spawnPoints = GetComponentsInChildren<SpawnPoint>();

        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        EnableSpawning();
    }

    public void EnableSpawning()
    {
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }
    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }

    private void SpawnEnemy()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
        PublishEnemySpawnedEvent();
    }

    public void PublishEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke();
    }

}
