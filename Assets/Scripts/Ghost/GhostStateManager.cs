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
    Fellow fellow;

    [SerializeField]
    Material scaredMaterial;
    Material normalMaterial;

    Vector3 startingPos;

    NavMeshAgent agent;

    const float speedPerLevel = 0.15f;
    float initialSpeed = 1.5f;

    // State machine
    Dictionary<StateType, IGhostState> stateParser;
    IGhostState currentState;
    GhostChasingState chasingState;
    GhostHidingState hidingState;
    GhostEatenState eatenState;
    GhostWaitingState waitingState;

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
        if (currentState == chasingState || currentState == hidingState)
        {
            SwitchState(StateType.HIDING);
        }
    }

    public void GetEaten()
    {
        gameObject.layer = LayerMask.NameToLayer("EatenGhost");
        agent.speed = 12f;
        agent.destination = startingPos;
    }

    public void StopBeingEaten()
    {
        UpdateSpeed();
        gameObject.layer = LayerMask.NameToLayer("Ghost");
    }

    void InitialiseStateParser()
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
        UpdateSpeed();
        agent.acceleration = 999f;
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
        currentState.UpdateState(this);

        if (Fellow.IsDead()) { agent.isStopped = true; }
        else { agent.isStopped = false; }
    }

    public void UpdateSpeed()
    {
        agent.speed = initialSpeed + YellowFellowGame.GetLevel() * speedPerLevel;
    }

    public void SwitchState(StateType newState)
    {
        currentState.ExitState(this);
        currentState = stateParser[newState];
        currentState.EnterState(this);
    }
}
