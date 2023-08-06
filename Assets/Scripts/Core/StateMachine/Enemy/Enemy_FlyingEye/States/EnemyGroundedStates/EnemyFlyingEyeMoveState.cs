using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeMoveState : EnemyFlyingEyeGroundedState
{
    public EnemyFlyingEyeMoveState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyFlyingEye)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (enemy.IsWallDetected() || enemy.IsGroundedDetected() == false)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
