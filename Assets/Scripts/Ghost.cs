using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private Fellow player;

    [SerializeField]
    private Material scaredMaterial;
    private Material normalMaterial;

    private bool hiding;

    private NavMeshAgent agent;

    private Vector3 startingPos;

    private bool isEaten;

    public void ResetPos()
    {
        agent.Warp(startingPos); // Can't just set transform.position cause then nav agent gets confused
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        normalMaterial = GetComponent<Renderer>().material;
        startingPos = transform.position;
    }

    void Update()
    {
        // Don't update if eaten
        if (isEaten)
        {
            return;
        }

        if (player.PowerUpActive())
        {
            if (!hiding || agent.remainingDistance < 0.5f)
            {
                hiding = true;
                agent.destination = PickHidingPlace();
                GetComponent<Renderer>().material = scaredMaterial;
            }
        }
        else
        {
            if (hiding)
            {
                GetComponent<Renderer>().material = normalMaterial;
                hiding = false;
            }

            if (CanSeePlayer())
            {
                agent.destination = player.transform.position;
            }
            else if (agent.remainingDistance < 0.5f)
            {
                agent.destination = PickRandomPosition();
            }
        }
    }

    private Vector3 PickRandomPosition()
    {
        Vector3 destination = transform.position;
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 8.0f;
        destination.x += randomDirection.x;
        destination.z += randomDirection.y;

        NavMeshHit navHit;
        NavMesh.SamplePosition(destination, out navHit, 8.0f, NavMesh.AllAreas);

        return navHit.position;
    }

    private bool CanSeePlayer()
    {
        Vector3 rayPos = transform.position;
        Vector3 rayDir = (player.transform.position - rayPos).normalized;

        RaycastHit hit;
        if (Physics.Raycast(rayPos, rayDir, out hit))
        {
            if (hit.transform.CompareTag("Fellow"))
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 PickHidingPlace()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position - (directionToPlayer * 8f), out navHit, 8f, NavMesh.AllAreas);

        return navHit.position;
    }

    public IEnumerator GetEaten()
    { 
        // Values to make ghost zoom back to start
        isEaten = true;
        gameObject.layer = LayerMask.NameToLayer("IgnoreFellow");
        agent.speed = 15f;
        agent.acceleration = 9999f;
        agent.destination = startingPos;

        // Wait until back at the start
        yield return new WaitUntil(() => agent.remainingDistance < 0.05f);

        // Reset values
        agent.speed = 3.5f;
        agent.acceleration = 8f;
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        isEaten = false;
    }
}
