using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public int WaveNumer { get { return waveNumber; } }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject gameOverUI;

    private GameObject gameOverlayUI;
    private GameObject player;
    private bool gameOver = false;
    private int enemiesRemaining;
    private int waveNumber = 1;
    private int playerScore = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindGameObjectWithTag("Player");
        enemiesRemaining = enemiesPerWave;
        
        // get a handle on the the GameOver screen GameObject
        gameOverlayUI = FindObjectOfType<Canvas>().transform.GetChild(0).gameObject;

    }

    void Update()
    {
        IsGameOver();
    }

    private void IsGameOver()
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameOverUI.SetActive(true);

        FindObjectOfType<SpawnController>().GetComponent<SpawnController>().DisableSpawning(); //stop enemy spawning

        FindObjectOfType<GameController>().GetComponent<AudioSource>().Stop(); // stop gameplay music

        FindObjectOfType<SoundController>().PlayGameOverSound(); // play game over sound

        FindObjectOfType<SoundController>().PlayMenuMusic();

        gameOverlayUI.SetActive(true); // show game over screen

        // 2. make player invisible
        player.SetActive(false); // make player invisible (this was done instead of Destroy(player) to stop nullptr exceptions

        var enemies = GameObject.Find("Enemies").gameObject.GetComponentsInChildren<EnemyBehaviour>(); // get a list of all enemies 
        foreach(EnemyBehaviour e in enemies) {
            Destroy(e.gameObject); // disable each enemies' movement behaviour
        }

        GameObject.Find("SpawnPoints").GetComponent<SpawnController>().DisableSpawning(); // stop spawning new enemies
    }

    private void OnEnable()
    {
        SpawnController.EnemySpawnedEvent += SpawnController_OnEnemySpawnedEvent;
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        Enemy.EnemyKilledByDashEvent += OnEnemyDashKilledEvent;
    }
    
    private void OnDisable()
    {
        SpawnController.EnemySpawnedEvent -= SpawnController_OnEnemySpawnedEvent;
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        Enemy.EnemyKilledByDashEvent -= OnEnemyDashKilledEvent;
    }

    private void OnEnemyDashKilledEvent(Enemy enemy)
    {
        playerScore += enemy.ScoreValue * waveNumber * 100;
        UpdateScore();
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        playerScore += enemy.ScoreValue * waveNumber * 10;
        UpdateScore();
    }
    private void UpdateScore()
    {
        //Debug.Log("Score: " + playerScore);
        scoreText.text = $"SCORE: {playerScore}";
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
        yield return new WaitForSeconds(5.0f); // TODO reduce time between enemy waves?
        // play a sound for player to announce next wave
        enemiesRemaining = enemiesPerWave + waveNumber;
       //Debug.Log($"Enemy count: {enemiesRemaining}");
        gameObject.GetComponent<AudioSource>().pitch *= 1.005f; // increase pitch by 0.5% each wave
        //Debug.Log(gameObject.GetComponent<AudioSource>().pitch);
        waveNumber++;
        //Debug.Log($"Wave #{waveNumber}");
        waveText.text = $"WAVE: {waveNumber}";
        FindObjectOfType<SoundController>().PlayNextWaveSound();


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
