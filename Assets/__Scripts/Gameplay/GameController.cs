using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

// This class contains gamplay logic, UI, music, and more
// Over the development of this game it became somewhat of an unruly beast
// In heinsight I would work to modularize and decouple much of this class
public class GameController : MonoBehaviour
{
    public int WaveNumer { get { return waveNumber; } }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private int enemiesPerWave = 5;

    private GameObject gameOverlayUI;
    private GameObject player;
    private bool gameOver = false;
    private int enemiesRemaining;
    private int waveNumber = 1;
    private int playerScore = 0;

    void Start()
    {
        Cursor.visible = false; // hide & lock the cursor when gameplay starts
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
        Cursor.visible = true; // show & unlock mouse
        Cursor.lockState = CursorLockMode.None;

        gameOverUI.SetActive(true); // show game over ui

        FindObjectOfType<SpawnController>().GetComponent<SpawnController>().DisableSpawning(); //stop enemy spawning
        FindObjectOfType<GameController>().GetComponent<AudioSource>().Stop(); // stop gameplay music
        FindObjectOfType<SoundController>().PlayMenuMusic(); // play main menu music
        FindObjectOfType<SoundController>().PlayGameOverSound(); // play game over sound

        gameOverlayUI.SetActive(true); // show game over screen

        player.SetActive(false); // make player invisible (this was done instead of Destroy(player) to stop nullptr exceptions?

        var enemies = GameObject.Find("Enemies").gameObject.GetComponentsInChildren<EnemyBehaviour>(); // get a list of all enemies 
        foreach(EnemyBehaviour e in enemies) {
            Destroy(e.gameObject); // destroy all enemies
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
        playerScore += enemy.ScoreValue * waveNumber * 100; // dash kills get bonus points
        UpdateScore();
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // the score value of an enemy is multiplied by the waveNumber*10 so higher level enemies are worth more points
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
        yield return new WaitForSeconds(5.0f);
        enemiesRemaining = enemiesPerWave + waveNumber;
       //Debug.Log($"Enemy count: {enemiesRemaining}");

        gameObject.GetComponent<AudioSource>().pitch *= 1.005f; // increase musich pitch each wave by 1.005%

        waveNumber++;

        //Debug.Log($"Wave #{waveNumber}");
        waveText.text = $"WAVE: {waveNumber}"; //update wave number on ui
        FindObjectOfType<SoundController>().PlayNextWaveSound(); // play a sound to announce next wave

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
