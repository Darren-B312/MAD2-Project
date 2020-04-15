using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int DamageValue { get { return damageValue; } }
    public int ScoreValue { get { return scoreValue; } }

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;


    [SerializeField] private int damageValue = 1;
    [SerializeField] private int scoreValue = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        var bullet = collision.GetComponent<Projectile>();

        if (player)
        {
            Destroy(gameObject); 
        }

        if (bullet)
        {
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
        if(EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }
}
