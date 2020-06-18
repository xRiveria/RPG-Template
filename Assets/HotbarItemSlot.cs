using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HotbarItemSlot : MonoBehaviour
{
    [Header("Slot Properties")]
    public Image slotImage;
    public Text slotAmountText;

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
            slotImage.sprite = slotItem.GetItemSprite();
        }
    }



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

}
