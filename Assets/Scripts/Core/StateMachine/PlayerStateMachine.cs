using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState stateState)
    {
        currentState = stateState;
        currentState.BeginState();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.EndState();
        currentState = newState;
        currentState.BeginState();
    }
}
