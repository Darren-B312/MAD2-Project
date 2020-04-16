using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController soundController;

    private AudioSource audioSource;

    private AudioClip[] enemyDeathSounds;
    int enemyDeathSoundsCount;

    // Start is called before the first frame update
    void Start()
    {
        soundController = this;
        audioSource = GetComponent<AudioSource>();

        enemyDeathSounds = Resources.LoadAll<AudioClip>("Audio/EnemyDeathSounds");
        enemyDeathSoundsCount = enemyDeathSounds.Length; // get the size of the audio clip array once and use it later for random 
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSounds[Random.Range(0, enemyDeathSoundsCount)]);
    }
}
