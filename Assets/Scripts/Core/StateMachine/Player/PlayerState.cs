using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    
    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected float stateTimer;
    protected bool animationTriggerCalled = false;

    public PlayerState(Player inputPlayer, PlayerStateMachine inputPlayerStateMachine, string inputAnimBoolName)
    {
        this.stateMachine = inputPlayerStateMachine;
        this.player = inputPlayer;
        this.animBoolName = inputAnimBoolName;
    }

    public virtual void BeginState()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
        animationTriggerCalled = false;
    }
    public virtual void UpdateState()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }
    public virtual void EndState()
    {
        player.animator.SetBool(animBoolName, false);
    }

    public virtual void FinishAnimationTrigger()
    {
        animationTriggerCalled = true;
    }
}
