using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Item itemInside;
    public Image itemImage;
    public Text itemAmount;
    public Image itemequipped;
    public string itemDescription;
    public int slotIndex;

    public Inventory inventory;
    
    public GameObject itemInSlot;

    public void SetUpSlot(int i)
    {
        if (inventory.itemList[i].item == null)
        {

            if (EquipmentManager.instance.slotIndex == i )
            {
                EquipmentManager.instance.UnEquipItem();
            }

            itemDescription = "";
            itemInSlot.SetActive(false);
            return;
        }
        else if (inventory.itemList[i].itemAmount <= 0)
        {
            inventory.itemList[i].item = null;
            itemDescription = "";
            itemInSlot.SetActive(false);
            return;
        }
        else
        {
            itemInside = inventory.itemList[i].item;
            itemImage.sprite = itemInside.itemImage;
            itemAmount.text = inventory.itemList[i].itemAmount.ToString();
            itemDescription = itemInside.itemDescription;
            if (slotIndex < InventoryManager.instance.qaSlotsCount)
            {
                itemequipped.gameObject.SetActive(slotIndex == EquipmentManager.instance.slotIndex);

            }
        }
    }

    public void OnClicked()
    {
        InventoryManager.instance.UpdateItemInfo(itemDescription);
    }

    public void OnSelected()
    {
        if (EquipmentManager.instance.slotIndex == slotIndex)
        {
            EquipmentManager.instance.UnEquipItem();
        }
        else
        {
            EquipmentManager.instance.EquipItem(itemInside, slotIndex);
        }
        InventoryManager.instance.RefreshItem();
    }

    public void UnEquip()
    {
        EquipmentManager.instance.UnEquipItem();
        InventoryManager.instance.RefreshItem();
    }

    public void UnEquipAnyway()
    {
        EquipmentManager.instance.UnEquipItem();
        InventoryManager.instance.RefreshItem();

    }
}
