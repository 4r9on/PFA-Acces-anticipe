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
    float lastRotation = 0;
    public int value;
    public GameObject Cursor;
    public bool CanMoveCursor = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        //Utilise un raycast limit� par un certain layer pour �viter de prendre en compte tout les objets
        if (dragged && Physics.Raycast(ray, out hit, 100, layersToHit) && draggedObject != null)
        {
            draggedObject.transform.position = hit.point;
        }

        else if (Physics.Raycast(ray, out hit, 10000))
        {
            GameObject thisObject = hit.transform.gameObject;
            if (isButtonDown)
            {
                //Quand l'objet est pos� on va pouvoir faire tourner l'objet dans lequel il est introduit
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((hit.point - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);

                //On va faire augmenter notre jauge ici
                if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w > lastRotation || (lastRotation -ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w   > 1 &&  lastRotation > 0))
                {
                    value++;
                }

                //On va faire d�cr�menter notre jauge ici
                else if(ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w != lastRotation)
                {
                    value--;
                }
                lastRotation = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (thisObject.tag == "Object")
                {
                    draggedObject = thisObject;
                    if (draggedObject.GetComponent<ObjectToDrag>() != null)
                    {
                        //Permet de faire tomber l'UI lorsqu'on aura clicker suffisamment dessus
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
                else if (thisObject.tag == "Bar")
                {
                    isButtonDown = true;
                }

                //Donne le nom de l'UI que l'on a cliqu� dessus
                else if(thisObject.tag == "Simon")
                {
                    GetComponent<Simon>().AddToComparative(thisObject.name);
                }
            }


        }

        //Permet d'avoir un curseur qui nous suit
        if (CanMoveCursor)
        {
            Cursor.transform.position = new Vector3(hit.point.x, hit.point.y, 0f);
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            isButtonDown = false;
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
                        ObjectPut = draggedObject;
                    }

                    StartCoroutine(draggedObject.GetComponent<ObjectToDrag>().BecomeDestroyable());
                }
            }
            dragged = false;
            draggedObject = null;

        }

    }



}
