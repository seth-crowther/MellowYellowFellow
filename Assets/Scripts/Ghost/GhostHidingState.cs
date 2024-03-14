using System;
using UnityEngine;
using UnityEngine.AI;

public class GhostHidingState : IGhostState
{
    float powerUpDuration = 10.0f;
    DateTime start;

    public void EnterState(GhostStateManager ghost)
    {
        start = DateTime.Now;
        ghost.SetScaredMaterial();
    }
    public void UpdateState(GhostStateManager ghost)
    {
        ghost.GetAgent().destination = PickHidingPlace(ghost);

        TimeSpan elapsedTime = DateTime.Now - start;
        if (elapsedTime.TotalSeconds > powerUpDuration)
        {
            ghost.SwitchState(StateType.CHASING);
        }
    }
    public void ExitState(GhostStateManager ghost)
    {
        
    }

    Vector3 PickHidingPlace(GhostStateManager ghost)
    {
        Vector3 fellowPos = ghost.GetFellow().transform.position;
        Vector3 ghostPos = ghost.transform.position;
        Vector3 directionToPlayer = (fellowPos - ghostPos).normalized;

        NavMeshHit navHit;
        NavMesh.SamplePosition(ghostPos - (directionToPlayer * 8f), out navHit, 8f, NavMesh.AllAreas);

        return navHit.position;
    }
}
