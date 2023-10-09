using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.position;
    }

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, Player.position + offset, Time.deltaTime * 5f);
    }
}
