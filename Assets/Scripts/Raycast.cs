using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    public bool DraggedAnObject;
    public LayerMask layersToHit;
    public RaycastHit HitToStopMouse;

    public List<Transform> spawnPoint;
    public GameObject pointOn;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (!GetComponent<DragAndDrop>().dragged)
            {
                GetComponent<DragAndDrop>().DragSimple(hit);
            }
            /*string tagFromHit = hit.transform.gameObject.name;
            if (tagFromHit == "On")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Count)];
                    GameObject instantiated = Instantiate(pointOn);
                    instantiated.transform.position = randomPoint.position;
                    Destroy(this.gameObject);  

                }
            }*/
        }

        //Utilise un raycast limité par un certain layer pour éviter de prendre en compte tout les objets

        if (GetComponent<DragAndDrop>().dragged && Physics.Raycast(ray, out hit, 100, layersToHit) && GetComponent<DragAndDrop>().draggedObject != null)
        {
            GetComponent<DragAndDrop>().DragOnUsableObject(hit);
        }

        if (GetComponent<DragAndDrop>().nbrOfTimeWeTouch == 1)
        {
            HitToStopMouse = hit;
        }
        GetComponent<DragAndDrop>().lastHit = hit;



        /*    if (Physics2D.Raycast(ray, out hit, 10000))
            {
                if (!GetComponent<DragAndDrop>().dragged)
                {
                    GetComponent<DragAndDrop>().DragSimple(hit);
                }
            }

            //Utilise un raycast limité par un certain layer pour éviter de prendre en compte tout les objets

            if (GetComponent<DragAndDrop>().dragged && Physics.Raycast(ray, out hit, 100, layersToHit) && GetComponent<DragAndDrop>().draggedObject != null)
            {
                GetComponent<DragAndDrop>().DragOnUsableObject(hit);
            }

            if (GetComponent<DragAndDrop>().nbrOfTimeWeTouch == 1)
            {
                HitToStopMouse = hit;
            }
            GetComponent<DragAndDrop>().lastHit = hit;*/
    }

}

