using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentItemSlot : BaseItemSlot
{
    [Header("Slot Properties")]
    public ItemTypes slotEquipmentType;
    [SerializeField] private Image equipmentPromptImage;

    public override uint SlotItemAmount 
    { 
        get => base.SlotItemAmount;
        set
        {
            base.SlotItemAmount = value;
            if (base.SlotItemAmount == 0)
            {
                equipmentPromptImage.gameObject.SetActive(true);
            }
            else if (base.SlotItemAmount > 0)
            {
                equipmentPromptImage.gameObject.SetActive(false);
            }
        }
        
    }

    public override void OnEquipmentRightClick(PointerEventData eventData)
    {
        if (SlotItem != null)
        {
            InventorySystem.inventorySystem.CanAcceptItem(SlotItem);
            SlotItem = null;
            SlotItemAmount = 0;
        }
    }
}
