using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using TMPro.EditorUtilities;

public class BaseItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Slot Properties")]
    public Image slotImage;
    private Image slotImageReplica;

    private ItemTemplate slotItem;
    public ItemTemplate SlotItem
    {
        get
        {
            return slotItem;
        }
        set
        {
            slotItem = value;
            if (slotItem != null)
            {
                slotImage.sprite = slotItem.GetItemSprite();
            }
        }
    }

    [Header("Slot Properties")]
    public Text slotAmountText;

    private uint slotItemAmount;
    public uint SlotItemAmount
    {
        get
        {
            return slotItemAmount;
        }
        set
        {
            slotItemAmount = value;
            if (slotItemAmount > 1)
            {
                slotAmountText.text = slotItemAmount.ToString();
                slotImage.gameObject.SetActive(true);
                slotAmountText.gameObject.SetActive(true);
            }
            else if (slotItemAmount is 1)
            {
                slotImage.gameObject.SetActive(true);
                slotAmountText.gameObject.SetActive(false);
            }
            else
            {
                slotImage.gameObject.SetActive(false);
                slotAmountText.gameObject.SetActive(false);
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        slotImageReplica = Instantiate(slotImage, transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        slotImageReplica.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<BaseItemSlot>())
        {
            BaseItemSlot itemSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<BaseItemSlot>();

            if (itemSlot is EquipmentItemSlot)
            {
                InventorySystem.inventorySystem.EquipItem(itemSlot as EquipmentItemSlot, this, SlotItem);
            }
            else
            {
                SwapItems(itemSlot);
            }
        }

        Destroy(slotImageReplica.gameObject);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (slotItem != null && slotItem.GetItemCategory() == ItemCategories.Equipment)
            {
                foreach (EquipmentItemSlot equipmentSlot in InventorySystem.inventorySystem.equipmentItemSlots)
                {
                    if (equipmentSlot.slotEquipmentType == slotItem.GetItemType())
                    {
                        InventorySystem.inventorySystem.EquipItem(equipmentSlot, this, slotItem);
                        return;
                    }
                }
            }
        }
    }

    public void SwapItems(BaseItemSlot itemSlot)
    {

        //If the other slot is empty.
        if (itemSlot.SlotItem == null)
        {
            itemSlot.SlotItem = slotItem;
            itemSlot.SlotItemAmount = slotItemAmount;

            SlotItem = null;
            SlotItemAmount = 0;
           
        }

        else //If the other slot isn't empty.
        {
            ItemTemplate itemToSwap = slotItem;
            uint itemToSwapAmount = slotItemAmount;

            //Sets this slot's item to the other ones'.
            SlotItemAmount = itemSlot.SlotItemAmount;
            SlotItem = itemSlot.SlotItem;

            //Set to other slot's items to the stuff saved.
            itemSlot.SlotItemAmount = itemToSwapAmount;
            itemSlot.SlotItem = itemToSwap;
        }
    }

}
