using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGoblinStunnedState : EnemyState
{
    private Enemy_Goblin _enemy;
    private SpriteRenderer _spriteRenderer;
    public EnemyGoblinStunnedState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        _enemy = enemyGoblin;
    }

    public override void BeginState()
    {
        base.BeginState();
        _spriteRenderer = _enemy.GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.flipX = true;
        _enemy.entityEffect.InvokeRepeating(nameof(_enemy.entityEffect.BlinkRedColor), 0, 0.1f);
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
        _enemy.entityEffect.Invoke(nameof(_enemy.entityEffect.CancelRedBlink), 0);
    }
}
