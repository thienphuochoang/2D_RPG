using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (player.IsGroundedDetected() && rigidbody2D.velocity.y == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
            
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rigidbody2D.velocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
