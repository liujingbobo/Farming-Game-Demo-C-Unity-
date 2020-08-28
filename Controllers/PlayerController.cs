using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LeftCubeController
{
    public static PlayerController instance;
    public PlayerMovement movement;
    public SeedController holdingseed;
    public LeftCubeController axe;
    public HoeController hoe;
    public WateringCanController wateringcan;
    public NoneController holdingnothing;

    public ItemType curControllerType;
    public LeftCubeController curController;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

    }

    public void UpdateController(ItemType itemtype)
    {
        GetController(curControllerType).enabled = false;
        GetController(itemtype).enabled = true;
        curControllerType = itemtype;
        curController = GetController(itemtype);
    }

    public LeftCubeController GetController(ItemType itemtype)
    {
        switch (itemtype)
        {
            case ItemType.NONE:
                return holdingnothing;
            case ItemType.AXE:
                return axe;
            case ItemType.HOE:
                return hoe;
            case ItemType.WATERINGCAN:
                return wateringcan;
            case ItemType.FURNITURE:
                break;
            case ItemType.ROD:
                break;
            case ItemType.SEED:
                return holdingseed;
            case ItemType.SWORD:
                break;
        }
        return null;
    }


}
