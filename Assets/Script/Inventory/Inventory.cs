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

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallBack;

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
            if(onItemChangeCallBack != null)
               onItemChangeCallBack.Invoke();
            //GameObject.Find("Canvas").GetComponent<InventoryUI>().UpdateUI();
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangeCallBack != null)
            onItemChangeCallBack.Invoke();
    }
}
