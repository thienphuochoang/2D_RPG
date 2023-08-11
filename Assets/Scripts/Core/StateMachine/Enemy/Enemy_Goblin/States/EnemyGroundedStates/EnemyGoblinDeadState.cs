using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyGoblinDeadState : EnemyState
{
    private Enemy_Goblin enemy;
    public EnemyGoblinDeadState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGoblin;
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
