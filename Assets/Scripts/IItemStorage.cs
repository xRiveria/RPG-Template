using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemStorage
{
    void AcceptItem(ItemTemplate itemToAccept);
    void RemoveItem(ItemTemplate itemToRemove);
}
