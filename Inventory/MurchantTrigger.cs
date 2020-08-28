using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MurchantTrigger : MonoBehaviour
{
    public GameObject hint;
    public GameObject murchantUI;
    public bool isOpened;
    public bool locked;

    public void Update()
    {

        if (!locked)
        {
            OpenMuchantUi();
        }
    }

    public void OpenMuchantUi()
    {
        //isOpened = murchantUI.activeSelf;
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //isOpened = !murchantUI.activeSelf;
        // murchantUI.SetActive(isOpened);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        locked = false;
        hint.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        locked = true;
        hint.SetActive(false);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    isOpened = murchantUI.activeSelf;
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        isOpened = !murchantUI.activeSelf;
    //        murchantUI.SetActive(isOpened);
    //    }
    //}

}
