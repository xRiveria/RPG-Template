using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Properties")]
    public List<string> itemTypes = new List<string>();

    [Header("Hotbar Item Slots")]
    public Transform hotbarItemSlotsParent;
    public List<HotbarItemSlot> hotbarItemSlots = new List<HotbarItemSlot>();

    public ItemTemplate test;

    private void OnValidate()
    {
        PopulateHotbarItemSlots();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AcceptItem(test);
        }
    }

    private void PopulateHotbarItemSlots()     
    {
        if (hotbarItemSlotsParent != null) 
        {
            if (hotbarItemSlots.Count > hotbarItemSlotsParent.childCount)  //If the editor detects that there are more slots in the list than there should be, we clear the entire list completely.
            {
                hotbarItemSlots.Clear();  
            }

            for (int i = 0; i < hotbarItemSlotsParent.childCount; i++)   //Repopulate the list with hotbar slots found with the parent.
            {
                if (!hotbarItemSlots.Contains(hotbarItemSlotsParent.GetChild(i).GetComponent<HotbarItemSlot>()))
                {
                    hotbarItemSlots.Add(hotbarItemSlotsParent.GetChild(i).GetComponent<HotbarItemSlot>());
                }
            }
        }
    }

    public void AcceptItem(ItemTemplate itemToAccept)
    {
        foreach (HotbarItemSlot itemSlot in hotbarItemSlots)
        {
            if (itemSlot.SlotItem != null && itemSlot.SlotItem == itemToAccept && itemSlot.SlotItemAmount < itemToAccept.GetItemMaxStackSize())
            {
                Debug.Log("Increment Item!");
                itemSlot.SlotItemAmount++;
                return;
            }
            else if (itemSlot.SlotItem == null)
            {
                Debug.Log("Insert Item!");
                itemSlot.SlotItem = itemToAccept;
                itemSlot.SlotItemAmount++;
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }    
}


