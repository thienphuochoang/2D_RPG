using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatSlot_UI : MonoBehaviour
{
    [SerializeField] private string _statName;
    [SerializeField] private StatType _statType;
    [SerializeField] private TextMeshProUGUI _statValueText;
    [SerializeField] private TextMeshProUGUI _statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + _statName;
        if (_statNameText != null)
        {
            _statNameText.text = _statName;
        }
    }

    private void Start()
    {
        UpdateStatValueUI();
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            _statValueText.text = playerStats.GetStat(_statType).GetValue().ToString();
        }
    }
}
