using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float _existTime;
    private float _moveSpeed;

    public void Setup(float existTime, float moveSpeed)
    {
        _existTime = existTime;
        _moveSpeed = moveSpeed;
    }

    private void Update()
    {
        _existTime -= Time.deltaTime;
        if (_existTime < 0)
            Destroy(this.gameObject);
        transform.position += (Vector3)(transform.right * (_moveSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D hitEnemy)
    {
        if (hitEnemy.gameObject.GetComponent<Enemy>() != null)
            hitEnemy.gameObject.GetComponent<Enemy>().Damage();
    }
}
