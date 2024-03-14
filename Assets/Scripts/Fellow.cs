using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fellow : MonoBehaviour
{
    [SerializeField]
    YellowFellowGame game;

    [SerializeField]
    LifeCounter lifeCounter;

    [SerializeField]
    float speed = 0.5f;

    // Static so they persist in Menus scene
    static int lives = 3;
    static int score = 0;

    int pelletsEaten = 0;
    const int pointsPerPellet = 100;
    Rigidbody rb;
    Vector3 startingPos;

    static bool dead = false;

    public void ResetPos()
    {
        rb.velocity = Vector3.zero;
        transform.position = startingPos;
    }

    public static bool IsDead() { return dead; }
    public static void SetDead(bool value) { dead = value; }
    public static int GetScore() { return score; }
    public static int GetLives() { return lives; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPos = transform.position;

        score = 0;
        pelletsEaten = 0;
        lives = 3;
        lifeCounter.UpdateCounter(lives);
    }

    void Update()
    {
        if (!dead)
        {
            if (pelletsEaten == YellowFellowGame.NumPellets())
            {
                StartCoroutine(game.NextLevel());
                pelletsEaten = 0;
            }

            if (lives <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (!dead)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pellet"))
        {
            pelletsEaten++;
            score += pointsPerPellet;
        }

        if (other.gameObject.CompareTag("Powerup"))
        {
            foreach (GhostStateManager g in YellowFellowGame.GetGhosts())
            {
                g.Hide();
            }
        }
    }

    public IEnumerator Die()
    {
        dead = true;
        game.DeathSound();
        yield return StartCoroutine(game.FadeOut());
        lives--;
        lifeCounter.UpdateCounter(lives);

        if (lives != 0)
        {
            YellowFellowGame.ResetCharsPos();
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        yield return StartCoroutine(game.FadeIn());
        dead = false;
    }
}
