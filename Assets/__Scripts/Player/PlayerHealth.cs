using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth { get { return currentHealth; } }

    private int currentHealth { get; set; }
    private CameraShake cameraShake;

    [SerializeField] private int startHealth = 1;
    [SerializeField] private TextMeshProUGUI healthText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if(enemy && FindObjectOfType<PlayerMovementController>().Dash != true)
        {
            FindObjectOfType<SoundController>().PlayPlayerDamageSound();

            cameraShake.Shake();
            currentHealth -= enemy.DamageValue;
            //Debug.Log($"Player Health: = {currentHealth}");
            healthText.text = $"HP: {currentHealth*10}%";
        }

    }

    void Start()
    {
        currentHealth = startHealth;
        cameraShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }


    public void Heal()
    {
        if (currentHealth < 10)
        {
            currentHealth += 1;
            healthText.text = $"HP: {currentHealth * 10}%";

        }
    }
}
