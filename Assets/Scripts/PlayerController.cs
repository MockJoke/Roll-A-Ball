using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
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
        
        // Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector3.right * speed * Time.deltaTime;
        }
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

        if (other.gameObject.CompareTag("enemy"))
        {
            gameManager.ToggleGameOverScreen(true);
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
        
        checkWin();
    }
    
    private void displayCoinCount()
    {
        score.text = $"Score: {coinCount}";
    }

    private void checkWin()
    {
        if (coinCount >= gameManager.TotalCoinCount)
        {
            gameManager.ToggleGameWonScreen(true);
        }
    }
}