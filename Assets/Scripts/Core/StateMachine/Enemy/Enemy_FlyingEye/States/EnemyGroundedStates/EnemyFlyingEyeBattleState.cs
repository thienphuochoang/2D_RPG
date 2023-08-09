using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyFlyingEyeBattleState : EnemyState
{
    private Enemy_FlyingEye _enemy;
    private Transform _player;
    private int _moveDirection;
    public EnemyFlyingEyeBattleState(Enemy enemyBase, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(enemyBase, inputEnemyStateMachine, inputAnimBoolName)
    {
        _enemy = enemyFlyingEye;
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
        if (_enemy.isAlreadyFlyAway == false)
        {
            if (_enemy.stats.currentHealth < ((float)_enemy.stats.GetMaxHealthValue() / 2f) ||
                _enemy.stats.currentHealth < ((float)_enemy.stats.GetMaxHealthValue() / 3f))
            {
                stateMachine.ChangeState(_enemy.runAwayState);
            }
        }
            
        if (_enemy.IsPlayerDetectedInHalfOfTheCircle())
        {
            stateTimer = _enemy.battleTime;
            if (CanAttack())
            {
                if (Vector2.Distance(_player.transform.position, _enemy.transform.position) <= _enemy.attackDistance && _enemy.isAlreadyFlyAway == false)
                {
                    stateMachine.ChangeState(_enemy.meleeAttackState);
                }
                if (_enemy.isAlreadyFlyAway)
                {
                    stateMachine.ChangeState(_enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(_player.transform.position, _enemy.transform.position) > 15)
                stateMachine.ChangeState(_enemy.idleState);
        }
        
        if (_player.position.x > _enemy.transform.position.x && _enemy.facingDirection == -1)
            _enemy.Flip();
        else if (_player.position.x < _enemy.transform.position.x && _enemy.facingDirection == 1)
            _enemy.Flip();
        if (_enemy.isAlreadyFlyAway == false)
        {
            Vector2 direction = (_player.position - _enemy.transform.position).normalized;
            _enemy.SetVelocity(_enemy.moveSpeed * 1.5f * direction.x, direction.y * _enemy.moveSpeed * 1.5f);
        }
        else
        {
            _enemy.ResetZeroVelocity();
        }
        /*Vector2 direction = Vector2.zero;
        if (_enemy.stats.currentHealth < ((float)_enemy.stats.GetMaxHealthValue() / 2f))
        {
            if (Vector2.Distance(_enemy.transform.position, initialPosition) < 0.1f)
            {
                Debug.Log("Test");
                _enemy.ResetZeroVelocity();
            }
            else if (_enemy.isAlreadyFlyAway == false)
            {
                direction = (initialPosition - _enemy.transform.position).normalized;
                _enemy.SetVelocity(_enemy.moveSpeed * 1.5f * direction.x, direction.y * _enemy.moveSpeed * 1.5f);
                _enemy.isAlreadyFlyAway = true;
            }
        }
        else
        {
            direction = (_player.position - _enemy.transform.position).normalized;
            _enemy.SetVelocity(_enemy.moveSpeed * 1.5f * direction.x, direction.y * _enemy.moveSpeed * 1.5f);
        }*/
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
