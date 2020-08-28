using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class TestScript : MonoBehaviour
{
    public Seed abc;
    public Item item;
    public GameObject cube;

    public List<Item> itemList;

    public GameObject allGrounds;

    public List<GameObject> groundList;

    public GameObject dryPrefab;
    public GameObject wetPrefab;

    public RectTransform myui;

    public Camera cam;
    //x 230 y 109.04

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int u = 0; u < 100; u++)
            {
                Instantiate(dryPrefab, allGrounds.transform).transform.position = new Vector3(i, 0, u);
            }
        }
    }

    public void UpdateDate()
    {
        GameManager.instance.Update_ByDay();
        GroundManager.instance.Update_ByDay();
    }

    public void AddItemInInventory()
    {
        InventoryManager.instance.AddItem(abc.id, 1);
    }

    public void AddSeed()
    {
        Debug.Log("add seed");
        InventoryManager.instance.AddItem(abc.id, 1);
    }

    public void GetAsset()
    {
        string[] temp = AssetDatabase.FindAssets("t: Item");
        for (int i = 0; i < AssetDatabase.FindAssets("t: Item").Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(temp[i]);
            //Debug.Log(path);
            itemList.Add(AssetDatabase.LoadAssetAtPath<Item>(path));
        }
        //Debug.Log(Directory.GetFileSystemEntries());
        Debug.Log(AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/InventoryDemo/DemoBagSystem/FarmingSystem/Carrot/Ground").Length);
    }

    public void Dottest()
    {
        DOTween.Init();
        cube.transform.DOMove(new Vector3(17.79f, 3f, 42.56f), 3, false);
        cube.transform.DOScale(0, 2);
    }

    public void TestSave()
    {
        for (int i = 0; i < allGrounds.transform.childCount; i++)
        {
            groundList.Add(allGrounds.transform.GetChild(i).gameObject);
        }

        groundList = groundList.OrderBy(temp => temp.transform.position.x).ThenBy(temp => temp.transform.position.z).ToList();
    }

    public void DestroyByOne()
    {
        Destroy(groundList[3].gameObject);
        Destroy(groundList[5].gameObject);
        Destroy(groundList[7].gameObject);
    }
    
    public void MoveUI()
    {
        myui.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public void FoldUI()
    {
        StartCoroutine(Folduiele());
    }

    IEnumerator Folduiele()
    {
        for (int i = 0; i < myui.childCount; i++)
        {
            myui.GetChild(i).DOLocalMove(Vector2.zero, 0.2f);
            myui.GetChild(i).DOScale(Vector3.zero, 0.2f);

        }
        //foreach (var child in myui.GetComponentsInChildren<RectTransform>())
        //{
        //    child.DOLocalMove(Vector2.zero, 5f);
        //    //child.DOAnchorPos(Vector2.zero, 5f);
        //}
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < myui.childCount; i++)
        {
            myui.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(FoldUI2());
        
    }

    IEnumerator FoldUI2()
    {
       Tween fold = myui.DOAnchorPos(new Vector2(-667, -463), 0.2f);
        yield return fold.WaitForCompletion();
    }
    //x -667 y -463

    public void ShowUI()
    {
        Debug.Log(cam.WorldToViewportPoint(cube.transform.position));
        myui.transform.position = cam.WorldToScreenPoint(cube.transform.position);
        Time.timeScale = 0;
    }

    public void Mathtest()
    {
        Debug.Log(890 / 100); // 8
        Debug.Log(890 % 100); // 90
        Debug.Log(5 / 2);
    }
}


public class GroundInfooo
{
    public int abc;

    public GroundInfooo(int a)
    {
        abc = a;
    }
}


