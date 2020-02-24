using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 10.0f;

    [SerializeField] private int dashFactor = 5;
    [SerializeField] private float startDashTime = 0.1f;
    private float dashTime;
    private bool dash;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.LeftShift) && hMovement != 0)
        {
            dash = true;
            dashTime = startDashTime;
        }

        if(dash)
        {
            dashTime -= Time.deltaTime;

            if(dashTime <= 0)
            {
                dash = false;
            }
            else
            {
                rb.velocity = new Vector2(hMovement * speed * dashFactor, vMovement * speed);
            }
        } 
        else
        {
            rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
        }
     

        if (hMovement < 0)
        {
            sr.flipY = true;
        }
        else if (hMovement > 0)
        {
            sr.flipY = false;
        }

        float yValue = Mathf.Clamp(rb.position.y, -4.75f, 4.75f);
        float xValue = Mathf.Clamp(rb.position.x, -8.9f, 8.9f);

        rb.position = new Vector2(xValue, yValue);
    }
}
