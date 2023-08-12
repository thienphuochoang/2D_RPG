using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomDeadState : EnemyState
{
    private Enemy_Mushroom enemy;
    public EnemyMushroomDeadState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Mushroom enemyMushroom) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyMushroom;
    }

    public override void BeginState()
    {
        base.BeginState();
        rb.velocity = Vector2.zero;
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
