using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChange;
    }

    void OnEquipmentChange(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.AddModifier(oldItem.armorModifier);
            damage.AddModifier(oldItem.damageModifier);
        }
    }
}
