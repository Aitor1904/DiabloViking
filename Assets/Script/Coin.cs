using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Coin")]
public class Coin : Item
{
    public override void Use()
    {
        base.Use();
        GameManager.Instance.GetComponent<ScoreManager>().AddScore(1);
    }

    /*
    public override void Interact()
    {
        base.Interact();

        GameManager.Instance.GetComponent<ScoreManager>().ModifyScore(50);
        Debug.Log("Test");
    }*/
}
