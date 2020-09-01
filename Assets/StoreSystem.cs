using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSystem : MonoBehaviour
{
    [Header("Store Properties")]
    [SerializeField] private List<ItemTemplate> itemsForSale = new List<ItemTemplate>();
    [SerializeField] private Transform itemsListParent;
    [SerializeField] private BaseItemSlot itemSlotPrefab;

    private void Awake()
    {
        
    }
}
