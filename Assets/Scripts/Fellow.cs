using UnityEngine;
using UnityEngine.SceneManagement;

public class Fellow : MonoBehaviour
{
    [SerializeField]
    private YellowFellowGame game;

    [SerializeField]
    private LifeCounter lifeCounter;

    [SerializeField]
    private float speed = 0.5f;

    // Static so they persist in Menus scene
    private static int lives = 3;
    private static int score = 0;

    private int pelletsEaten = 0;
    private const int pointsPerPellet = 100;
    private Rigidbody rb;
    private Vector3 startingPos;

    public void ResetPos()
    {
        rb.velocity = Vector3.zero;
        transform.position = startingPos;
    }

    public static int GetScore() { return score; }

    public static int GetLives() { return lives; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPos = transform.position;

        score = 0;
        pelletsEaten = 0;
        lives = 3;
        lifeCounter.UpdateCounter(lives);
    }

    private void Update()
    {
        if (pelletsEaten == YellowFellowGame.NumPellets())
        {
            YellowFellowGame.NextLevel();
            pelletsEaten = 0;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            pelletsEaten++;
            score += pointsPerPellet;
        }

        if (other.gameObject.CompareTag("Powerup"))
        {
            foreach (GhostStateManager g in game.GetGhosts())
            {
                g.Hide();
            }
        }
    }

    public void Die()
    {
        lives--;
        lifeCounter.UpdateCounter(lives);

        if (lives != 0)
        {
            YellowFellowGame.ResetCharsPos();
        }
    }
}
