using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float flashDuration = 0.2f;
    [SerializeField]
    private Material _hitMat;
    
    private Material _originalMat;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMat = _spriteRenderer.material;
    }

    public IEnumerator FlashFX()
    {
        _spriteRenderer.material = _hitMat;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.material = _originalMat;
    }

    public void BlinkRedColor()
    {
        if (_spriteRenderer.color != Color.white)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.red;
    }

    public void CancelRedBlink()
    {
        CancelInvoke();
        _spriteRenderer.color = Color.white;
    }
}
