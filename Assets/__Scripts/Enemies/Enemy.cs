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

    private GameObject VFXParent;

    void Start()
    {
        VFXParent = GameObject.Find("VFX");
        if(!VFXParent)
        {
            VFXParent = new GameObject("VFX");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        var bullet = collision.GetComponent<Projectile>();

        if (player) 
        {
            if(FindObjectOfType<PlayerMovementController>().Dash == true)
            {


                PublishEnemyKilledByDashEvent();
                FindObjectOfType<SoundController>().PlayEnemyScreamSound();
                FindObjectOfType<SoundController>().PlayEnemyDeathSound();

                FindObjectOfType<PlayerHealth>().Heal();
            }

            EnemyExpload();

            Destroy(gameObject); 
        }

        if (bullet) // Enemy was hit by a butllet -> dies
        {
            FindObjectOfType<SoundController>().PlayEnemyDeathSound();

            EnemyExpload();

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

    private void EnemyExpload()
    {
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation, VFXParent.transform);
        Destroy(explosion, 2f);
    }

}
