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

    //public GameObject targetObject;
    //GameObject[] currentObjects;

    public SkinnedMeshRenderer targetMesh;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChange(Equipament newItem, Equipament oldItem);
    public OnEquipmentChange onEquipmentChange;
    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipamentSlot)).Length;
        currerntEquipament = new Equipament[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots]; 
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

        if (onEquipmentChange != null)
            onEquipmentChange(newItem, oldItem);

        currerntEquipament[slotIndex] = newItem;
        Debug.Log(newItem.name + "equipped");

        //GameObject spawnedObject = Instantiate(newItem.prefab, newItem.whatToParentTo
        //currentObjects[slotIndex] = spawnedObject;

        SkinnedMeshRenderer newMesh = Instantiate(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public void Unequip(int slotIndex)
    {
        if (currerntEquipament[slotIndex] != null)
        {
            /*if (currentObjects[slotIndex] != null)
            {
                Destroy(currentObjects[slotIndex].gameObject);
            }*/

            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipament oldItem = currerntEquipament[slotIndex];
            Inventory.instance.Add(oldItem);

            currerntEquipament[slotIndex] = null;
            if (onEquipmentChange != null)
                onEquipmentChange.Invoke(null, oldItem);
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currerntEquipament.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
