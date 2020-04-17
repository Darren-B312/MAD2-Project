using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private float bulletSpeed = 40f;
    [SerializeField] private float fireRate = 1.0f;

    private GameObject bulletParent;
    private float nextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        bulletParent = GameObject.Find("Bullets");
        if (!bulletParent)
        {
            bulletParent = new GameObject("Bullets");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time > nextFireTime)
        {
            Fire(1); // call fire function to fire right (1)
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time > nextFireTime)
        {
            Fire(2); // vall fire function to fire left (2)
        }
    }

    private void Fire(int direction)
    {
        nextFireTime = Time.time + fireRate; // stop player from being able to spam fire too fast

        FindObjectOfType<SoundController>().PlayPlayerShootSound();

        Projectile bullet = Instantiate(bulletPrefab, bulletParent.transform);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();

        switch (direction) // switch on Vector2.left or .right 
        {
            case 1:
                rb.velocity = Vector2.right * bulletSpeed;
                break;
            case 2:
                rb.velocity = Vector2.left * bulletSpeed;
                sr.flipY = true;
                break;

        }
    }

}
