using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Items/ItemEffect/Restore Mana effect", menuName = "2D_RPG/Item Effect/Restore Mana effect")]
public class RestoreManaItemEffect : ItemEffect
{
    [Range(0f, 1f)] [SerializeField] private float _manaPercent;
    public override void ExecuteEffect()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        int manaAmount = Mathf.RoundToInt(playerStats.GetMaxManaValue() * _manaPercent);
        playerStats.IncreaseManaBy(manaAmount);
    }
}
