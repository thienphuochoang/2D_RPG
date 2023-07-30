using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Items/ItemEffect/Restore HP effect", menuName = "2D_RPG/Item Effect/Restore HP effect")]
public class RestoreHPItemEffect : ItemEffect
{
    [Range(0f, 1f)] [SerializeField] private float _healPercent;
    public override void ExecuteEffect()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * _healPercent);
        playerStats.IncreaseHealthBy(healAmount);
    }
}
