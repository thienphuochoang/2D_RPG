using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
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
