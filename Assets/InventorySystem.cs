using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Properties")]
    public List<string> itemTypes = new List<string>();
    public List<string> equipmentTypes = new List<string>();

    [Header("Hotbar Item Slots")]
    public Transform hotbarItemSlotsParent;
    public List<HotbarItemSlot> hotbarItemSlots = new List<HotbarItemSlot>();

    [Header("Inventory Item Slots")]
    public Transform inventoryItemSlotsParent;
    public List<InventoryItemSlot> inventoryItemSlots = new List<InventoryItemSlot>();

    public ItemTemplate test;

    private void OnValidate()
    {
        PopulateHotbarItemSlots();
        PopulateInventoryItemSlots();
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

    private void PopulateInventoryItemSlots()
    {
        if (inventoryItemSlotsParent != null)
        {
            if (inventoryItemSlots.Count > inventoryItemSlotsParent.childCount)  //If the editor detects that there are more slots in the list than there should be, we clear the entire list completely.
            {
                inventoryItemSlots.Clear();
            }

            for (int i = 0; i < inventoryItemSlotsParent.childCount; i++)   //Repopulate the list with hotbar slots found with the parent.
            {
                if (!inventoryItemSlots.Contains(inventoryItemSlotsParent.GetChild(i).GetComponent<InventoryItemSlot>()))
                {
                    inventoryItemSlots.Add(inventoryItemSlotsParent.GetChild(i).GetComponent<InventoryItemSlot>());
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


