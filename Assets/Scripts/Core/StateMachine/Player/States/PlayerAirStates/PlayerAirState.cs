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
        if (Input.GetKey(KeyCode.A))
            stateMachine.ChangeState(player.airAttackState);
        
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (Mathf.Approximately(0, rb.velocity.y))
        {
            AudioManager.Instance.PlaySFX(4);
            stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
