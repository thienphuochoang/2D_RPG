using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;
using Cinemachine;
using TMPro;

public class EntityEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float flashDuration = 0.2f;
    [SerializeField]
    private Material _hitMat;
    
    private Material _originalMat;

    [Header("Screen shake FX")]
    [SerializeField]
    private CinemachineImpulseSource _screenShake;
    [SerializeField]
    private float _shakeMultiplier;
    [SerializeField]
    private Vector3 _shakePower;
    
    [Header("Hit FX")]
    [SerializeField] private GameObject _hitFX01Prefab;
    [SerializeField] private GameObject _hitFX02Prefab;

    [Header("Slash FX")]
    [SerializeField] private GameObject _slashFX01Prefab;
    [SerializeField] private GameObject _slashFX02Prefab;

    [Header("Counter FX")]
    [SerializeField]
    private GameObject _counterHixFXPrefab;
    
    [Header("Popup Text FX")]
    [SerializeField]
    private GameObject _popupTextPrefab;

    private Player _player;
    private void Start()
    {
        _player = PlayerManager.Instance.player;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMat = _spriteRenderer.material;
        _screenShake = GetComponent<CinemachineImpulseSource>();
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

    public void ShakeScreen()
    {
        _screenShake.m_DefaultVelocity =
            new Vector3(_shakePower.x * _player.facingDirection, _shakePower.y) * _shakeMultiplier;
        _screenShake.GenerateImpulse();
    }

    public void GenerateCounterFX(Transform targetTransform)
    {
        float xPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float yPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float zRotation = UnityEngine.Random.Range(-15, 15);
        GameObject newCounterHitFX = null;
        if (targetTransform.GetComponentInParent<Player>().facingDirection == 1)
        {
            newCounterHitFX = Instantiate(_counterHixFXPrefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newCounterHitFX.transform.Rotate(new Vector3(0,0, zRotation));
        }
        else
        {
            newCounterHitFX = Instantiate(_counterHixFXPrefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newCounterHitFX.transform.Rotate(new Vector3(0,0, 90 + zRotation));
        }
        
        //newCounterHitFX.transform.Rotate(new Vector3(0,0, zRotation));
        Destroy(newCounterHitFX, 0.2f);
    }

    public void GenerateHitFX(Transform targetTransform)
    {
        float xPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float yPosition = UnityEngine.Random.Range(-0.5f, 0.5f);
        float zRotation = UnityEngine.Random.Range(-90, 90);
        int whichHitFX = UnityEngine.Random.Range(0, 10);
        GameObject newHitFX = null;
        if (whichHitFX <= 5)
        {
            newHitFX = Instantiate(_hitFX01Prefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newHitFX.transform.Rotate(new Vector3(0,0, zRotation));
        }
        else
        {
            float yRotation = 0;
            zRotation = UnityEngine.Random.Range(-45, 45);
            if (GetComponent<Entity>().facingDirection == -1)
                yRotation = 180;
            Vector3 hitFXRotation = new Vector3(0, yRotation, zRotation);
            newHitFX = Instantiate(_hitFX02Prefab, targetTransform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            newHitFX.transform.Rotate(hitFXRotation);
        }
        Destroy(newHitFX, 0.5f);
    }

    public void CreatePopupText(string textContent)
    {
        float randomX = UnityEngine.Random.Range(-1, 1);
        float randomY = UnityEngine.Random.Range(3, 5);

        Vector3 positionOffset = new Vector3(randomX, randomY, 0);
        GameObject newPopupText =
            Instantiate(_popupTextPrefab, transform.position + positionOffset, Quaternion.identity);
        if (textContent.Length < 11)
            newPopupText.GetComponent<TextMeshPro>().fontSize =
                newPopupText.GetComponent<TextMeshPro>().fontSize * 2.5f;
        newPopupText.GetComponent<TextMeshPro>().text = textContent;
    }
}
