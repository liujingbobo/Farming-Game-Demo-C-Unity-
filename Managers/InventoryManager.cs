using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
//using System.Linq;
//using System.Text;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject bagSlot;
    public GameObject bagGrid;

    [Space]
    public int qaSlotsCount;
    public GameObject qa_Slot;
    public GameObject qa_Grid;
    [Space]
    public Inventory playerInventory;

    public List<GameObject> slots = new List<GameObject>();
    public Text itemInformation;

    public Item equippedItem;

    public Dictionary<int, Item> itemWarehouse;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }



    public void Start()
    {
        RefreshItem();
    }

    public int AddItem(int id, int amount)
    {
        Item item = Warehouse.instance.itemWarehouse[id];
        List<int> nullList = new List<int>();
        List<int> existItemList = new List<int>();

        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i].item != null)
            {
                //TODO
                if (playerInventory.itemList[i].item.itemName.Equals(item.itemName) && playerInventory.itemList[i].itemAmount < item.perSlotLimit)
                {
                    existItemList.Add(i);
                }
            }
            else
            {
                nullList.Add(i);
            }
        }

        if (existItemList.Count == 0 && nullList.Count == 0)
        {
            RefreshItem();
            return amount;
        }

        for (int i = 0; i < existItemList.Count; i++)
        {
            int index = existItemList[i];

            if (playerInventory.itemList[index].itemAmount < item.perSlotLimit)
            {
                if (amount + playerInventory.itemList[index].itemAmount <= item.perSlotLimit)
                {
                    playerInventory.itemList[index].itemAmount += amount;
                    RefreshItem();

                    return 0;
                }
                else
                {
                    int gap = item.perSlotLimit - playerInventory.itemList[index].itemAmount;
                    playerInventory.itemList[index].itemAmount = item.perSlotLimit;
                    amount -= gap;
                }
            }
        }

        if (nullList.Count == 0 || amount == 0)
        {
            RefreshItem();

            return amount;
        }

        for (int i = 0; i < nullList.Count; i++)
        {

            int index = nullList[i];

            playerInventory.itemList[index].item = item;

            if (amount > item.perSlotLimit)
            {
                playerInventory.itemList[index].itemAmount = item.perSlotLimit;
                amount -= item.perSlotLimit;
            }
            else
            {
                playerInventory.itemList[index].itemAmount = amount;
                RefreshItem();
                return 0;
            }
        }

        return amount;
    }

    public void RemoveItem(int index)
    {

    }

    public void RefreshItem()
    {
        if(EquipmentManager.instance.slotIndex != -1)
        {
            if (playerInventory.itemList[EquipmentManager.instance.slotIndex].itemAmount == 0)
            {
                EquipmentManager.instance.UnEquipItem();
            }
        }
        //Destroy All Item in slot first
        for (int i = 0; i < instance.qa_Grid.transform.childCount; i++)
        {
            if (qa_Grid.transform.childCount == 0)
                break;
            Destroy(instance.qa_Grid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.bagGrid.transform.childCount; i++)
        {
            if (bagGrid.transform.childCount == 0)
                break;
            Destroy(instance.bagGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (instance.slots.Count != instance.playerInventory.inventoryLimit)
            {
                if (i < qaSlotsCount)
                {
                    instance.slots.Add(Instantiate(instance.qa_Slot));
                    instance.slots[i].GetComponent<Slot>().slotIndex = i;
                }
                else
                {
                    instance.slots.Add(Instantiate(instance.bagSlot));
                    instance.slots[i].GetComponent<Slot>().slotIndex = i;
                }
            }
            else
            {
                if (i < qaSlotsCount)
                {
                    instance.slots[i] = Instantiate(instance.qa_Slot);
                    instance.slots[i].GetComponent<Slot>().slotIndex = i;
                }
                else
                {
                    instance.slots[i] = Instantiate(instance.bagSlot);
                    instance.slots[i].GetComponent<Slot>().slotIndex = i;
                }
            }

            if (i < qaSlotsCount)
            {
                instance.slots[i].transform.SetParent(instance.qa_Grid.transform);
                instance.slots[i].transform.localScale = Vector3.one;
                instance.slots[i].GetComponent<Slot>().SetUpSlot(i);
            }
            else
            {
                instance.slots[i].transform.SetParent(instance.bagGrid.transform);
                instance.slots[i].transform.localScale = Vector3.one;
                instance.slots[i].GetComponent<Slot>().SetUpSlot(i);
            }
        }
    }

    public void UpdateItemInfo(string description)
    {
        itemInformation.text = description;
        RefreshItem();
    }

    public void ClearItemInfo()
    {
        itemInformation.text = "";
    }

    public bool UseEquippedItem(int amount)
    {
        int slotIndex = EquipmentManager.instance.slotIndex;
        if(amount <= playerInventory.itemList[slotIndex].itemAmount)
        {
            playerInventory.itemList[slotIndex].itemAmount -= amount;
            RefreshItem();
            return true;
        }
        return false;
    }

    public bool SellItem(string name, int amount)
    {
        //TODO
        return true;
    }

    public void RemoveItem(int slotIndex, int amount)
    {
        //TODO;
    }

}


