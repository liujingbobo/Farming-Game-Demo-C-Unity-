using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemImage;
    public int perSlotLimit;
    public float purchasePrice;
    public float sellPrice;
    [TextArea]
    public string itemDescription;
    public ItemType itemType;
    [SerializeField]

    public override bool Equals(object other)
    {
        return other is Item item &&
            item.id == id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() + id.GetHashCode();
    }
}
