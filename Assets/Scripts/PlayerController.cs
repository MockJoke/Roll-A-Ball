using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    // Rigidbody of the player.
    [SerializeField] private Rigidbody rb; 
    
    // Joystick references.
    [SerializeField] private Joystick rightJoystick;
    [SerializeField] private Joystick leftJoystick;
    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    private bool isGrounded = true;
    [SerializeField] private bool useButtonControls = false;
    private int coinCount = 0;

    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        
        resetCoinCount();

        gameManager.ToggleInputControlsScreen(useButtonControls);
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
        if (useButtonControls)
        {
            if(rightJoystick.Vertical != 0)
            {
                AddForwardMovementInput();
            }
            if(leftJoystick.Horizontal != 0)
            {
                AddRotationInput();
            }
        }
        else
        {
            // Create a 3D movement vector using the X and Y inputs.
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        
            Vector3 forwardInput = rb.transform.position + movement * moveSpeed * Time.deltaTime;
            rb.MovePosition(forwardInput);
        
            // Apply force to the Rigidbody to move the player.
            // movement = Vector3.ClampMagnitude(movement, 1);
            // rb.AddForce(movement * speed);
        }
        
        // If we press SPACE and we are on the ground, jump.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        // If we are below the level, game over.
        if(transform.position.y < -5)
        {
            gameOver();
        }
        
        // Move();
    }
    
    // Moves rb in forward and backward direction based on vertical input of joystick.
    private void AddForwardMovementInput()
    {
        Vector3 forwardInput = rb.transform.position + rb.transform.forward * (rightJoystick.Vertical * moveSpeed * Time.deltaTime);

        rb.MovePosition(forwardInput); 
    }

    // Rotates rb based on horizontal input of joystick.
    private void AddRotationInput()
    {
        Quaternion desiredRotation = rb.transform.rotation * Quaternion.Euler(Vector3.up * leftJoystick.Horizontal * rotationSpeed * Time.deltaTime);

        rb.MoveRotation(desiredRotation);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector3.back * rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector3.left * rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector3.right * rotationSpeed * Time.deltaTime;
        }
    }
    
    void OnCollisionEnter (Collision collision)
    {
        // If we collided with a ground surface, we are grounded.
        if(collision.GetContact(0).normal == Vector3.up)
        {
            isGrounded = true;
        }
    }
    
    void OnTriggerEnter(Collider other) 
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("coin")) 
        {
            AudioManager.instance.Play(EAudioClips.CollectCoin);
            
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            
            addCoin();
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            gameOver();
        }
    }

    private void resetCoinCount()
    {
        coinCount = 0;
        
        gameManager.DisplayScore(coinCount);
    }

    private void addCoin()
    {
        coinCount++;
        
        gameManager.DisplayScore(coinCount);
        
        checkWin();
    }

    private void gameOver()
    {
        Time.timeScale = 0f;
        
        gameManager.ToggleGameOverScreen(true);
    }
    
    private void checkWin()
    {
        if (coinCount >= gameManager.TotalCoinCount)
        {
            gameManager.ToggleGameWonScreen(true);
        }
    }
}