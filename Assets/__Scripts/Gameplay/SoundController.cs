using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController soundController;
    //https://www.youtube.com/watch?v=r5VrBu_FaPw
    private AudioSource audioSource;

    private AudioClip[] enemyDeathSounds;
    int enemyDeathSoundsCount;

    private AudioClip[] enemyScreamSounds;
    int enemyScreamSoundsCount;

    private AudioClip[] playerDamageSounds;
    int playerDamageSoundsCount;

    private AudioClip[] playerDashSounds;
    int playerDashSoundsCount;
    private AudioClip playerDashReadySound;

    private AudioClip[] playerShootSounds;

    private AudioClip menuButtonSound;

    private AudioClip gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        soundController = this;
        audioSource = GetComponent<AudioSource>();

        enemyDeathSounds = Resources.LoadAll<AudioClip>("Audio/EnemyDeathSounds");
        enemyDeathSoundsCount = enemyDeathSounds.Length; // get the size of the audio clip array once and use it later for random 

        enemyScreamSounds = Resources.LoadAll<AudioClip>("Audio/EnemyScreamSounds");
        enemyScreamSoundsCount = enemyScreamSounds.Length;

        playerDamageSounds = Resources.LoadAll<AudioClip>("Audio/PlayerDamageSounds");
        playerDamageSoundsCount = playerDamageSounds.Length;

        playerShootSounds = Resources.LoadAll<AudioClip>("Audio/PlayerShootSounds");

        playerDashSounds = Resources.LoadAll<AudioClip>("Audio/PlayerDashSounds");
        playerDashSoundsCount = playerDashSounds.Length;

        playerDashReadySound = Resources.Load<AudioClip>("Audio/sfx_sounds_powerup6");

        menuButtonSound = Resources.Load<AudioClip>("Audio/sfx_menu_move3");

        gameOverSound = Resources.Load<AudioClip>("Audio/GameOver");
    }

    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    public void PlayMenuButtonSound()
    {
        audioSource.PlayOneShot(menuButtonSound);
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSounds[Random.Range(0, enemyDeathSoundsCount)]);
    }

    public void PlayEnemyScreamSound()
    {
        audioSource.PlayOneShot(enemyScreamSounds[Random.Range(0, enemyScreamSoundsCount)]);
    }

    public void PlayPlayerDamageSound()
    {
        audioSource.PlayOneShot(playerDamageSounds[Random.Range(0, playerDamageSoundsCount)]);
    }

    public void PlayPlayerShootSound()
    {
        audioSource.PlayOneShot(playerShootSounds[0]);
    }

    public void PlayPlayerDashSound()
    {
        audioSource.PlayOneShot(playerDashSounds[Random.Range(0, playerDashSoundsCount)]);
    }
    public void PlayPlayerDashReadySound()
    {
        audioSource.PlayOneShot(playerDashReadySound);
    }
}
