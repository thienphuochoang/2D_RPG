using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeIdleState : EnemyFlyingEyeGroundedState
{
    public EnemyFlyingEyeIdleState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyFlyingEye)
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
