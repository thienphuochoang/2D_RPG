using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float lastTimeDashed;
    public PlayerDashState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        stateTimer = player.dashDuration;
        player.SetVelocity(player.dashSpeed * player.dashDirection, rb.velocity.y);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
