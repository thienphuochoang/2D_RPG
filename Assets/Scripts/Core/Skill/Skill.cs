using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;
    public float coolDown;
    protected float coolDownTimer;
    [SerializeField]
    protected int manaCost;

    [SerializeField] private int _skillBaseDamage;
    public int skillBaseDamage => _skillBaseDamage;

    protected void Start()
    {
        player = PlayerManager.Instance.player;
    }

    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        bool canBeActivated = coolDownTimer < 0 && playerStats.currentMana > manaCost;
        if (canBeActivated == false)
            player.entityEffect.CreatePopupText("On Cooldown");
        return canBeActivated;
    }

    public virtual void UseSkill()
    {
        Activate();
        coolDownTimer = coolDown;
    }

    public virtual void Activate()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        playerStats.ConsumeMana(manaCost);
    }
}
