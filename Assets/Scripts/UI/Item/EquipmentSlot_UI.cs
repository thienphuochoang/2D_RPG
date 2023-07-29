using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot_UI : ItemSlot_UI
{
    public EquipmentType equipmentType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot: " + equipmentType.ToString();
    }
}
