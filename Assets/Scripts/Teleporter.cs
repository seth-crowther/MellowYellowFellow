using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Teleporter otherTeleporter;

    [SerializeField]
    Vector3 teleportOffset;

    Vector3 destination;

    void Start()
    {
        destination = transform.position + teleportOffset;
    }

    void OnTriggerEnter(Collider other)
    {
        otherTeleporter.TeleportToMe(other.gameObject);
    }

    public void TeleportToMe(GameObject obj)
    {
        if (obj.CompareTag("Fellow"))
        {
            obj.transform.position = destination;
        }
        else if (obj.CompareTag("Ghost"))
        {
            obj.GetComponent<NavMeshAgent>().Warp(destination);
        }
    }
}
