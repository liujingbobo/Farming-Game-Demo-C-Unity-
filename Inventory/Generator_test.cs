using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_test : MonoBehaviour
{
    public Item thisItem;
    public GameObject itemOnWorld;
    public Inventory playerInventory;

    private void Awake()
    {
        InvokeRepeating("GenerateItem", 0f,3f);
    }
        
    public void GenerateItem()
    {
        GameObject newItem = Instantiate(itemOnWorld, this.transform.position + new Vector3(-1, 0, 2), Quaternion.identity);
        newItem.gameObject.SetActive(true);
    }
}
