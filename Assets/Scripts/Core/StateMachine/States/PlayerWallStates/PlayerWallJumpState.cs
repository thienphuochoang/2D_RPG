using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
        stateTimer = 0.4f;
        player.SetVelocity(5 * -player.facingDirection, player.jumpForce);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
        
        if (player.IsGroundedDetected() && rb.velocity.y == 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
