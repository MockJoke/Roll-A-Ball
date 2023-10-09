using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraTransitionTime = 5f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, player.position + offset, Time.deltaTime * cameraTransitionTime);
    }
}
