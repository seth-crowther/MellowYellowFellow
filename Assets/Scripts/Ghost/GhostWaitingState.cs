public class GhostWaitingState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.ResetMaterial();
        ghost.GetAgent().isStopped = true;
        ghost.ResetPos();
    }

    public void UpdateState(GhostStateManager ghost)
    {
        if (!ghost.GetFellow().PowerUpActive())
        {
            ghost.SwitchState(StateType.CHASING);
        }
    }

    public void ExitState(GhostStateManager ghost)
    {
        ghost.GetAgent().isStopped = false;
    }
}
