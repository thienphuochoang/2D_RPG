using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKey(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttackState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundedDetected())
            stateMachine.ChangeState(player.jumpState);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
