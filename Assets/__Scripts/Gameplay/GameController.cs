using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int enemiesPerWave = 10;
    private int enemiesRemaining;
    private int waveNumber = 1;

    void Start()
    {
        enemiesRemaining = enemiesPerWave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SpawnController.EnemySpawnedEvent += SpawnController_OnEnemySpawnedEvent;
    }
    
    private void OnDisable()
    {
        SpawnController.EnemySpawnedEvent -= SpawnController_OnEnemySpawnedEvent;
    }

    private void SpawnController_OnEnemySpawnedEvent()
    {
        enemiesRemaining--;
        Debug.Log($"Remaining Enemies = {enemiesRemaining}");

        if(enemiesRemaining == 0) // disable spawning after enough enemies have been spawned
        {
            DisableSpawning();
            StartCoroutine(SetUpNextWave());
            
        }
    }

    private IEnumerator SetUpNextWave()
    {
        yield return new WaitForSeconds(10.0f); // TODO reduce time between enemy waves?
        // play a sound for player to announce next wave
        enemiesRemaining = enemiesPerWave;
        waveNumber++;
        EnableSpawning();
    }

    private void DisableSpawning()
    {
        foreach(var spawnpoint in FindObjectsOfType<SpawnController>())
        {
            spawnpoint.DisableSpawning();
        }
    }
    private void EnableSpawning()
    {
        foreach (var spawnpoint in FindObjectsOfType<SpawnController>())
        {
            spawnpoint.EnableSpawning();
        }
    }
}
