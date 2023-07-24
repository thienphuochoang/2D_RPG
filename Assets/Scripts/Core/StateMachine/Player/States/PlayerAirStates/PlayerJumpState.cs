using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
