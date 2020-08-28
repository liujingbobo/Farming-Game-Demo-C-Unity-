using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeController : LeftCubeController
{
    public Camera cam;
    public float distance;//The distance of the clos
    public int itemId;//Update onenable, which is everytime switch equipment
    public Action getMouseInput;

    public PlayerMovement playermovement;

    public List<Item> allHoe;

    public List<Transform> selectedList;

    public GameObject player;

    public int row;
    public int column;

    //The ground player standing on
    public int curGroundid;


    private void OnEnable()
    {
        itemId = EquipmentManager.instance.equippedId;
        SetAction();
        row = GroundManager.instance.row;
        column = GroundManager.instance.column;
    }

    private void SetAction()
    {
        if (itemId == allHoe[0].id)
        {
            getMouseInput = Hoe0;
        }
        else if (itemId == allHoe[1].id)
        {
            getMouseInput = Hoe1;
        }
        else if (itemId == allHoe[2].id)
        {
            getMouseInput = WateringCan2;
        }
    }

    private void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(player.transform.position, Vector3.down, out hitInfo, 5, 1 << 10))
        {
            curGroundid = hitInfo.collider.GetComponent<Ground>().groundid;
        }

        if (selectedList != null)
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i] != null)
                {
                    selectedList[i].GetComponent<Outline>().enabled = false;
                }
            }
            selectedList.Clear();
        }
        getMouseInput();
    }



    //Only water one ground around player
    public void Hoe0()
    {
        //Selection Part, pre select the ground that might watered
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;
            if ((gameObj.GetComponent<Ground>().groundid >= curGroundid - 1 && gameObj.GetComponent<Ground>().groundid <= curGroundid + 1) || (gameObj.GetComponent<Ground>().groundid >= curGroundid - row - 1 && gameObj.GetComponent<Ground>().groundid <= curGroundid - row + 1) || (gameObj.GetComponent<Ground>().groundid >= curGroundid + row - 1 && gameObj.GetComponent<Ground>().groundid <= curGroundid + row + 1))
            {
                selectedList.Add(gameObj.transform);
            }

            //if (gameObj.GetComponent<Ground>().groundType == GroundType.READY && (Vector3.Distance(gameObj.transform.position, transform.position) < distance))
            //    selectedList.Add(gameObj.transform);
            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }

        //If player click mouse, then water all ground in the selected list
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                if (selectedList[i].GetComponent<Ground>().groundType == GroundType.DRY || selectedList[i].GetComponent<Ground>().groundType == GroundType.GRASS)
                {
                    GroundManager.instance.SwapGround(selectedList[0].gameObject, GroundType.READY);
                }
            }
        }
    }

    //Water 3 in a row
    public void Hoe1()
    {
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;
        //if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        //{
        //    GameObject gameObj = hitInfo.collider.gameObject;
        //    if (gameObj.GetComponent<Ground>().groundType == GroundType.READY && (Vector3.Distance(gameObj.transform.position, transform.position) < distance))
        //        selectedList.Add(gameObj.transform);
        //    for (int i = 0; i < selectedList.Count; i++)
        //    {
        //        selectedList[i].GetComponent<Outline>().enabled = true;
        //    }
        //}

        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;
        //if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        //{
        //    GameObject gameObj = hitInfo.collider.gameObject;
        //    if (gameObj.GetComponent<Ground>().groundType == GroundType.READY && (Vector3.Distance(gameObj.transform.position, transform.position) < distance))
        //        selectedList.Add(gameObj.transform);
        //        selectedList.Add(GroundManager.instance.groundList[gameObj.GetComponent<Ground>().groundid + GroundManager.instance.row]);
        //    gameObj.GetComponent<Outline>().enabled = true;
        //}
    }

    public void WateringCan2()
    {

    }
}
