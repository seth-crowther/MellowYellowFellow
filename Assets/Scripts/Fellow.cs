using UnityEngine;

public class Fellow : MonoBehaviour
{
    [SerializeField]
    private YellowFellowGame game;

    [SerializeField]
    private LifeCounter lifeCounter;

    private int lives = 3;

    [SerializeField]
    private float speed = 0.5f;

    private int score = 0;
    private int pelletsEaten = 0;
    private const int pointsPerPellet = 100;

    [SerializeField]
    private const float powerUpDuration = 10f;

    private float powerUpTime = 0f;

    private Rigidbody rb;

    private Vector3 startingPos;

    public int PelletsEaten() { return pelletsEaten; }
    public bool PowerUpActive() { return powerUpTime > 0f; }


    public void ResetPos()
    {
        rb.velocity = Vector3.zero;
        transform.position = startingPos;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPos = transform.position;
    }

    private void Update()
    {
        if (game.IsInGame())
            powerUpTime = Mathf.Max(0f, powerUpTime - Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (game.IsInGame())
        {
            Vector3 velocity = rb.velocity;

            if (Input.GetKey(KeyCode.A))
            {
                velocity.x -= speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity.x += speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                velocity.z += speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity.z -= speed;
            }

            rb.velocity = velocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            pelletsEaten++;
            score += pointsPerPellet;
            Debug.Log("Score is: " + score);
        }

        if (other.gameObject.CompareTag("Powerup"))
        {
            powerUpTime = powerUpDuration;
        }
    }

    public void Die()
    {
        lives--;
        lifeCounter.UpdateCounter(lives);

        if (lives == 0)
        {
            Debug.Log("Game over");
        }
        else
        {
            game.ResetGame();
        }
    }
}
