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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fellow"))
        {
            if (ghost.IsChasing())
            {
                Debug.Log("You Died!");
                ghost.GetFellow().SetDead(true);
            }
            else if (ghost.IsHiding())
            {
                ghost.SwitchState(StateType.EATEN);
            }
        }
    }
}
