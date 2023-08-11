using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinAttack03State : EnemyState
{
    private Enemy_Goblin enemy;
    private Player _player;
    public EnemyGoblinAttack03State(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGoblin;
    }

    public override void BeginState()
    {
        base.BeginState();
        _player = PlayerManager.Instance.player;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.ResetZeroVelocity();
        if (_player.transform.position.x > enemy.transform.position.x && enemy.facingDirection == -1)
            enemy.Flip();
        else if (_player.transform.position.x < enemy.transform.position.x && enemy.facingDirection == 1)
            enemy.Flip();
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