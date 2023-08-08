using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmProjectileController : MonoBehaviour
{
    private float _projectileSpeed = 10f;
    private string targetLayerName = "Player";
    private Vector2 _shootingDirection;
    private Rigidbody2D _rb;
    private bool _flipped;
    private CharacterStats _enemyGolemStats;
    private bool _canMove = true;
    private Animator _animator;
    private Player _player;
    private Enemy_Golem _enemyGolem;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_canMove)
        {
            _shootingDirection = _shootingDirection.normalized * _projectileSpeed;
            _rb.velocity = _shootingDirection;
        }
            
    }

    public void SetupProjectile(Vector2 shootingDir, CharacterStats enemyGolemStats)
    {
        _shootingDirection = shootingDir;
        _enemyGolemStats = enemyGolemStats;
        _enemyGolem = _enemyGolemStats.GetComponent<Enemy_Golem>();
        if (_enemyGolem.facingDirection == -1)
            this.transform.Rotate(0, 180, 0);
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            Debug.Log(other);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            //col.GetComponent<CharacterStats>()?.TakeDamage(_damage);
            _enemyGolemStats.DoDamage(col.GetComponent<CharacterStats>());
            StuckInto(col);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StuckInto(col);
        }
    }

    private void StuckInto(Collider2D col)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _canMove = false;
        _rb.isKinematic = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = col.transform;
        Invoke(nameof(BeginExplodeAnimation), 3f);
    }

    private void BeginExplodeAnimation()
    {
        _animator.SetTrigger("Explode");
    }

    private void Explode()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        _rb.isKinematic = false;
        _rb.constraints = RigidbodyConstraints2D.None;
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
