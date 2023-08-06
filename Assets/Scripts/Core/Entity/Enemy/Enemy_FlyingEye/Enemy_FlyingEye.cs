using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlyingEye : Enemy
{
    [Header("Enemy Flying Eye Projectile Info")]
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField] 
    private float _projectileSpeed;
    [Header("Enemy Flying Eye Player Detection View")]
    public float coneAngle = 90f;
    public float coneRange = 10f;
    
    [SerializeField]
    protected Player _player;
    
    public EnemyFlyingEyeIdleState idleState { get; private set; }
    public EnemyFlyingEyeMoveState moveState { get; private set; }
    public EnemyFlyingEyeBattleState battleState { get; private set; }
    public EnemyFlyingEyeAttackState attackState { get; private set; }
    public EnemyFlyingEyeStunnedState stunnedState { get; private set; }
    public EnemyFlyingEyeDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyFlyingEyeIdleState(this, stateMachine, "Idle", this);
        moveState = new EnemyFlyingEyeMoveState(this, stateMachine, "Move", this);
        battleState = new EnemyFlyingEyeBattleState(this, stateMachine, "Idle", this);
        attackState = new EnemyFlyingEyeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new EnemyFlyingEyeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new EnemyFlyingEyeDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start(); 
        _player = PlayerManager.Instance.player;
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Draw cone base (using the edge of the coneRange)
        Vector2 coneBaseLeft = Quaternion.Euler(0, 0, -coneAngle / 2) * transform.right * coneRange;
        Vector2 coneBaseRight = Quaternion.Euler(0, 0, coneAngle / 2) * transform.right * coneRange;

        // Draw the cone shape
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f); // Yellow with transparency
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + coneBaseLeft);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + coneBaseRight);

        // Draw the fan-shaped area
        Gizmos.DrawLine((Vector2)transform.position + coneBaseLeft, (Vector2)transform.position + coneBaseRight);
    }

    public bool IsPlayerDetectedInConeArea()
    {
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.right, directionToPlayer);

        return angleToPlayer <= coneAngle / 2.0f && directionToPlayer.magnitude <= coneRange;
    }

    public void TriggerRangeAttackAnimation()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, attackCheck.position, Quaternion.identity);
        Vector2 directionToPlayer = _player.transform.position - transform.position;
        //newProjectile.GetComponent<ProjectileController>().SetupProjectile(_projectileSpeed * facingDirection, stats);
        newProjectile.GetComponent<ProjectileController>().SetupProjectile(directionToPlayer, stats);
    }
}
