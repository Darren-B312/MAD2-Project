using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        var bullet = collision.GetComponent<Projectile>();

        if(player)
        {
            Destroy(player.gameObject);
        }

        if(bullet)
        {
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}
