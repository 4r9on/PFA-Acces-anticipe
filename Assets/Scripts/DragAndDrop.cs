using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool dragged = false;
    GameObject draggedObject;
    GameObject ObjectPut;
    public Camera cam;
    public LayerMask layersToHit;
    bool isButtonDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (dragged && Physics.Raycast(ray, out hit, 100, layersToHit) && draggedObject != null)
        {
            draggedObject.transform.position = hit.point;
        }
        else if (Physics.Raycast(ray, out hit, 10000))
        {
            GameObject thisObject = hit.transform.gameObject;
            if (isButtonDown)
            {
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((hit.point - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);
            }
            else if (thisObject.tag == "Object")
            {

                if (Input.GetMouseButtonDown(0))
                {
                    draggedObject = thisObject;
                    if (draggedObject.GetComponent<ObjectToDrag>() != null)
                    {
                        if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity > 0)
                        {
                            draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity--;
                            if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity == 0)
                            {
                                draggedObject.GetComponent<Rigidbody>().useGravity = true;
                            }
                            draggedObject = null;
                        }
                        else
                        {
                            dragged = true;
                            if (draggedObject.GetComponent<ObjectToDrag>().isPut)
                            {

                                isButtonDown = true;
                                ObjectPut = draggedObject;
                            }
                            else
                            {
                                if (draggedObject.GetComponent<ObjectToDrag>().canPutObject)
                                {

                                }
                                else
                                {
                                    draggedObject.GetComponent<ObjectToDrag>().destroyOnGravity = false;
                                }
                            }
                        }

                    }

                }
            }


        }

        if (Input.GetMouseButtonUp(0))
        {
            if (draggedObject != null)
            {
                if (draggedObject.GetComponent<ObjectToDrag>() != null)
                {
                    if (draggedObject.GetComponent<ObjectToDrag>().canPutObject && hit.transform.gameObject == draggedObject.GetComponent<ObjectToDrag>().objectToPutOn)
                    {
                        draggedObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - 1);
                        draggedObject.GetComponent<Rigidbody>().useGravity = false;
                        draggedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        draggedObject.GetComponent<ObjectToDrag>().isPut = true;
                    }

                    StartCoroutine(draggedObject.GetComponent<ObjectToDrag>().BecomeDestroyable());
                }
            }
            dragged = false;
            draggedObject = null;

        }

    }



}
