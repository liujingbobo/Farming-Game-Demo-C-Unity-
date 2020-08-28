using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemOnWorld : MonoBehaviour
{
    public Item item;
    public Inventory PlayerInventory;
    public int amount;
    public Transform player;
    public float attraction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int temp = InventoryManager.instance.AddItem(item.id, amount);
            InventoryManager.instance.RefreshItem();
            if (temp == amount)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke("UnLockTrigger", 1f);
            }
            else if (temp > 0)
            {
                this.amount = temp;
                GameObject tempItem = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                tempItem.GetComponent<BoxCollider>().enabled = false;
                tempItem.GetComponent<ItemOnWorld>().player = other.transform;
                tempItem.GetComponent<ItemOnWorld>().StartAbsorb();
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke("UnLockTrigger", 1f);
            }
            else
            {
                player = other.transform;
                InvokeRepeating("Absorb", 0f, 0.02f);
            }
        }

    }

    public void StartAbsorb()
    {
        InvokeRepeating("Absorb", 0f, 0.02f);

    }

    public void Absorb()
    {
        this.transform.position = this.transform.position - (this.transform.position - player.transform.position) / attraction;
        attraction -= 0.5f;
        if (Vector3.Distance(transform.position, player.position) < 0.5)
        {
            Destroy(this.gameObject);
        }
    }

    public void UnLockTrigger()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }

    public void LockTrigger()
    {
        this.GetComponent<BoxCollider>().enabled = false;
    }

    public IEnumerator OnHarvest(Vector3 parentPos)
    {
        LockTrigger();
        float tempx = Random.Range(-1, 1);
        float tempz = Random.Range(-1, 1);
        //TODO make this animation look more realistic
        Vector3[] wayPoint = new Vector3[]{ new Vector3(tempx, 1, tempz), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(2 * tempx, 0.2f, 2 * tempz), new Vector3(2 * tempx, 1, 2 * tempz), new Vector3(2 * tempx, 1, 2 * tempz)};
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] += parentPos;
        }
        Tween myTween = transform.DOLocalPath(wayPoint, 1, PathType.CubicBezier, PathMode.Full3D, 5, null);
        yield return myTween.WaitForCompletion();
        UnLockTrigger();
    }
}
