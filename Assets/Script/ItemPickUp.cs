using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;

    private ItemDetector itemDetector;
    private void Start()
    {
        itemDetector = FindObjectOfType<ItemDetector>();
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        //item.Use();
        Inventory.instance.Add(item);
        Destroy(gameObject);
        itemDetector.HideItemText();
    }
    private void OnMouseEnter()
    {
        if (itemDetector != null)
        {
            itemDetector.ShowItemText(item.name);
        }
    }

    private void OnMouseExit()
    {
        if (itemDetector != null)
        {
            itemDetector.HideItemText();
        }
    }
}
