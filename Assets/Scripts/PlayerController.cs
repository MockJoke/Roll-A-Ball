using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    [SerializeField] private Rigidbody rb; 

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    [SerializeField] private float speed = 0;
    [SerializeField] private TextMeshProUGUI score;
    
    private int coinCount = 0;

    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        
        resetCoinCount();
    }
 
    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
    
        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    private void FixedUpdate() 
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed); 
    }
    
    void OnTriggerEnter(Collider other) 
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("coin")) 
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            
            addCoin();
        }
    }

    private void resetCoinCount()
    {
        coinCount = 0;
        
        displayCoinCount();
    }

    private void addCoin()
    {
        coinCount++;
        
        displayCoinCount();
    }
    
    private void displayCoinCount()
    {
        score.text = $"Score: {coinCount}";
    }
}