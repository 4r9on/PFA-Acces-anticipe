using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningChest : MonoBehaviour
{
    public GameObject enterChest;

    public GameObject opening1;
    public GameObject opening2;
    public GameObject opening3;
    public GameObject opening4;

    public bool[] listLock = new bool [4];

    public int lastLock;

    public int nbForOpen;

    public GameObject lightMain;



    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Locker1") // si l'objet rentre en colition avec ce tag alors
        {
            listLock[0] = true; // l'élément de la liste passe à true  

            if (lastLock == 1 || 3 == lastLock) // si lastLock poséde l'une des deux valeur alors
            {
                listLock[lastLock] = true; // listLock garde l'index auquel il corespond activé

            }

            lastLock = 0; // on défit la valeur de lastLock
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

    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Locker1") // si l'objet rentre en colition avec ce tag alors
        {
            listLock[0] = true; // l'élément de la liste passe à true  

            if (lastLock == 1 || 3 == lastLock) // si lastLock poséde l'une des deux valeur alors
            {
                listLock[lastLock] = true; // listLock garde l'index auquel il corespond activé
            }

            lastLock = 0; // on défit la valeur de lastLock
        }
        else if (collision.gameObject.tag == "Locker2")
        {
            listLock[1] = true;

            if (lastLock == 0 || 2 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 1;

        }
        else if (collision.gameObject.tag == "Locker3")
        {
            listLock[2] = true;

            if (lastLock == 1 || 3 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 2;

        }
        else if (collision.gameObject.tag == "Locker4")
        {
            listLock[3] = true;

            if (lastLock == 0 || 3 == lastLock)
            {
                listLock[lastLock] = true;
            }

            lastLock = 3;

        }

        Open();

    }

    public void Open()
    {
        if (listLock[0] && listLock[1] == true && !opening1.activeInHierarchy) // on regerde si le premier et deuxième élément de la liste égal à true et si l'objet est désactiver
        {
            opening1.SetActive(true); // on activer le sprite qui montre la coupure
            nbForOpen++; 

            FalseLock1(); // on désactiver les élément de la liste activer
            Debug.Log("1a");
        }
        if (listLock[2] && listLock[1] == true && !opening2.activeInHierarchy)
        {
            opening2.SetActive(true);
            nbForOpen++;

            FalseLock2();
            Debug.Log("2a");

        }
        if (listLock[2] && listLock[3] == true && !opening3.activeInHierarchy)
        {
            opening3.SetActive(true);
            nbForOpen++;

            FalseLock3();
            Debug.Log("3a");

        }
        if (listLock[0] && listLock[3] == true && !opening4.activeInHierarchy)
        {
            opening4.SetActive(true);
            nbForOpen++;

            FalseLock4();
            Debug.Log("4a");

        }
        if (nbForOpen == 4)
        {
            enterChest.transform.position = new Vector3(0, 0, -1);
            Debug.Log("Open");

            enterChest.SetActive(false);
            GameManager.Instance.dAD.flashinglight.SetActive(true);
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
