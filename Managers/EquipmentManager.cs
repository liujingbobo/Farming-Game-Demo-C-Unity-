using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keep track of what's been equipped, assign its item, slotindex, itemType and equippedId
//Active controller specified by each item
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public Item equippedItem;
    public int slotIndex;
    public Inventory inventory;
    public ItemType itemtype;
    public int equippedId;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        UnEquipItem();
    }

    public void EquipItem(Item item, int num)
    {
        if (item != null)
        {
            equippedItem = item;
            slotIndex = num;
            itemtype = item.itemType;
            equippedId = item.id;
            PlayerController.instance.UpdateController(item.itemType);
        }
        else
        {
            UnEquipItem();
        }
    }

    public void UnEquipItem()
    {
        equippedItem = null;
        slotIndex = -1;
        itemtype = ItemType.NONE;
        equippedId = -1;
        PlayerController.instance.UpdateController(ItemType.NONE);
    }
}
