using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public int inventoryLimit;
    public List<InventoryItem> itemList = new List<InventoryItem>(30);
}

[Serializable]
public class InventoryItem
{
    public Item item;
    public int itemAmount;
}


