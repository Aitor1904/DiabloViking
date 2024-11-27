using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    //public GameObject targetObject;
    //GameObject[] currentObjects;

    public SkinnedMeshRenderer[] targetMesh;
    //SkinnedMeshRenderer[] currentMeshes; 

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //currentMeshes = new SkinnedMeshRenderer[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            oldItem.objectToActivate.SetActive(false);
            Inventory.instance.Add(oldItem);
        }

        if (onEquipmentChanged != null)
            onEquipmentChanged(newItem, oldItem);


        currentEquipment[slotIndex] = newItem;
        currentEquipment[slotIndex].objectToActivate.SetActive(true);
        Debug.Log(newItem.name + " equipped!");

        //GameObject arruntak 
        /*GameObject spawnedObject = Instantiate(newItem.prefab, newItem.whatToParentTo);
        currentObjects[slotIndex] = spawnedObject;*/

        //SkinnedMeshRender
        /*SkinnedMeshRenderer newMesh = Instantiate(newItem.mesh);
        newMesh.transform.parent = targetMesh[(int)newItem.equipSlot].transform;

        newMesh.bones = targetMesh[(int)newItem.equipSlot].bones;
        newMesh.rootBone = targetMesh[(int)newItem.equipSlot].rootBone;
        currentMeshes[slotIndex] = newMesh;*/
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            currentEquipment[slotIndex].objectToActivate.SetActive(false);
            //GameObject arruntak
            /*if (currentObjects[slotIndex] != null)
            {
                Destroy(currentObjects[slotIndex].gameObject);
            }*/

            //SkinnedMeshRenderrak
            /*if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }*/

            Equipment oldItem = currentEquipment[slotIndex];
            Inventory.instance.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
