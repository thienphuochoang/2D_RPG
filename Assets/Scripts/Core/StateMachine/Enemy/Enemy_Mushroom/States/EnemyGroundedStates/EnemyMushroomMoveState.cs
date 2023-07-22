using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomMoveState : EnemyGroundedState
{
    public EnemyMushroomMoveState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Mushroom enemyMushroom) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyMushroom)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, enemy.rb.velocity.y);

        if (enemy.IsWallDetected() || enemy.IsGroundedDetected() == false)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
    
}
