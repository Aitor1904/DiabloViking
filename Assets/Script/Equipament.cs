using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipament")]
public class Equipament : Item
{
    public EquipamentSlot equipamentSlot;
    public SkinnedMeshRenderer prefab;
    public override void Use()
    {
        //RemoveFromInventory();
        base.Use();
    }
}

public enum EquipamentSlot
{
    Head, Chest, Legs, Weapon, Shield, Feet
}
