using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipament")]
public class Equipament : Item
{
    public EquipamentSlot equipSlot;

    public GameObject prefab;
    public Transform whatToParentTo;

    public SkinnedMeshRenderer mesh;
    public override void Use()
    {
        EquipamentManager.Instance.Equip(this);
        RemoveFromInventory();
        base.Use();
    }
}

public enum EquipamentSlot
{
    Head, Chest, Legs, Weapon, Shield, Feet
}
