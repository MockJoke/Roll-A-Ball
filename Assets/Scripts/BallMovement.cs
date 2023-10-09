using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody ball;
    [SerializeField] private GameObject win, GameOver;
    [SerializeField] private Text ScoreBoard;
    [SerializeField] private int score; 
    private int coinCount;

    void Start()
    {
        if (ball == null)
            ball = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(-vertical, 0, horizontal);

        ball.AddForce(vel * Time.deltaTime * 100f);

        ScoreBoard.text = "" + score; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("finishline"))
        {
            win.SetActive(true);
            score += 50; 
        }

        else if(collision.gameObject.CompareTag("ground"))
        {
            GameOver.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            coinCount++;
            score = coinCount; 
        }
    }
}
