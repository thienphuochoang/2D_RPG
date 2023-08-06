using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine { get; private set; }
    public BoxCollider2D col { get; private set; }
    public string lastAnimationBoolName { get; private set; }
    private float defaultMoveSpeed;
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

    public event System.Action OnFlipped;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();
        col = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.UpdateState();
    }

    public virtual void AssignLastAnimationName(string animationBoolName)
    {
        lastAnimationBoolName = animationBoolName;
    }

    public override void Flip()
    {
        base.Flip();
        OnFlipped?.Invoke();
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

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            DisableCounterAttack();
            return true;
        }

        return false;
    }

    public void FreezeTime(bool isTimeFrozen)
    {
        if (isTimeFrozen)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            animator.speed = 1;
        }
    }

    public virtual IEnumerator FreezeTimeFor(float seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(seconds);
        FreezeTime(false);
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + playerCheckDistance * facingDirection, wallCheck.position.y));
    }
    public void TriggerAnimation() => stateMachine.currentState.FinishAnimationTrigger();
}
