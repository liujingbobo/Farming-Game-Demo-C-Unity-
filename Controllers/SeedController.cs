using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeedController : LeftCubeController
{
    public Camera cam;

    public float distance;

    private void Update()
    {
        GetMouseInput();
    }

    public void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo,1000f, 1 << 10))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (Vector3.Distance(gameObj.transform.position, transform.position) < distance)
                {
                    if (gameObj.GetComponent<Ground>().PlantSeed(Warehouse.instance.seedWarehouse[EquipmentManager.instance.equippedId]))
                    {
                        InventoryManager.instance.UseEquippedItem(1);
                    }
                }
            }
        }
    }
   
}
