using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startHealth = 1;
    private int currentHealth { get; set; }

    [SerializeField] private TextMeshProUGUI healthText;

    private CameraShake cameraShake;



    public int CurrentHealth { get { return currentHealth; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        var enemy = collision.GetComponent<Enemy>();
        if(enemy && FindObjectOfType<PlayerMovementController>().Dash != true)
        {
            cameraShake.Shake();
            currentHealth -= enemy.DamageValue;
            //Debug.Log($"Player Health: = {currentHealth}");
            healthText.text = $"HP: {currentHealth*10}%";
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        cameraShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }
}
