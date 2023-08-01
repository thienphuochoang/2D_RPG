using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatTooltip_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statDescriptionText;

    public void ShowStatToolTip(string inputText)
    {
        statDescriptionText.text = inputText;
        gameObject.SetActive(true);
    }
    
    public void HideStatToolTip()
    {
        statDescriptionText.text = "";
        gameObject.SetActive(false);
    }
}
