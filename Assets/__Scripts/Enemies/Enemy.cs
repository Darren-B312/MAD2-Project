using UnityEngine;

// Encapsulate all Ememy state/behaviour
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

    private GameObject VFXParent; // parent object for all particle systems to keep hierarchy organised

    void Start()
    {
        VFXParent = GameObject.Find("VFX");
        if (!VFXParent)
        {
            VFXParent = new GameObject("VFX");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        var bullet = collision.GetComponent<Projectile>();

        if (player) // if enemy hits a player
        {
            if (FindObjectOfType<PlayerMovementController>().Dash == true) // & if enemy was dashing during collision
            {
                PublishEnemyKilledByDashEvent();

                FindObjectOfType<SoundController>().PlayEnemyScreamSound(); // play extra death sound for dash kill emphasis
                FindObjectOfType<SoundController>().PlayEnemyDeathSound();

                FindObjectOfType<PlayerHealth>().Heal(); // Heal the player
            }

            EnemyExpload();

            Destroy(gameObject); 
        }

        if (bullet) // if enemy hits a bullet
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
        // play particle system explosion
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation, VFXParent.transform);
        Destroy(explosion, 2f);
    }

}
