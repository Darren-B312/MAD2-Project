using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float dashTime;
    [SerializeField] private float startDashTime = 2.0f;
    private int direction;
    private bool dash = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }
     
        if(direction == 0)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                direction = -1;
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                direction = 1;
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if(direction == -1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                } 
                else if( direction == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }
}
