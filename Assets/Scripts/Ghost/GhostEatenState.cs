public class GhostEatenState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.GetEaten();
    }

    public void UpdateState(GhostStateManager ghost)
    {
        if (ghost.ReachedDestination())
        {
            ghost.SwitchState(StateType.WAITING);
        }
    }

    public void ExitState(GhostStateManager ghost) 
    {
        ghost.StopBeingEaten();
    }
}
