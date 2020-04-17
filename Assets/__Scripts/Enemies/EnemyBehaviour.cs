using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Transform target;

    void Start()
    { 
 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed += FindObjectOfType<GameController>().WaveNumer;
        Debug.Log($"Speed: {speed}");

    }

    void Update()
    {
        if(target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
} 
