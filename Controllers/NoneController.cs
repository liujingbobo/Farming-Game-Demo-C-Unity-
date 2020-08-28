using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneController : LeftCubeController { 
    public Camera cam;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                if (Vector3.Distance(gameObj.transform.position, transform.position) < distance)
                {
                    if(gameObj.GetComponent<Ground>().isOccupied && gameObj.GetComponent<Ground>().placedType == PlacedType.SEED)
                    {
                        if(gameObj.transform.GetComponentInChildren<SeedState>().readyForHarvest)
                        {
                            gameObj.transform.GetComponentInChildren<SeedState>().Harvest();
                        }
                    }
                }
            }
        }
    }
}
