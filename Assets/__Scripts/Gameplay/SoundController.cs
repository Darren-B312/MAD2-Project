using UnityEngine;

// Manage loading & playing of all SFX (exluding music)
// I learned how to play random sounds from a collection, partly from this video - https://www.youtube.com/watch?v=r5VrBu_FaPw
public class SoundController : MonoBehaviour
{
    public static SoundController soundController;

    private AudioSource audioSource;

    private AudioClip[] enemyDeathSounds;
    int enemyDeathSoundsCount;

    private AudioClip[] enemyScreamSounds;
    int enemyScreamSoundsCount;

    private AudioClip[] playerDamageSounds;
    int playerDamageSoundsCount;

    private AudioClip[] playerDashSounds;
    int playerDashSoundsCount;

    private AudioClip[] playerShootSounds;
    private AudioClip playerDashReadySound;
    private AudioClip menuButtonSound;
    private AudioClip gameOverSound;
    private AudioClip menuMusic;
    private AudioClip nextWaveSound;

    // Start is called before the first frame update
    void Start()
    {
        soundController = this;
        audioSource = GetComponent<AudioSource>();

        enemyDeathSounds = Resources.LoadAll<AudioClip>("Audio/EnemyDeathSounds"); // get all clips from a folder and store them in an array of type AudioClip
        enemyDeathSoundsCount = enemyDeathSounds.Length; // get the size of the audio clip array once @load time and use it later for random 

        enemyScreamSounds = Resources.LoadAll<AudioClip>("Audio/EnemyScreamSounds");
        enemyScreamSoundsCount = enemyScreamSounds.Length;

        playerDamageSounds = Resources.LoadAll<AudioClip>("Audio/PlayerDamageSounds");
        playerDamageSoundsCount = playerDamageSounds.Length;

        playerDashSounds = Resources.LoadAll<AudioClip>("Audio/PlayerDashSounds");
        playerDashSoundsCount = playerDashSounds.Length;

        playerShootSounds = Resources.LoadAll<AudioClip>("Audio/PlayerShootSounds");
        playerDashReadySound = Resources.Load<AudioClip>("Audio/sfx_sounds_powerup6");
        menuButtonSound = Resources.Load<AudioClip>("Audio/sfx_menu_move3");
        gameOverSound = Resources.Load<AudioClip>("Audio/GameOver");
        menuMusic = Resources.Load<AudioClip>("Audio/Blipotron");
        nextWaveSound = Resources.Load<AudioClip>("Audio/sfx_sound_poweron");
    }

    public void PlayNextWaveSound()
    {
        audioSource.PlayOneShot(nextWaveSound);
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
        // pick a random sound from the array to play (for flavoured/varied gameplay E.g. different explosion sound each time an enemy dies)
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

    public void PlayMenuMusic()
    {
        audioSource.PlayOneShot(menuMusic);
    }
}
