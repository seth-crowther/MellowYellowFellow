using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    GhostStateManager ghost;

    void Start()
    {
        ghost = transform.parent.gameObject.GetComponent<GhostStateManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fellow"))
        {
            if (ghost.IsChasing() && !Fellow.IsDead())
            {
                StartCoroutine(ghost.GetFellow().Die());
            }
            else if (ghost.IsHiding())
            {
                ghost.SwitchState(StateType.EATEN);
            }
        }
    }
}
