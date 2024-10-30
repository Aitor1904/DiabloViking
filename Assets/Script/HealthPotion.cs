using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/HealhtPosion")]
public class HealthPotion : Item
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.GetComponent<HealthManager>().ModifyHealth(50);        
    }
    /*public override void Interact()
    {
        base.Interact();

        GameManager.Instance.GetComponent<HealthManager>().ModifyHealth(50);
        Debug.Log("Test");
    }*/
}
