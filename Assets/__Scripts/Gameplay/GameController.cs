using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject gameOverlayUI;
    private GameObject player;
    private bool gameOver = false;

    [SerializeField] private int enemiesPerWave = 10;
    private int enemiesRemaining;
    private int waveNumber = 1;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemiesRemaining = enemiesPerWave;

        gameOverlayUI = FindObjectOfType<Canvas>().transform.GetChild(0).gameObject; // get a handle on the the GameOver screen GameObject
    }

    // Update is called once per frame
    void Update()
    {
        isGameOver();
    }

    private void isGameOver()
    {
        if(player.GetComponent<PlayerHealth>().CurrentHealth <= 0)
        {
            if (!gameOver)
            {
                gameOver = true;
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        gameOverlayUI.SetActive(true); // show game over screen

        // 2. make player invisible
        player.SetActive(false); // make player invisible (this was done instead of Destroy(player) to stop nullptr exceptions

        var enemies = GameObject.Find("Enemies").gameObject.GetComponentsInChildren<EnemyBehaviour>(); // get a list of all enemies 
        foreach(EnemyBehaviour e in enemies) {
            Destroy(e); // disable each enemies' movement behaviour
        }

        GameObject.Find("SpawnPoints").GetComponent<SpawnController>().DisableSpawning(); // stop spawning new enemies
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
        //Debug.Log($"Remaining Enemies = {enemiesRemaining}");

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
