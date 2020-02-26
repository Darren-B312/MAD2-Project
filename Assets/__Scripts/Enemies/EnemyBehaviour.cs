using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision!");
        var player = collision.GetComponent<MovementController>();
        var bullet = collision.GetComponent<Bullet>();

        if(player)
        {
            Debug.Log("player hit enemy");
            Destroy(player.gameObject);
        }

        if(bullet)
        {
            Debug.Log("bullet hit enemy");
            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}
