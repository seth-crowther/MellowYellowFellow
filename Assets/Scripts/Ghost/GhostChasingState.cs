using UnityEngine;
using UnityEngine.AI;

public class GhostChasingState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.ResetMaterial();
    }
    public void UpdateState(GhostStateManager ghost)
    {
        if (ghost.GetFellow().PowerUpActive())
        {
            ghost.SwitchState(StateType.HIDING);
        }

        if (CanSeePlayer(ghost))
        {
            ghost.GetAgent().destination = ghost.GetFellow().transform.position;
        }
        else if (ghost.GetAgent().remainingDistance < 0.25f)
        {
            ghost.GetAgent().destination = PickRandomPosition(ghost);
        }
    }
    public void ExitState(GhostStateManager ghost)
    {
        
    }

    public bool CanSeePlayer(GhostStateManager ghost)
    {
        Vector3 rayPos = ghost.transform.position;
        Vector3 rayDir = (ghost.GetFellow().transform.position - rayPos).normalized;

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

    public Vector3 PickRandomPosition(GhostStateManager ghost)
    {
        Vector3 destination = ghost.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle * 8.0f;
        destination.x += randomDirection.x;
        destination.z += randomDirection.y;

        NavMeshHit navHit;
        NavMesh.SamplePosition(destination, out navHit, 8.0f, NavMesh.AllAreas);

        return navHit.position;
    }
}
