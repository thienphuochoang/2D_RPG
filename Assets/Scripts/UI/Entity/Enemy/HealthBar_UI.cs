using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Enemy _enemy;
    private Slider _slider;
    private CharacterStats _enemyStats;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _slider = GetComponentInChildren<Slider>();
        _enemyStats = _enemy.GetComponent<CharacterStats>();
        _enemy.OnFlipped += FlipUI;
        _enemyStats.OnHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void FlipUI() => this.transform.Rotate(0, 180, 0);

    private void UpdateHealthUI()
    {
        _slider.maxValue = _enemyStats.GetMaxHealthValue();
        _slider.value = _enemyStats.currentHealth;
    }
    private void OnDestroy()
    {
        _enemy.OnFlipped -= FlipUI;
        _enemyStats.OnHealthChanged -= UpdateHealthUI;
    }
}
