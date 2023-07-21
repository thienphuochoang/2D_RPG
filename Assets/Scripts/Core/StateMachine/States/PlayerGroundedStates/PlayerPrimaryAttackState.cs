using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 1;

    public PlayerPrimaryAttackState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName) : base(inputPlayer, inputPlayerStateMachine, inputAnimBoolName)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        
        player.animator.SetInteger("ComboCounter", comboCounter);

        float attackDirection = player.facingDirection;
        if (xInput != 0)
            attackDirection = xInput;
        
        player.SetVelocity(player.attackOffsetMovement[comboCounter].x * attackDirection, player.attackOffsetMovement[comboCounter].y);
        stateTimer = 0.1f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (stateTimer < 0)
            player.ResetZeroVelocity();
        if (animationTriggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void EndState()
    {
        base.EndState();
        player.StartCoroutine(nameof(player.BusyFor), 0.15f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
