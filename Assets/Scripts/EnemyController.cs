using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 moveOffset;
    private Vector3 targetPos;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    void Update()
    {
        // Move towards the target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Did we reach the target position?
        if(transform.position == targetPos)
        {
            // Ping pong between startPos and startPos + moveOffset.
            if(targetPos == startPos)
            {
                targetPos = startPos + moveOffset;
            }
            else
            {
                targetPos = startPos;
            }
        }
    }
}
