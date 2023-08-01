using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSlot_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private MainUI _mainUI;
    [SerializeField] private string _statName;
    [SerializeField] private StatType _statType;
    [SerializeField] private TextMeshProUGUI _statValueText;
    [SerializeField] private TextMeshProUGUI _statNameText;
    [TextArea]
    [SerializeField] private string _statDescription;
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
        _mainUI = GetComponentInParent<MainUI>();
        UpdateStatValueUI();
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            _statValueText.text = playerStats.GetStat(_statType).GetValue().ToString();

            switch (_statType)
            {
                case StatType.Health:
                {
                    _statValueText.text = playerStats.GetMaxHealthValue().ToString();
                    break;
                }
                case StatType.PhysicalDamage:
                {
                    _statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();
                    break;
                }
                case StatType.Mana:
                {
                    _statValueText.text = playerStats.mana.GetValue().ToString();
                    break;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mainUI.statTooltipUI.ShowStatToolTip(_statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mainUI.statTooltipUI.HideStatToolTip();
    }
}
