using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Items/New Item", order = 1)]
public class ItemTemplate : ScriptableObject
{
    [Header("Item Properties")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private ItemTypes itemType;
    [SerializeField] private ItemCategories itemCategory;
    [SerializeField] private int itemMaxStackSize;

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetItemSprite()
    {
        return itemSprite; 
    }

    public ItemTypes GetItemType()
    {
        return itemType;
    }

    public ItemCategories GetItemCategory()
    {
        return itemCategory;
    }

    public int GetItemMaxStackSize()
    {
        return itemMaxStackSize;
    }
}
