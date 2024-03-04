public class GhostWaitingState : IGhostState
{
    public void EnterState(GhostStateManager ghost)
    {
        ghost.ResetMaterial();
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

    }
}
