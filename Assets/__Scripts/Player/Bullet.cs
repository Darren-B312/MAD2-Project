using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Bullet : MonoBehaviour
{
    // this class is used only to tag objects as type Bullet
    //void OnBecameInvisible()
    //{
    //    Destroy(gameObject); // remove bullet from hierarchy when it leaves the camera view
    //}
}
