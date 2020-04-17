using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool Dash { get; private set; }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform t;

    [SerializeField] private float vSpeed = 12.5f;
    [SerializeField] private float hSpeed = 10.0f;

    [SerializeField] private int dashFactor = 7;
    [SerializeField] private float startDashTime = 0.1f;
    [SerializeField] private float cooldown = 5f;

    [SerializeField] private TextMeshProUGUI cooldownText;

    private float dashTime;
    private bool dashReady = true;
    private float dashCD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        t = GetComponent<Transform>();
    }

    void Update()
    {
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");


        // if LeftShit or Space is pressed while dash is not on cooldown
        if (((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space)) && hMovement != 0 && dashReady))
        {
            FindObjectOfType<SoundController>().PlayPlayerDashSound();

            Dash = true;
            dashTime = startDashTime; // init how long a dash lasts for

            dashReady = false; // stop player from being able to spam dash
            dashCD = cooldown;
        }

        if (Dash) //if player is currently dashing, pass their h/v movements to function to be increased
        {
            dashMovement(hMovement, vMovement);
        }
        else
        {
            rb.velocity = new Vector2(hMovement * hSpeed, vMovement * vSpeed); // else normal movement 
        }

        sr.flipY = flipSprite(hMovement, vMovement); // flip which way the sprite is facing based on movement


        if(!dashReady)
        {
            dashCD -= Time.deltaTime;
            //Debug.Log(dashCD);
            var dashpercent = 100 - ((dashCD / cooldown) * 100); // invert percentage for style on UI so it can charge from 0 to 100%
            cooldownText.text = $"BOOST: {dashpercent:0}%";

            if(dashCD <= 0)
            {
                FindObjectOfType<SoundController>().PlayPlayerDashReadySound();
            }
        }

        if (dashCD <= 0)
        {
            cooldownText.text = $"BOOST: READY";
            dashReady = true;
        }

        clampPosition();
    }

    private void clampPosition() // keep the player from escaping view
    {
        float yValue = Mathf.Clamp(rb.position.y, -4.75f, 4.75f);
        float xValue = Mathf.Clamp(rb.position.x, -8.9f, 8.9f);

        rb.position = new Vector2(xValue, yValue);
    }

    private void dashMovement(float hMovement, float vMovement)
    {
        dashTime -= Time.deltaTime;

        if (dashTime <= 0) // if player is no longer dashing (dashtime has run out)
        {
            Dash = false;
        }
        else
        {
            rb.velocity = new Vector2(hMovement * hSpeed * dashFactor, vMovement * vSpeed); // move the player by an increased dashfactor amount 
            // Design Note: dashing only has an effect on your horizontal movement to try and make the player manouver around more to setup dash multikills
        }
    }

    private bool flipSprite(float hMovement, float vMovement)
    {
        if (hMovement < 0)
        {
            return true;
        }

        return false;
    }

}
