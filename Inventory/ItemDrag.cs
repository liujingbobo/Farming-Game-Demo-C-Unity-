using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform originalParent;
    public Vector3 originalPos;
    public Inventory playerInventory;
    public int currentIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentIndex = this.transform.GetComponentInParent<Slot>().slotIndex;

        originalParent = transform.parent;
        originalPos = transform.position;

        transform.SetParent(transform.parent.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = eventData.position;

    }

    //If bag to qa, create a qa slot'

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.SetParent(originalParent);
            transform.position = originalPos;

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            InventoryManager.instance.RefreshItem();
            return;
        }

        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" || eventData.pointerCurrentRaycast.gameObject.name == "Amount")
        {
            int targetIndex = eventData.pointerCurrentRaycast.gameObject.transform.parent.transform.GetComponentInParent<Slot>().slotIndex;
            InventoryItem targetItem = playerInventory.itemList[targetIndex];
            InventoryItem currentItem = playerInventory.itemList[currentIndex];
            if (currentItem.item.itemName == targetItem.item.itemName && currentItem.itemAmount < currentItem.item.perSlotLimit && targetItem.itemAmount < targetItem.item.perSlotLimit)
            {
                int gap = targetItem.itemAmount + currentItem.itemAmount - targetItem.itemAmount;
                if (gap > 0)
                {
                    targetItem.itemAmount = targetItem.item.perSlotLimit;
                    currentItem.itemAmount = gap;
                }
                else
                {
                    targetItem.itemAmount += currentItem.itemAmount;
                    playerInventory.itemList[currentIndex].item = null;
                }
                InventoryManager.instance.RefreshItem();
                return;
            }
            else
            {
                //Switch position in inventory
                //eventData.pointerCurrentRaycast.gameObject - itemImage ------.parent - item
                SwapPositionInBag(currentIndex, eventData.pointerCurrentRaycast.gameObject.transform.parent.transform.GetComponentInParent<Slot>().slotIndex);

                //olditem's parent change to new parent
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.instance.RefreshItem();
                return;
            }
        }

        if (eventData.pointerCurrentRaycast.gameObject.name == "B_Slot(Clone)" || eventData.pointerCurrentRaycast.gameObject.name == "Q_Slot(Clone)")
        {
            SwapPositionInBag(currentIndex, eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotIndex);

            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            InventoryManager.instance.RefreshItem();
            return;
        }
        transform.SetParent(originalParent);
        transform.position = originalPos;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        InventoryManager.instance.RefreshItem();
    }

    public void SwapPositionInBag(int a, int b)
    {
        InventoryItem temp = playerInventory.itemList[b];

        playerInventory.itemList[b] = playerInventory.itemList[a];
        playerInventory.itemList[a] = temp;
    }

}
