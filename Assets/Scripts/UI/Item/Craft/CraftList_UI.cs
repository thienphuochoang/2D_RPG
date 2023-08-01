using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftList_UI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftSlotPrefab;

    [SerializeField] private List<ItemData_Equipment> craftEquipment;

    private void Start()
    {
        transform.parent.GetChild(0).GetComponent<CraftList_UI>().SetupCraftList();
        SetupDefaultCraftWindow();
    }

    public void SetupCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < craftEquipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            CraftSlot_UI craftSlot = newSlot.GetComponent<CraftSlot_UI>();
            craftSlot.SetupCraftSlot(craftEquipment[i]);
        }
    }

    private void SetupDefaultCraftWindow()
    {
        if (craftEquipment[0] != null)
            GetComponentInParent<MainUI>().craftWindowUI.SetupCraftWindow(craftEquipment[0]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetupCraftList();
    }
}
