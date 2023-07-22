using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundedState : EnemyState
{
    protected Enemy_Mushroom enemy;
    protected Transform player;
    public EnemyGroundedState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Mushroom enemyMushroom) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyMushroom;
    }

    public override void BeginState()
    {
        base.BeginState();
        player = GameObject.FindWithTag("Player").transform;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 3f)
            stateMachine.ChangeState(enemy.battleState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
