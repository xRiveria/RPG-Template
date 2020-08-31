using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
// - Right clicking on equipment slot to unequip == Main Unequip function.
// - Using Canvas grouping.
// - Make sure to completely finish the system before moving on.
// - Skills, Potions, Crafting etc.


public class InventorySystem : MonoBehaviour, IItemStorage
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

    public void PopulateWithRandomItem()
    {
        int random = Random.Range(0, randomItems.Count);
        AcceptItem(randomItems[random]);
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

        //
        foreach (InventoryItemSlot itemSlot in inventoryItemSlots)
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

        Debug.Log("Inventory Full!");
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

    public void RemoveItem(ItemTemplate itemToRemove)
    {

    }
}


