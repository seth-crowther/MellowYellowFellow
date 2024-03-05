using UnityEngine;

public class GhostEatenState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.GetEaten();
    }

    public void UpdateState(GhostStateManager ghost)
    {
        if (ghost.AtStartPos())
        {
            ghost.SwitchState(StateType.WAITING);
        }
    }

    public void ExitState(GhostStateManager ghost) 
    {
        ghost.StopBeingEaten();
    }
}
