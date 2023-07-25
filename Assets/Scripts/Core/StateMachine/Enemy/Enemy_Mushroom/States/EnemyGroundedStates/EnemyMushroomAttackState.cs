using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomAttackState : EnemyState
{
    private Enemy_Mushroom enemy;
    private Transform _player;
    public EnemyMushroomAttackState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Mushroom enemyMushroom) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyMushroom;
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
