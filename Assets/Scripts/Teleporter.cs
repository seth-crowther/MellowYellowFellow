using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.CompareTag("Fellow") || other.gameObject.CompareTag("Ghost"))
            otherTeleporter.TeleportToMe(other.gameObject);
    }

    public void TeleportToMe(GameObject obj)
    {
        Debug.Log(obj.tag);
        Debug.Log("Teleported from: " + otherTeleporter.transform.position + "; Teleported to: " + destination);
        obj.transform.position = destination;
    }
}
