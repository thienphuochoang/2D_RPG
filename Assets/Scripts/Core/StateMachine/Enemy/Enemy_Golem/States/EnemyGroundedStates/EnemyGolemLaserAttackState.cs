using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemLaserAttackState : EnemyState
{
    private Enemy_Golem enemy;
    public EnemyGolemLaserAttackState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGolem;
    }

    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.ResetZeroVelocity();
        if (animationTriggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void EndState()
    {
        base.EndState();
        enemy.lastTimeAttacked = Time.time;
    }
}
