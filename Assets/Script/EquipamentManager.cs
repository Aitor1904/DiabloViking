using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipamentManager : MonoBehaviour
{
    #region Singleton
    public static EquipamentManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    Equipament[] currerntEquipament;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipamentSlot)).Length;
        currerntEquipament = new Equipament[numSlots];
    }

    public void Equip(Equipament newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipament oldItem = null;

        if (currerntEquipament[slotIndex] != null)
        {
            oldItem = currerntEquipament[slotIndex];
            Inventory.instance.Add(oldItem);        
        }
        currerntEquipament[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currerntEquipament[slotIndex] != null)
        {
            Equipament oldItem = currerntEquipament[slotIndex];
            Inventory.instance.Add(oldItem);

            currerntEquipament[slotIndex] = null;
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currerntEquipament.Length; i++)
        {
            Unequip(i);
        }
    }
}
