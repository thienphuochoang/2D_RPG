using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeStunnedState : EnemyState
{
    private Enemy_FlyingEye _enemy;
    private SpriteRenderer _spriteRenderer;
    public EnemyFlyingEyeStunnedState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        _enemy = enemyFlyingEye;
    }

    public override void BeginState()
    {
        base.BeginState();
        _spriteRenderer = _enemy.GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.flipX = true;
        _enemy.hitEffect.InvokeRepeating(nameof(_enemy.hitEffect.BlinkRedColor), 0, 0.1f);
        stateTimer = _enemy.stunDuration;
        _enemy.SetVelocity(-(_enemy.facingDirection * _enemy.stunDirection.x), _enemy.stunDirection.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            stateMachine.ChangeState(_enemy.idleState);
    }

    public override void EndState()
    {
        base.EndState();
        _spriteRenderer.flipX = false;
        _enemy.Flip();
        _enemy.hitEffect.Invoke(nameof(_enemy.hitEffect.CancelRedBlink), 0);
    }
}
