using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject myBag;
    public bool isBagOpened;
    bool movementLock;
    bool inputLock;
    
    // Update is called once per frame
    void Update()
    {
        if(!movementLock)
            GetMovement();
        OpenMyBag();

    }

    private void LateUpdate()
    {
        if (!inputLock)
            GetInput();
    }

    public void LockMovement()
    {
        movementLock = true;
    }
    public void UnLockMovement()
    {
        movementLock = false;
    }
    public void LockInput()
    {
        inputLock = true;
    }
    public void UnLockInput()
    {
        inputLock = false;
    }

    void GetMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerController.instance.curController.MouseDown();
        }
    }
    void OpenMyBag()
    {
        isBagOpened = myBag.activeSelf;
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBagOpened = !isBagOpened;
            myBag.SetActive(isBagOpened);
            InventoryManager.instance.RefreshItem();
        }
    }


}
