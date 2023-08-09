using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeRunAwayState : EnemyState
{
    private Enemy_FlyingEye enemy;
    private Vector3 flyAwayPosition;
    public EnemyFlyingEyeRunAwayState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName)
    {
        enemy = enemyFlyingEye;
    }

    public override void BeginState()
    {
        base.BeginState();
        flyAwayPosition = enemy.transform.position;
        flyAwayPosition.y += UnityEngine.Random.Range(3f, 6f);
        flyAwayPosition.x += UnityEngine.Random.Range(-6f, 6f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Vector2.Distance(enemy.transform.position, flyAwayPosition) < 0.1f)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
        Vector2 direction = (flyAwayPosition - enemy.transform.position).normalized;
        enemy.SetVelocity(enemy.moveSpeed * 1.5f * direction.x, direction.y * enemy.moveSpeed * 1.5f);
    }

    public override void EndState()
    {
        base.EndState();
        enemy.isAlreadyFlyAway = true;
    }
    
    private bool CanFlyAway()
    {
        if (Time.time >= enemy.lastTimeFlyAway + enemy.flyAwayCooldown)
        {
            enemy.lastTimeFlyAway = Time.time;
            return true;
        }

        return false;
    }
}
