using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Button removeButton;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void RemoveItemFromInventory()
    {
        if (item != null)
        {
            Inventory.instance.Remove(item);
            ClearSlot(); 
            GameObject.Find("Canvas").GetComponent<InventoryUI>().UpdateUI();
        }
    }

    public void Use()
    {
        if (item != null)
        {
            Debug.Log("Usando el ítem: " + item.name);
            item.Use();
            RemoveItemFromInventory();
        }
    }
}
