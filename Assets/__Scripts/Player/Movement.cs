using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform t;
    [SerializeField] private float speed = 10f;
    //[SerializeField] private int boostFactor = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        if (hMovement < 0)
        {
            sr.flipY = true;
        }
        else if (hMovement > 0)
        {
            sr.flipY = false;
        }

        //if(didBoost) // TODO: better dash implementation - https://www.youtube.com/watch?v=w4YV8s9Wi3w
        //{
        //    rb.velocity = new Vector2(hMovement * speed * boostFactor, vMovement * speed);
        //}
        //else
        //{
        //    rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
        //}



        float yValue = Mathf.Clamp(rb.position.y, -4.75f, 4.75f);
        float xValue = Mathf.Clamp(rb.position.x, -8.9f, 8.9f);

        rb.position = new Vector2(xValue, yValue);
    }
}
