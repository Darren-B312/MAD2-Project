using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Projectile : MonoBehaviour
{
    //this class is used only to tag objects as type Projectile
    void OnBecameInvisible()
    {
        Destroy(gameObject); // remove bullet from hierarchy when it leaves the camera view
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }
}
