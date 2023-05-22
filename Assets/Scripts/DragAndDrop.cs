using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public bool dragged = false;
    public GameObject draggedObject;
    GameObject ObjectPut;
    public Camera cam;
    bool MovingBar;
    float lastRotation = 0;
    public float value;
    public GameObject Cursor;
    public bool CanMoveCursor = true;
    public RaycastHit lastHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            //Barre de chargement
            if (MovingBar)
            {
                //Quand l'objet est posé on va pouvoir faire tourner l'objet dans lequel il est introduit
                if (value < 2.29f)
                {
                    ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((lastHit.point - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                    ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);

                    //On va faire augmenter notre jauge ici
                    if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w > lastRotation || (lastRotation - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w > 1 && lastRotation > 0))
                    {
                        float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;

                        if (theValue < 0)
                        {
                            if(theValue > lastRotation)
                            {
                                theValue = lastRotation - theValue;
                            }
                            theValue *= -1;
                        }
                        else
                        {
                            if (theValue > lastRotation)
                            {
                                theValue = theValue - lastRotation;
                            }
                        }

                        Debug.Log("gain");
                        value += theValue;
                    }

                    //On va faire décrémenter notre jauge ici
                    else if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w != lastRotation)
                    {
                        float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;

                        if (theValue < 0)
                        {
                            if (theValue < lastRotation)
                            {
                                theValue =  theValue - lastRotation;
                            }
                        }
                        else
                        {
                            if (theValue < lastRotation)
                            {
                                theValue =   lastRotation - theValue;
                            }
                            theValue *= -1;
                        }

                        Debug.Log("lost");
                        value += theValue;
                    }
                    lastRotation = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                OnClicked();
            }

       
        

        if (Input.GetMouseButtonUp(0))
        {
            StopClick();
        }

    }

    public void DragSimple(RaycastHit hit)
    {
        GameManager.Instance.ObjectHover = hit.transform.gameObject;
    }
    public void DragOnUsableObject(RaycastHit hit)
    {
        GameManager.Instance.ObjectHover = hit.transform.gameObject;
        draggedObject.transform.position = hit.point;
    }

    //Permet d'avoir un curseur qui nous suit
    //Pour changer de curseur il faut changer la texture info en curseur
    //Ce move curseur sera utile UNIQUEMENT quand on va arrêter le curseur
    public void MoveCursor(RaycastHit hit)
    {
        //nous permet de rendre la souris invisible et non utilisable
       // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        if (CanMoveCursor)
        {
            Cursor.transform.position = new Vector3(hit.point.x, hit.point.y, 0f);
        }
    }
    void OnClicked()
    {
        if (GameManager.Instance.ObjectHover.tag == "Object")
        {
            draggedObject = GameManager.Instance.ObjectHover;
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

        //Nous fera bouger la barre de chargement
        else if (GameManager.Instance.ObjectHover.tag == "Bar")
        {
            MovingBar = true;
        }

        //Donne le nom de l'UI que l'on a cliqué dessus
        else if (GameManager.Instance.ObjectHover.tag == "Simon")
        {
            GetComponent<Simon>().AddToComparative(GameManager.Instance.ObjectHover.name);
        }
    }
    void StopClick()
    {
        MovingBar = false;
        if (draggedObject != null)
        {
            if (draggedObject.GetComponent<ObjectToDrag>() != null)
            {
                if (draggedObject.GetComponent<ObjectToDrag>().canPutObject && GameManager.Instance.ObjectHover == draggedObject.GetComponent<ObjectToDrag>().objectToPutOn)
                {
                    draggedObject.transform.position = new Vector3(GameManager.Instance.ObjectHover.transform.position.x, GameManager.Instance.ObjectHover.transform.position.y, GameManager.Instance.ObjectHover.transform.position.z - 1);
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
