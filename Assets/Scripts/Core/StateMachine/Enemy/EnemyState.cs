using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;
    

    private string animBoolName;
    protected float stateTimer;
    protected bool animationTriggerCalled = false;

    public EnemyState(Enemy inputEnemy, EnemyStateMachine inputEnemyStateMachine, string inputAnimBoolName)
    {
        this.stateMachine = inputEnemyStateMachine;
        this.enemyBase = inputEnemy;
        this.animBoolName = inputAnimBoolName;
    }

    public virtual void BeginState()
    {
        rb = enemyBase.rb;
        enemyBase.animator.SetBool(animBoolName, true);
        animationTriggerCalled = false;
    }
    public virtual void UpdateState()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void EndState()
    {
        enemyBase.animator.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimationName(animBoolName);
    }

    public virtual void FinishAnimationTrigger()
    {
        animationTriggerCalled = true;
    }
}
