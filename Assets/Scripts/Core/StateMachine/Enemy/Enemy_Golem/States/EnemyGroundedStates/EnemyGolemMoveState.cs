using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemMoveState : EnemyState
{
    private Enemy_Golem enemy;
    private Player _player;
    public EnemyGolemMoveState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGolem;
    }
    public override void BeginState()
    {
        base.BeginState();
        _player = PlayerManager.Instance.player;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, _player.transform.position) < 30f)
            stateMachine.ChangeState(enemy.battleState);
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
