using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private float bulletSpeed = 50f;

    private GameObject bulletParent;

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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Fire(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Fire(2);
        }
    }

    private void Fire(int direction)
    {
        FindObjectOfType<SoundController>().PlayPlayerShootSound();

        Projectile bullet = Instantiate(bulletPrefab, bulletParent.transform);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();

        switch (direction)
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
