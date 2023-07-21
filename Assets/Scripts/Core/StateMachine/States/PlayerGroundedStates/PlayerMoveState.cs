using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    
    public PlayerMoveState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        player.SetVelocity(xInput * player.moveSpeed, rigidbody2D.velocity.y);
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
            
    }

    public override void EndState()
    {
        base.EndState();
    }
}
