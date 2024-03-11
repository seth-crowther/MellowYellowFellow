using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IGhostState
{
    void EnterState(GhostStateManager ghost);
    void UpdateState(GhostStateManager ghost);
    void ExitState(GhostStateManager ghost);
}

public enum StateType
{
    CHASING,
    HIDING,
    EATEN,
    WAITING
}

public class GhostStateManager : MonoBehaviour
{
    [SerializeField]
    private YellowFellowGame game;

    [SerializeField]
    private Fellow fellow;

    [SerializeField]
    private Material scaredMaterial;
    private Material normalMaterial;

    private Vector3 startingPos;

    private NavMeshAgent agent;

    private bool alreadyEaten;

    // State machine
    private Dictionary<StateType, IGhostState> stateParser;
    private IGhostState currentState;
    private GhostChasingState chasingState;
    private GhostHidingState hidingState;
    private GhostEatenState eatenState;
    private GhostWaitingState waitingState;

    public Fellow GetFellow() { return fellow; }
    public NavMeshAgent GetAgent() { return agent; }
    public void ResetPos() 
    {
        agent.isStopped = true;
        agent.Warp(startingPos);
        agent.destination = startingPos;
        agent.isStopped = false;
    }
    public void ResetMaterial() { GetComponent<Renderer>().material = normalMaterial; }
    public void SetScaredMaterial() { GetComponent<Renderer>().material = scaredMaterial; }
    public bool AtStartPos() { return Vector3.Distance(transform.position, startingPos) < 0.5f; }
    public bool IsChasing() { return currentState == chasingState; }
    public bool IsHiding() { return currentState == hidingState; }
    public void Hide()
    {
        if (currentState == chasingState)
        {
            SwitchState(StateType.HIDING);
        }
    }

    public void GetEaten()
    {
        gameObject.layer = LayerMask.NameToLayer("EatenGhost");
        agent.speed = 12f;
        agent.acceleration = 9999f;
        agent.destination = startingPos;
    }

    public void StopBeingEaten()
    {
        agent.speed = 3f;
        agent.acceleration = 8f;
        gameObject.layer = LayerMask.NameToLayer("Ghost");
    }

    private void InitialiseStateParser()
    {
        stateParser = new Dictionary<StateType, IGhostState>
        {
            { StateType.CHASING, chasingState },
            { StateType.HIDING, hidingState },
            { StateType.EATEN, eatenState },
            { StateType.WAITING, waitingState }
        };
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        normalMaterial = GetComponent<Renderer>().material;

        startingPos = transform.position;
        agent.destination = startingPos;

        chasingState = new GhostChasingState();
        hidingState = new GhostHidingState();
        eatenState = new GhostEatenState();
        waitingState = new GhostWaitingState();
        currentState = chasingState;

        InitialiseStateParser();

        currentState.EnterState(this);
    }

    void Update()
    {
        if (game.IsInGame())
            currentState.UpdateState(this);
    }

    public void SwitchState(StateType newState)
    {
        currentState.ExitState(this);
        currentState = stateParser[newState];
        currentState.EnterState(this);
    }
}
