using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState state)
    {
        currentState = state;
        currentState.BeginState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.EndState();
        currentState = newState;
        currentState.BeginState();
    }
}
