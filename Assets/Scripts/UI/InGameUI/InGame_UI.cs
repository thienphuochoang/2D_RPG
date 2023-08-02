using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Image _healthSlider;
    [SerializeField] private Image _manaSlider;

    [SerializeField] private Image dashImage;
    private float _dashCooldown;
    [SerializeField] private Image fireBulletSkillImage;
    private float _fireBulletSkillCooldown;
    [SerializeField] private Image explosionHoleSkillImage;
    private float _explosionHoleSkillCooldown;
    [SerializeField] private Image hpFlaskImage;
    private float _hpFlaskCooldown;
    [SerializeField] private Image manaFlaskImage;
    private float _manaFlaskCooldown;
    [SerializeField] private TextMeshProUGUI currentCoins;
    
    private void Start()
    {
        _playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        if (_playerStats != null)
        {
            _playerStats.OnHealthChanged += UpdateHealthUI;
            _playerStats.OnManaChanged += UpdateManaUI;
        }
        UpdateHealthUI();
        UpdateManaUI();
        UpdateCoinsUI();
        _dashCooldown = SkillManager.Instance.dash.coolDown;
        _fireBulletSkillCooldown = SkillManager.Instance.fireBulletSkill.coolDown;
        _explosionHoleSkillCooldown = SkillManager.Instance.explosionHoleSkill.coolDown;
        _hpFlaskCooldown = InventoryManager.Instance.hpFlaskCooldown;
        _manaFlaskCooldown = InventoryManager.Instance.manaFlaskCooldown;
        SkillManager.Instance.dash.OnDashSkillCoolDown += SetCoolDownOfDashImage;
        SkillManager.Instance.fireBulletSkill.OnFireBulletSkillCoolDown += SetCoolDownOfFireBulletImage;
        SkillManager.Instance.explosionHoleSkill.OnExplosionHoleSkillCoolDown += SetCoolDownOfExplosionHoleImage;
        InventoryManager.Instance.OnHPFlaskCoolDown += SetCoolDownOfHpImage;
        InventoryManager.Instance.OnManaFlaskCoolDown += SetCoolDownOfManaImage;
        PlayerManager.Instance.OnCoinsChanged += UpdateCoinsUI;
    }

    private void Update()
    {
        CheckCoolDownOf(dashImage, _dashCooldown);
        CheckCoolDownOf(fireBulletSkillImage, _fireBulletSkillCooldown);
        CheckCoolDownOf(explosionHoleSkillImage, _explosionHoleSkillCooldown);
        CheckCoolDownOf(hpFlaskImage, _hpFlaskCooldown);
        CheckCoolDownOf(manaFlaskImage, _manaFlaskCooldown);
        
    }
    private void UpdateHealthUI()
    {
        _healthSlider.fillAmount = (float)_playerStats.currentHealth / (float)_playerStats.GetMaxHealthValue();
    }
    private void UpdateManaUI()
    {
        _manaSlider.fillAmount = (float)_playerStats.currentMana / (float)_playerStats.GetMaxManaValue();
    }

    private void SetCoolDownOfDashImage()
    {
        if (dashImage.fillAmount <= 0)
        {
            dashImage.fillAmount = 1;
        }
    }
    
    private void SetCoolDownOfExplosionHoleImage()
    {
        if (explosionHoleSkillImage.fillAmount <= 0)
        {
            explosionHoleSkillImage.fillAmount = 1;
        }
    }
    
    private void SetCoolDownOfFireBulletImage()
    {
        if (fireBulletSkillImage.fillAmount <= 0)
        {
            fireBulletSkillImage.fillAmount = 1;
        }
    }
    private void SetCoolDownOfHpImage()
    {
        if (hpFlaskImage.fillAmount <= 0)
        {
            hpFlaskImage.fillAmount = 1;
        }
    }
    private void SetCoolDownOfManaImage()
    {
        if (manaFlaskImage.fillAmount <= 0)
        {
            manaFlaskImage.fillAmount = 1;
        }
    }
    

    public void CheckCoolDownOf(Image image, float cooldown)
    {
        if (image.fillAmount > 0)
        {
            image.fillAmount -= 1 / cooldown * Time.deltaTime;
        }
    }

    private void UpdateCoinsUI()
    {
        currentCoins.text = PlayerManager.Instance.GetCurrentCoins().ToString();
    }
}
