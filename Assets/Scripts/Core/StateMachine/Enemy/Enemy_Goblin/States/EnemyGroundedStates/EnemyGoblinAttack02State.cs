using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinAttack02State : EnemyState
{
    private Enemy_Goblin enemy;
    public EnemyGoblinAttack02State(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGoblin;
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