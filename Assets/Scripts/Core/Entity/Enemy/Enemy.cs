using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 2f;
    public float idleTime = 2f;

    [Header("Player Detection Info")]
    [SerializeField]
    private float playerCheckDistance;
    [SerializeField]
    private LayerMask playerLayerMask;

    [Header("Attack info")] 
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector]
    public float lastTimeAttacked;
    public float battleTime;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned = false;
    [SerializeField]
    protected GameObject counterImage;

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerCheckDistance,
        playerLayerMask);

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.UpdateState();
    }

    public virtual void EnableCounterAttack()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void DisableCounterAttack()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    protected virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            DisableCounterAttack();
            return true;
        }

        return false;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + playerCheckDistance * facingDirection, wallCheck.position.y));
        
    }
    public void TriggerAnimation() => stateMachine.currentState.FinishAnimationTrigger();
}
