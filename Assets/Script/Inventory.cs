using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    public void Awake()
    {
        instance = this;
    }
    #endregion

    public int inventorySpace = 10;
    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(item.showInInventory)
        {
            if(items.Count >= inventorySpace)
            {
                Debug.Log("Not enought room");
                return;
            }
            items.Add(item);
            GameObject.Find("Canvas").GetComponent<InventoryUI>().UpdateUI();
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
