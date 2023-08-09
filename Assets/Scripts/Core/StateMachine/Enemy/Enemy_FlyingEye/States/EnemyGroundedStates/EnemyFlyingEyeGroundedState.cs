using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeGroundedState : EnemyState
{
    protected Enemy_FlyingEye enemy;
    protected Transform player;
    public EnemyFlyingEyeGroundedState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyFlyingEye;
    }

    public override void BeginState()
    {
        base.BeginState();
        player = PlayerManager.Instance.player.transform;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (enemy.IsPlayerDetectedInHalfOfTheCircle() || Vector2.Distance(enemy.transform.position, player.transform.position) < 5f)
            stateMachine.ChangeState(enemy.battleState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
