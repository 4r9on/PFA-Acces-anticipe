using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool dragged = false;
    GameObject draggedObject;
    public Camera cam;
    public LayerMask layersToHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (dragged && Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit))
        {
            draggedObject.transform.position = hitData.point;
        }
        else if (Physics.Raycast(ray, out hit, 10000) )
        {
            GameObject thisObject = hit.transform.gameObject;
            if (thisObject.tag == "Object")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dragged = true;
                    draggedObject = thisObject;
                }
            }

        }
        if(Input.GetMouseButtonUp(0)) 
        {
            dragged = false;
            draggedObject = null;
        }

    }


}
