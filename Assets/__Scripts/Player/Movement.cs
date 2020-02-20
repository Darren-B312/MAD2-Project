using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
    }
}
