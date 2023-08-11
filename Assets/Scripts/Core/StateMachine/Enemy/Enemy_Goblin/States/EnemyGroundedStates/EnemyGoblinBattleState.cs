using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyGoblinBattleState : EnemyState
{
    private Enemy_Goblin _enemy;
    private Transform _player;
    private int _moveDirection;
    public EnemyGoblinBattleState(Enemy enemyBase, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(enemyBase, inputEnemyStateMachine, inputAnimBoolName)
    {
        _enemy = enemyGoblin;
    }

    public override void BeginState()
    {
        base.BeginState();
        _player = PlayerManager.Instance.player.transform;
        if (_player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(_enemy.moveState);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.IsPlayerDetected())
        {
            stateTimer = _enemy.battleTime;
            if (_enemy.IsPlayerDetected().distance < _enemy.attackDistance)
            {
                if (CanAttack())
                {
                    int randomNumber = UnityEngine.Random.Range(0, 15);
                    if (randomNumber <= 5)
                        stateMachine.ChangeState(_enemy.attackState);
                    else if (randomNumber > 5 && randomNumber <= 10)
                        stateMachine.ChangeState(_enemy.attack02State);
                    else if (randomNumber > 10)
                        stateMachine.ChangeState(_enemy.attack03State);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(_player.transform.position, _enemy.transform.position) > 15)
                stateMachine.ChangeState(_enemy.idleState);
        }

        if (Vector2.Distance(_player.position, _enemy.transform.position) < _enemy.attackDistance)
            _moveDirection = 0;
        else if (_player.position.x > _enemy.transform.position.x)
            _moveDirection = 1;
        else if (_player.position.x < _enemy.transform.position.x)
            _moveDirection = -1;

        _enemy.SetVelocity(_enemy.moveSpeed * 1.5f * _moveDirection, rb.velocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }

    private bool CanAttack()
    {
        if (Time.time >= _enemy.lastTimeAttacked + _enemy.attackCooldown)
        {
            _enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
