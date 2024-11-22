using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : Interactable
{
    public Equipament equipament;
    private Transform whatToParenTo;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Inventory.instance.Add(equipament);
        equipament.whatToParentTo = this.whatToParenTo;
        Destroy(gameObject);
    }
}
