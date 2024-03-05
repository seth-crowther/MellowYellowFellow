using UnityEngine;
using UnityEngine.AI;

public class GhostHidingState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.SetScaredMaterial();
    }
    public void UpdateState(GhostStateManager ghost)
    {
        ghost.GetAgent().destination = PickHidingPlace(ghost);

        if (!ghost.GetFellow().PowerUpActive())
        {
            ghost.SwitchState(StateType.CHASING);
        }
    }
    public void ExitState(GhostStateManager ghost)
    {
        
    }

    private Vector3 PickHidingPlace(GhostStateManager ghost)
    {
        Vector3 directionToPlayer = (ghost.GetFellow().transform.position - ghost.transform.position).normalized;

        NavMeshHit navHit;
        NavMesh.SamplePosition(ghost.transform.position - (directionToPlayer * 8f), out navHit, 8f, NavMesh.AllAreas);

        return navHit.position;
    }
}
