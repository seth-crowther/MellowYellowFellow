using System;

public class GhostWaitingState : IGhostState
{
    const float waitTime = 2.5f;
    DateTime start;
    public void EnterState(GhostStateManager ghost)
    {
        start = DateTime.Now;
        ghost.ResetMaterial();
        ghost.GetAgent().isStopped = true;
    }

    public void UpdateState(GhostStateManager ghost)
    {
        TimeSpan elapsedTime = DateTime.Now - start;
        if (elapsedTime.TotalSeconds > waitTime)
        {
            ghost.SwitchState(StateType.CHASING);
        }
    }

    public void ExitState(GhostStateManager ghost)
    {
        ghost.GetAgent().isStopped = false;
    }
}
