using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindow_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _craftButton;

    [SerializeField] private Image[] materialImages;

    private void EnableAllSubWindows(GameObject parentGameObject)
    {
        // Loop through all child objects of the parent game object
        foreach (Transform child in this.transform)
        {
            // Enable the child game object if it's disabled
            child.gameObject.SetActive(true);
        }
    }
    public void SetupCraftWindow(ItemData_Equipment data)
    {
        EnableAllSubWindows(this.gameObject);
        _craftButton.onClick.RemoveAllListeners();
        for (int i = 0; i < materialImages.Length; i++)
        {
            materialImages[i].color = Color.clear;
            materialImages[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < data.craftingMaterials.Count; i++)
        {
            if (data.craftingMaterials.Count > materialImages.Length)
            {
                Debug.LogWarning("You have more materials amount than you have material slots in craft window");
            }
            
            TextMeshProUGUI materialSlotText = materialImages[i].GetComponentInChildren<TextMeshProUGUI>();
            if (InventoryManager.Instance.stash.TryGetValue(data.craftingMaterials[i].itemData, out InventoryItem stashValue))
            {
                if (stashValue.stackSize < data.craftingMaterials[i].stackSize)
                {
                    materialSlotText.text = stashValue.stackSize.ToString() + "/" + data.craftingMaterials[i].stackSize.ToString();
                    materialSlotText.color = Color.red;
                }
                else
                {
                    materialSlotText.text = stashValue.stackSize.ToString() + "/" + data.craftingMaterials[i].stackSize.ToString();
                    materialSlotText.color = Color.white;
                }
            }
            else
            {
                materialSlotText.text = "0" + "/" + data.craftingMaterials[i].stackSize.ToString();
                materialSlotText.color = Color.red;
            }
            materialImages[i].sprite = data.craftingMaterials[i].itemData.itemIcon;
            materialImages[i].color = Color.white;
        }

        _itemIcon.sprite = data.itemIcon;
        _itemName.text = data.itemName;
        _itemDescription.text = data.GetDescription();
        
        _craftButton.onClick.AddListener(() => InventoryManager.Instance.CanCraft(data, data.craftingMaterials));
    }
}
