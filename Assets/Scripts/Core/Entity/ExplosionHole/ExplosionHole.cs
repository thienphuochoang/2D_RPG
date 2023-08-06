using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionHole : MonoBehaviour
{
    private float _maxSize;
    private float _growSpeed;
    private bool _canGrow;
    private float _existDuration;
    private Animator _animator;
    public float explosionForce = 10f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public List<Enemy> enemyList;
    private static readonly int IsExplode = Animator.StringToHash("IsExplode");

    public void Setup(float maxSize, float growSpeed, bool canGrow, float existDuration)
    {
        _maxSize = maxSize;
        _growSpeed = growSpeed;
        _canGrow = canGrow;
        _existDuration = existDuration;
        AudioManager.Instance.PlaySFX(7);
    }
    private void Update()
    {
        _existDuration -= Time.deltaTime;
        if (_canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(_maxSize, _maxSize),
                _growSpeed * Time.deltaTime);
            foreach (var enemy in enemyList)
            {
                Rigidbody2D rb = enemy.GetComponentInChildren<Rigidbody2D>();
                Vector3 direction = (transform.position - rb.transform.position).normalized;
                rb.velocity = direction * _growSpeed;
            }
        }

        if (_existDuration < 0)
        {
            _canGrow = false;
            _animator.SetTrigger(IsExplode);
            AudioManager.Instance.StopSFX(7);
            Invoke(nameof(SelfDestroy), 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.FreezeTime(true);
            enemyList.Add(enemy);
        }
    }

    private void SelfDestroy()
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.FreezeTime(false);
            enemy.CriticalDamage();
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(PlayerManager.Instance.player.attackCheck.position, GetComponent<CircleCollider2D>().radius);
    }
}
