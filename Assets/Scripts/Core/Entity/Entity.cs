using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

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
    
    
    public bool IsGroundedDetected() =>Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayerMask);
    
    protected virtual void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
