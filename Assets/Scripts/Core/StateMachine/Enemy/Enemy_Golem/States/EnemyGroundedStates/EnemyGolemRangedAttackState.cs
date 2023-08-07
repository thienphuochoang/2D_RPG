using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemRangedAttackState : EnemyState
{
    private Enemy_Golem enemy;
    private Transform _player;
    public EnemyGolemRangedAttackState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGolem;
    }

    public override void BeginState()
    {
        base.BeginState();
        _player = PlayerManager.Instance.player.transform;
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
