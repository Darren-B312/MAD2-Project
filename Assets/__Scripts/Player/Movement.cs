using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
        //rb.AddForce(new Vector2(hMovement * speed, vMovement * speed));

        float yValue = Mathf.Clamp(rb.position.y, -4.75f, 4.75f);
        float xValue = Mathf.Clamp(rb.position.x, -8.9f, 8.9f);

        rb.position = new Vector2(xValue, yValue);
        //rb.AddForce(new Vector2(hMovement * speed * -1, vMovement * speed * -1));
    }
}
