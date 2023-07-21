using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        player.ResetZeroVelocity();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (xInput == player.facingDirection && player.IsWallDetected())
            return;
        if (xInput != 0 && player.isBusy == false)
            stateMachine.ChangeState(player.moveState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
