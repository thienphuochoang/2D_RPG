using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public ItemTooltip_UI itemTooltipUI;
    public StatTooltip_UI statTooltipUI;
    public CraftWindow_UI craftWindowUI;

    public void SwitchTo(GameObject menu)
    {
        if (menu != null)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
            
    }
}
