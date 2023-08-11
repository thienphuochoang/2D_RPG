using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemBlockState : EnemyState
{
    private Enemy_Golem enemy;
    public EnemyGolemBlockState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGolem;
    }

    public override void BeginState()
    {
        base.BeginState();
        enemy.isBlocking = true;
        enemy.blockTimer = enemy.blockDuration;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.ResetZeroVelocity();
        if (enemy.isBlocking)
        {
            enemy.blockTimer -= Time.deltaTime;
            enemy.animator.SetBool("Block", true);
            if (enemy.blockTimer <= 0)
            {
                stateMachine.ChangeState(enemy.battleState);
                enemy.animator.SetBool("Block", false);
            }
        }
    }

    public override void EndState()
    {
        base.EndState();
        enemy.isBlocking = false;
        enemy.blockHealthThreshold = 50;
    }
}
