using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private int airComboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 0.6f;
    public PlayerAirAttackState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        xInput = 0;
        if (airComboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
            airComboCounter = 0;
        
        player.animator.SetInteger("AirComboCounter", airComboCounter);

        /*float attackDirection = player.facingDirection;
        if (xInput != 0)
            attackDirection = xInput;*/
        
        //player.SetVelocity(rb.velocity.x * attackDirection, rb.velocity.y);
        stateTimer = 0.1f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            player.SetVelocity(0, rb.velocity.y);
        if (animationTriggerCalled)
            stateMachine.ChangeState(player.airState);
    }

    public override void EndState()
    {
        base.EndState();
        player.StartCoroutine(nameof(player.BusyFor), 0.15f);
        airComboCounter++;
        lastTimeAttacked = Time.time;
    }
}
