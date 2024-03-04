using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fellow : MonoBehaviour
{
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
        powerUpTime = Mathf.Max(0f, powerUpTime - Time.deltaTime);
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
            if (!PowerUpActive())
            {
                Debug.Log("You Died!");
                died = true;
            }
            else
            {
                StartCoroutine(collision.gameObject.GetComponent<Ghost>().GetEaten());
            }
        }
    }
}
