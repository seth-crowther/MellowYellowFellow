using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Teleporter otherTeleporter;

    [SerializeField]
    private Vector3 teleportOffset;

    private Vector3 destination;

    void Start()
    {
        destination = transform.position + teleportOffset;
    }

    private void OnTriggerEnter(Collider other)
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
