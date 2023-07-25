using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionHoleState : PlayerState
{
    public PlayerExplosionHoleState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
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
        if (animationTriggerCalled)
            stateMachine.ChangeState(player.idleState);
        player.ResetZeroVelocity();
    }

    public override void EndState()
    {
        base.EndState();
    }
}
