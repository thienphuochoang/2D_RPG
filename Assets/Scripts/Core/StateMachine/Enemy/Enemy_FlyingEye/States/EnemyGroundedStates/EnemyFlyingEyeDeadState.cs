using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeDeadState : EnemyState
{
    private Enemy_FlyingEye enemy;
    public EnemyFlyingEyeDeadState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyFlyingEye;
    }

    public override void BeginState()
    {
        base.BeginState();
        enemy.ResetZeroVelocity();
        rb.gravityScale = 3;
        enemy.animator.SetBool("Die", true);
        stateTimer = 3f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
        {
            DestroySelf();
        }
    }

    public override void EndState()
    {
        base.EndState();
    }

    private void DestroySelf()
    {
        Object.Destroy(enemy.gameObject);
    }
}
