using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinGroundedState : EnemyState
{
    protected Enemy_Goblin enemy;
    protected Transform player;
    public EnemyGoblinGroundedState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyGoblin;
    }

    public override void BeginState()
    {
        base.BeginState();
        player = PlayerManager.Instance.player.transform;
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
