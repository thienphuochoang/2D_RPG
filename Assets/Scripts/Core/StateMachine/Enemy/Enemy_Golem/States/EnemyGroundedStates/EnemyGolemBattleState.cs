using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemBattleState : EnemyState
{
    private Enemy_Golem _enemy;
    private Transform _player;
    private int _moveDirection;
    public EnemyGolemBattleState(Enemy enemyBase, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Golem enemyGolem) : base(enemyBase, inputEnemyStateMachine, inputAnimBoolName)
    {
        _enemy = enemyGolem;
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
                    stateMachine.ChangeState(_enemy.meeleAttackState);
            }
            if (_enemy.IsPlayerDetected().distance > _enemy.attackDistance && 
                _enemy.IsPlayerDetected().distance < _enemy.rangedAttackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(_enemy.rangedAttackState);
            }
            if (_enemy.IsPlayerDetected().distance > _enemy.rangedAttackDistance && 
                _enemy.IsPlayerDetected().distance < _enemy.laserAttackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(_enemy.laserAttackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(_player.transform.position, _enemy.transform.position) > 30)
                stateMachine.ChangeState(_enemy.idleState);
        }

        if (_player.position.x > _enemy.transform.position.x)
            _moveDirection = 1;
        else if (_player.position.x < _enemy.transform.position.x)
            _moveDirection = -1;
        
        _enemy.SetVelocity(_enemy.moveSpeed * 2f * _moveDirection, rb.velocity.y);
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
