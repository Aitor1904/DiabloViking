using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : Interactable
{
    public Equipment equipment;
    public GameObject objectToActivate;
    //public Transform whatToParenTo;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        Inventory.instance.Add(equipment);
        equipment.objectToActivate = this.objectToActivate;
        //equipment.whatToParentTo = this.whatToParenTo;
        Destroy(gameObject);
    }
}
