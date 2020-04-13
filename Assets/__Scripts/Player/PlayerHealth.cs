using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 3;
    private int currentHealth { get; set; }


    public int CurrentHealth { get { return currentHealth; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy)
        {
            currentHealth -= enemy.DamageValue;
            //Debug.Log($"Player Health: = {currentHealth}");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }
}
