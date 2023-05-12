using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningChest : MonoBehaviour
{
    public GameObject enterChest;

    public GameObject lockObject1;
    public GameObject lockObject2;
    public GameObject lockObject3;
    public GameObject lockObject4;

    public GameObject opening1;
    public GameObject opening2;
    public GameObject opening3;
    public GameObject opening4;

    public bool[] listLock = new bool [4];

    private int lastLock;

    private int nbForOpen;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Locker1")
        {
            Debug.Log("toucher");
            listLock[0] = true;

            if(lastLock == 1 || 3 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 0;

        }
        else if(collision.gameObject.tag == "Locker2")
        {
            listLock[1] = true;

            if (lastLock == 0 || 2 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 1;

        }
        else if(collision.gameObject.tag == "Locker3")
        {
            listLock[2] = true;

            if (lastLock == 1 || 3 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 2;

        }
        else if(collision.gameObject.tag == "Locker4")
        {
            listLock[3] = true;

            if (lastLock == 0 || 2 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 3;

        }

        Open();

    }

    public void Open()
    {
        if (listLock[0] && listLock[1] == true && !opening1.activeInHierarchy)
        {
            opening1.SetActive(true);
            nbForOpen++;

            FalseLock1();

        }
        if (listLock[2] && listLock[1] == true && !opening2.activeInHierarchy)
        {
            opening2.SetActive(true);
            nbForOpen++;

            FalseLock2();

        }
        if (listLock[2] && listLock[3] == true && !opening3.activeInHierarchy)
        {
            opening3.SetActive(true);
            nbForOpen++;

            FalseLock3();

        }
        if (listLock[0] && listLock[3] == true && !opening4.activeInHierarchy)
        {
            opening4.SetActive(true);
            nbForOpen++;

            FalseLock4();

        }

        if (nbForOpen == 4)
        {
            enterChest.transform.position = new Vector3(0, 0, -1);
        }
    }

    public void FalseLock1()
    {
        listLock[0] = false;
        listLock[1] = false;
    }

    public void FalseLock2()
    {
        listLock[1] = false;
        listLock[2] = false;
    }

    public void FalseLock3()
    {
        listLock[2] = false;
        listLock[3] = false;
    }

    public void FalseLock4()
    {
        listLock[0] = false;
        listLock[3] = false;
    }
}
