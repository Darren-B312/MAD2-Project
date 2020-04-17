using UnityEngine;

// Enemy movement behaviours
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed += (FindObjectOfType<GameController>().WaveNumer * 0.25f); // increase movement speed with each wave
        //Debug.Log($"Speed: {speed}");
    }

    void Update()
    {
        if (target)
        {
            // constantly move towards player position (updating constantly)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
