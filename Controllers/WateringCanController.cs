using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class WateringCanController : LeftCubeController
{
    public Camera cam;
    public float distance;//The distance of the clos
    public int itemId;//Update onenable, which is everytime switch equipment
    public Action getMouseInput;
    public PlayerMovement playermovement;

    public List<Item> allwateringcan;

    public List<Transform> selectedList;

    public GameObject player;

    public GameObject rightCube;

    public int row;
    public int column;

    //The ground player standing on
    public int curGroundid;
    public List<int> groundidaround;
    public Vector3 originalPos;

    private void OnEnable()
    {
        SetAction();
        row = GroundManager.instance.row;
        column = GroundManager.instance.column;
    }

    //set action based on player's watering can level
    private void SetAction()
    {

        switch (PlayerInfo.instance.leftcube.watercanlvl)
        {
            case 0:
                getMouseInput = WateringCan0;
                break;
            case 1:
                getMouseInput = WateringCan1;
                break;
            case 2:
                getMouseInput = WateringCan2;
                break;
            case 3:
                getMouseInput = WateringCan3;
                break;
            case 4:
                getMouseInput = WateringCan4;
                break;
            case 5:
                getMouseInput = WateringCan5;
                break;
        }
    }

    //if(itemId == allwateringcan[0].id)
    //{
    //    getMouseInput = WateringCan0;
    //} else if(itemId == allwateringcan[1].id)
    //{
    //    getMouseInput = WateringCan1;
    //} else if(itemId == allwateringcan[2].id)
    //{
    //    getMouseInput = WateringCan2;
    //}

    private void Update()
    {
        groundidaround.Clear();

        RaycastHit hitInfo;

        if (Physics.Raycast(player.transform.position, Vector3.down, out hitInfo,5, 1 << 10))
        {
            curGroundid = hitInfo.collider.GetComponent<Ground>().groundid;
        }

        groundidaround.Add(curGroundid + 1);
        groundidaround.Add(curGroundid - 1);
        groundidaround.Add(curGroundid - 100);
        groundidaround.Add(curGroundid + 100);

        if (selectedList != null)
        {
            for (int i = 0; i < selectedList.Count; i++)
            {
                if(selectedList[i] != null)
                {
                    selectedList[i].GetComponent<Outline>().enabled = false;
                }
            }
            selectedList.Clear();
        }
        getMouseInput();
    }



    //Only water one ground around player
    public void WateringCan0()
    {
        //Selection Part, pre select the ground that might watered
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;
            if((gameObj.GetComponent<Ground>().groundid >= curGroundid - 1  && gameObj.GetComponent<Ground>().groundid <= curGroundid +1) || (gameObj.GetComponent<Ground>().groundid >= curGroundid - row - 1 && gameObj.GetComponent<Ground>().groundid <= curGroundid - row + 1) || (gameObj.GetComponent<Ground>().groundid >= curGroundid + row - 1 && gameObj.GetComponent<Ground>().groundid <= curGroundid + row + 1))
            {
                selectedList.Add(gameObj.transform);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    //Water 3 in a row
    public void WateringCan1()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;

            if (groundidaround.Contains(gameObj.GetComponent<Ground>().groundid))
            {
                GroundManager.instance.SelectSquareGround(selectedList, GetPositionType(curGroundid, gameObj.GetComponent<Ground>().groundid), 3, 1, gameObj.GetComponent<Ground>().groundid);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    public void WateringCan2()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;

            if (groundidaround.Contains(gameObj.GetComponent<Ground>().groundid))
            {
                GroundManager.instance.SelectSquareGround(selectedList, GetPositionType(curGroundid, gameObj.GetComponent<Ground>().groundid), 3, 2, gameObj.GetComponent<Ground>().groundid);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    //IEnumerator moverightcube(Transform target)
    //{
    //    originalPos = rightCube.transform.position;

    //    Tween mytween = rightCube.transform.DOMove(target.transform.position, 1f);

    //    yield return mytween.WaitForCompletion();

    //    GroundManager.instance.SwapGround(target.gameObject, GroundType.WET);

    //    mytween = rightCube.transform.DOMove(originalPos, 0.3f);
    //}

    public void WateringCan3()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;

            if (groundidaround.Contains(gameObj.GetComponent<Ground>().groundid))
            {
                GroundManager.instance.SelectSquareGround(selectedList, GetPositionType(curGroundid, gameObj.GetComponent<Ground>().groundid), 3, 3, gameObj.GetComponent<Ground>().groundid);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    public void WateringCan4()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;

            if (groundidaround.Contains(gameObj.GetComponent<Ground>().groundid))
            {
                GroundManager.instance.SelectSquareGround(selectedList, GetPositionType(curGroundid, gameObj.GetComponent<Ground>().groundid), 3, 6, gameObj.GetComponent<Ground>().groundid);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    public void WateringCan5()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, 1 << 10))
        {
            GameObject gameObj = hitInfo.collider.gameObject;

            if (groundidaround.Contains(gameObj.GetComponent<Ground>().groundid))
            {
                GroundManager.instance.SelectSquareGround(selectedList, GetPositionType(curGroundid, gameObj.GetComponent<Ground>().groundid), 5, 6, gameObj.GetComponent<Ground>().groundid);
            }

            for (int i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].GetComponent<Outline>().enabled = true;
            }
        }
    }

    public override void MouseDown()
    {
        Debug.Log("MouseDown!");
        for (int i = 0; i < selectedList.Count; i++)
        {
            if (selectedList[i].GetComponent<Ground>().groundType == GroundType.READY)
            {
                GroundManager.instance.SwapGround(selectedList[0].gameObject, GroundType.WET);
            }
        }
    }

    public PositionType GetPositionType(int cur, int selected)
    {
        if(cur/100 == selected / 100)
        {
            if(cur%100 > selected % 100)
            {
                return PositionType.LEFT;
            }
            else
            {
                return PositionType.RIGHT;
            }
        }else
        {
            if(cur/100 > selected / 100)
            {
                return PositionType.UP;
            }
            else
            {
                return PositionType.DOWN;
            }
        }
    }
}
