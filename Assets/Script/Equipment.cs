using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipament")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;


    //GameObject arruntak
    //public GameObject prefab;
    //public Transform whatToParentTo;

    //SkinnedMeshRenderrak
    //public SkinnedMeshRenderer mesh;
    public GameObject objectToActivate;
    public override void Use()
    {
        //Ekipatzeko logika
        EquipmentManager.Instance.Equip(this);
        objectToActivate.SetActive(true);
        RemoveFromInventory();
        base.Use();
    }
}

public enum EquipmentSlot
{
    Head, Chest, Legs, Weapon, Shield, Feet
}
