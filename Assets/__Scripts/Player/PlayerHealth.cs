using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    private int currentHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            currentHealth -= enemy.DamageValue;
            Debug.Log($"Player Health: = {currentHealth}");
        }
        if(currentHealth <= 0)
        {
            // GAME OVER HERE (no respawns)
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
