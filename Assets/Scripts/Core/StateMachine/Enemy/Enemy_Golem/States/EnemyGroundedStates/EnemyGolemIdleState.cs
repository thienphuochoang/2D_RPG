using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemIdleState : EnemyState
{
    private Enemy_Golem enemy;
    private Player _player;
    public EnemyGolemIdleState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGolem;
    }
    public override void BeginState()
    {
        base.BeginState();
        _player = PlayerManager.Instance.player;
        stateTimer = enemy.idleTime;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, _player.transform.position) < 30f)
            stateMachine.ChangeState(enemy.battleState);
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
