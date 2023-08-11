using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinIdleState : EnemyGoblinGroundedState
{
    public EnemyGoblinIdleState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_Goblin enemyGoblin) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyGoblin)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
        stateTimer = enemy.idleTime;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void EndState()
    {
        base.EndState();
    }


}
