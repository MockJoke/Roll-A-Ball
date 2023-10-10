using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f;
    
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed) * Time.deltaTime);
    }
}
