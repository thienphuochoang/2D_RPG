using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public HitEffect hitEffect { get; private set; }
    public CharacterStats stats { get; private set; }
    public int facingDirection { get; private set; } = 1;
    private bool _facingRight = true;


    [Header("Collision Info")]
    [SerializeField]
    protected Transform groundCheck;
    [SerializeField]
    protected LayerMask groundLayerMask;
    [SerializeField] 
    protected float groundCheckDistance;
    [Space]
    [SerializeField]
    protected Transform wallCheck;
    [SerializeField]
    protected float wallCheckDistance;
    [Space]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Knockback info")]
    [SerializeField]
    protected Vector2 knockbackDirection;
    [SerializeField]
    protected float knockbackDuration;
    [SerializeField]
    protected float criticalKnockbackDuration;
    [SerializeField]
    protected Vector2 criticalKnockbackDirection;
    protected bool isKnocked;
    

    public bool IsGroundedDetected() =>Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayerMask);

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        hitEffect = GetComponent<HitEffect>();
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        if (wallCheck == null)
            wallCheck = this.transform;
    }

    protected virtual void Update()
    {
    }

    public void Flip()
    {
        facingDirection = facingDirection * -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void ControlFlip(float xVelocity)
    {
        if (xVelocity > 0 && !_facingRight)
        {
            Flip();
        }
        else if (xVelocity < 0 && _facingRight)
        {
            Flip();
        }
    }

    public void ResetZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = Vector2.zero;
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        ControlFlip(xVelocity);
    }

    public virtual void TriggerDamageEffect()
    {
        hitEffect.StartCoroutine(nameof(hitEffect.FlashFX));
        StartCoroutine(nameof(HitKnockBack));
    }

    public virtual void CriticalDamage()
    {
        hitEffect.StartCoroutine(nameof(hitEffect.FlashFX));
        StartCoroutine(nameof(CriticalHitKnockBack));
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    
    protected virtual IEnumerator CriticalHitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(criticalKnockbackDirection.x * -facingDirection, criticalKnockbackDirection.y);
        yield return new WaitForSeconds(criticalKnockbackDuration);
        isKnocked = false;
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
