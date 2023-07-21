using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rigidbody2D;
    
    protected float xInput;
    private string animBoolName;

    public PlayerState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName)
    {
        this.stateMachine = inputPlayerStateMachine;
        this.player = inputPlayer;
        this.animBoolName = inputAnimBoolName;
    }

    public virtual void BeginState()
    {
        player.animator.SetBool(animBoolName, true);
        rigidbody2D = player.rigidbody2D;
    }
    public virtual void UpdateState()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.animator.SetFloat("yVelocity", rigidbody2D.velocity.y);
    }
    public virtual void EndState()
    {
        player.animator.SetBool(animBoolName, false);
    }
}
