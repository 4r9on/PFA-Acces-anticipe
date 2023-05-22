using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public bool DraggedAnObject;
    public LayerMask layersToHit;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 10000))
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
        
        GetComponent<DragAndDrop>().MoveCursor(hit);
        GetComponent<DragAndDrop>().lastHit = hit;
    }
}

