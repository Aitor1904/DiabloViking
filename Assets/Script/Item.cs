using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon;
    public bool showInInventory = true;

    public virtual void Use()
    {
        Debug.Log("Use metodoa erabilita");
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
