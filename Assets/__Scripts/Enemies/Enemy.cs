using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int DamageValue { get { return damageValue; } }
    public int ScoreValue { get { return scoreValue; } }

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    public delegate void DashKill(Enemy enemy);
    public static DashKill EnemyKilledByDashEvent;

    [SerializeField] private int damageValue = 1;
    [SerializeField] private int scoreValue = 1;

    [SerializeField] private GameObject explosionFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        var bullet = collision.GetComponent<Projectile>();

        if (player) 
        {
            if(FindObjectOfType<PlayerMovementController>().Dash == true)
            {
                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                Destroy(explosion, 2f);

                PublishEnemyKilledByDashEvent();
                FindObjectOfType<SoundController>().PlayEnemyScreamSound();
                FindObjectOfType<SoundController>().PlayEnemyDeathSound();

            }
            Destroy(gameObject); 
        }

        if (bullet) // Enemy was hit by a butllet -> dies
        {
            FindObjectOfType<SoundController>().PlayEnemyDeathSound();

            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, 2f);

            Destroy(bullet.gameObject);
            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void PublishEnemyKilledEvent()
    {
        EnemyKilledEvent?.Invoke(this);
    }

    private void PublishEnemyKilledByDashEvent()
    {
        EnemyKilledByDashEvent?.Invoke(this);
    }

}
