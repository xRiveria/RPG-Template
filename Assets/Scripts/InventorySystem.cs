using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
// - Right clicking on equipment slot to unequip == Main Unequip function.
// - Using Canvas grouping.
// - Make sure to completely finish the system before moving on.
// - Skills, Potions, Crafting etc.


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

    [Header("Equipment Item Slots")]
    public Transform equipmentItemSlotsParent;
    public List<EquipmentItemSlot> equipmentItemSlots = new List<EquipmentItemSlot>();

    public List<ItemTemplate> randomItems = new List<ItemTemplate>();

    public static InventorySystem inventorySystem;

    private void OnValidate()
    {
        PopulateHotbarItemSlots();
        PopulateInventoryItemSlots();
        PopulateEquipmentItemSlots();
    }

    private void Awake()
    {
        inventorySystem = this;
    }

    #region Auto Population
    public void PopulateWithRandomItem()
    {
        int random = Random.Range(0, randomItems.Count);
        CanAcceptItem(randomItems[random]);
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

    private void PopulateEquipmentItemSlots()
    {
        if (equipmentItemSlotsParent != null)
        {
            if (equipmentItemSlots.Count > equipmentItemSlotsParent.childCount)  //If the editor detects that there are more slots in the list than there should be, we clear the entire list completely.
            {
                equipmentItemSlots.Clear();
            }

            for (int i = 0; i < equipmentItemSlotsParent.childCount; i++)   //Repopulate the list with hotbar slots found with the parent.
            {
                if (!equipmentItemSlots.Contains(equipmentItemSlotsParent.GetChild(i).GetComponent<EquipmentItemSlot>()))
                {
                    equipmentItemSlots.Add(equipmentItemSlotsParent.GetChild(i).GetComponent<EquipmentItemSlot>());
                }
            }
        }
    }

    #endregion

    public bool CanAcceptItem(ItemTemplate itemToAccept)
    { 
        foreach (HotbarItemSlot itemSlot in hotbarItemSlots)
        {
            //If the item slot isn't empty...
            if (itemSlot.SlotItem != null)
            {
                //If the item in the item slot is the same as the incoming item...
                if (itemSlot.SlotItem == itemToAccept && itemSlot.SlotItemAmount < itemToAccept.GetItemMaxStackSize())
                {
                    IncrementItemSlotStack(itemSlot, itemToAccept);
                    return true;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                AddItemToNewSlot(itemSlot, itemToAccept);
                return true;
            }
        }

        foreach (InventoryItemSlot itemSlot in inventoryItemSlots)
        {
            //If the item slot isn't empty...
            if (itemSlot.SlotItem != null)
            {
                //If the item in the item slot is the same as the incoming item...
                if (itemSlot.SlotItem == itemToAccept && itemSlot.SlotItemAmount < itemToAccept.GetItemMaxStackSize())
                {
                    IncrementItemSlotStack(itemSlot, itemToAccept);
                    return true;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                AddItemToNewSlot(itemSlot, itemToAccept);
                return true;
            }
        }

        Debug.Log("Entire inventory is full...");
        return false;
    }

    private void AddItemToNewSlot(BaseItemSlot itemSlot, ItemTemplate itemToAccept)
    {
        itemSlot.SlotItem = itemToAccept;
        itemSlot.SlotItemAmount++;
    }

    private void IncrementItemSlotStack(BaseItemSlot itemSlot, ItemTemplate itemToAccept)
    {
        //If the item slot's current item amount is less than its maximum stack size...
        itemSlot.SlotItemAmount++;
    }

    public void EquipItem(EquipmentItemSlot equipmentSlot, BaseItemSlot itemSlot, ItemTemplate equipmentToAccept)
    {
        if (equipmentSlot.slotEquipmentType == equipmentToAccept.GetItemType())
        {
            equipmentSlot.SlotItem = equipmentToAccept;
            equipmentSlot.SlotItemAmount++;

            itemSlot.SlotItem = null;
            itemSlot.SlotItemAmount = 0;
        }
    }

}


