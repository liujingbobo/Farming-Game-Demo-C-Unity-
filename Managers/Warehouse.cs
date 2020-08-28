using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    public static Warehouse instance;
    public Dictionary<int, Item> itemWarehouse;
    public Dictionary<int, Seed> seedWarehouse;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        itemWarehouse = new Dictionary<int, Item>();
        seedWarehouse = new Dictionary<int, Seed>();
        InitializeWarehouse();
    }   


    private void InitializeWarehouse()
    {
        string[] itemTemp = AssetDatabase.FindAssets("t: Item");
        for (int i = 0; i < itemTemp.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(itemTemp[i]);
            Item newItem = AssetDatabase.LoadAssetAtPath<Item>(path);

            try
            {
                itemWarehouse.Add(newItem.id, newItem);
            }
            catch
            {
                //The following part is only for testing use, should be deleted before game released
                Debug.Log("ERROR: Id " + newItem.id + " already existed in WareHouse.");
                Debug.Log("This Item is " + newItem.name + ", the exsiting item is " + itemWarehouse[newItem.id].name);
            }
        }

        string[] seedTemp = AssetDatabase.FindAssets("t: Seed");
        for (int i = 0; i < seedTemp.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(seedTemp[i]);
            Seed newSeed = AssetDatabase.LoadAssetAtPath<Seed>(path);
            seedWarehouse.Add(newSeed.id, newSeed);
        }

        LinkedList<int> temp = new LinkedList<int>();

        for (int i = 0; i < 1000; i++)
        {
            temp.AddLast(i);
        }

        temp.RemoveFirst();

        foreach (int key in itemWarehouse.Keys)
        {

            if (temp.Contains(key))
            {
                temp.Remove(key);
            }
        }

        foreach (int key in seedWarehouse.Keys)
        {
            if (temp.Contains(key))
            {
                temp.Remove(key);
            }
        }

        Debug.Log("We suggest assign this item as id: " + temp.First.Value + ", " + temp.First.Next.Value + ", "+temp.First.Next.Next.Value);
    }
}
