using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeMoveState : EnemyFlyingEyeGroundedState
{
    private float amplitude = 2f;        // Amplitude of the sine wave
    private float frequency = 1f;        // Frequency of the sine wave
    public EnemyFlyingEyeMoveState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName, Enemy_FlyingEye enemyFlyingEye) : base(inputEnemy, inputEnemyStateMachine, inputAnimBoolName, enemyFlyingEye)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Calculate the new position based on a sine wave pattern
        float newY = enemy.initialPosition.y + amplitude * Mathf.Sin(Time.time * frequency);
        Vector2 velocity = Vector2.zero;
        if (newY > enemy.currentMaxHeight)
        {
            velocity = new Vector2(enemy.moveSpeed, -(newY - enemy.transform.position.y));
        }
        else
        {
            velocity = new Vector2(enemy.moveSpeed, newY - enemy.transform.position.y);
        }
        //velocity = new Vector2(horizontalSpeed, newY - enemy.transform.position.y);
        if (enemy.IsWallDetected() || enemy.IsGroundedDetected() == false)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        //enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);
        enemy.SetVelocity(velocity.x * enemy.facingDirection, velocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
