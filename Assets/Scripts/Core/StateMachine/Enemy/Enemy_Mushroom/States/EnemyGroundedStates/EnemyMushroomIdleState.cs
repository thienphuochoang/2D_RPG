using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomIdleState : EnemyGroundedState
{
    public EnemyMushroomIdleState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Mushroom enemyMushroom) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyMushroom)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
        stateTimer = enemy.idleTime;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void EndState()
    {
        base.EndState();
    }


}
