using UnityEngine;

public class Fellow : MonoBehaviour
{
    [SerializeField]
    private YellowFellowGame game;

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

    private bool died;

    public int PelletsEaten()
    {
        return pelletsEaten;
    }

    public bool IsDead()
    {
        return died;
    }

    public void ResetPos()
    {
        transform.position = startingPos;
        died = false;
    }

    public bool PowerUpActive()
    {
        return powerUpTime > 0f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            GhostStateManager ghost = collision.gameObject.GetComponent<GhostStateManager>();
            if (ghost.IsChasing())
            {
                Debug.Log("You Died!");
                died = true;
            }
            else if (ghost.IsHiding())
            {
               ghost.SwitchState(StateType.EATEN);
            }
        }
    }
}
