using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // this class is used only to tag objects as type Bullet
    void OnBecameInvisible()
    {
        Destroy(gameObject); // remove bullet from hierarchy when it leaves the camera view
    }
}
