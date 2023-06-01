using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;


public class DragAndDrop : MonoBehaviour
{
    public bool dragged = false;
    public GameObject draggedObject;
    public GameObject ObjectPut;
    public Camera cam;
    bool MovingBar;
    float lastRotation = 0;
    public float value;
    public GameObject cursor;
    public bool CanMoveCursor = true;
    public RaycastHit lastHit;
    public int nbrOfTimeWeTouch;
    Color oldColor;
    public Slider slider;
    private SpriteRenderer sr;
    public SpriteRenderer mySpriteBar;
    public Texture2D tex;
    public Animator animator;
    public float posInit = 10000.0f;
    public float posMaxInit;
    public AnimatorController clip;
    public Texture2D tex;
    public float totalSliderValue;
    public GameObject engrenage;
    private Rigidbody2D _Rigidbody;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
     
        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "PlayButton")
            {
                oldColor = SimonUI.GetComponent<SpriteRenderer>().color;
            }
        }
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //Barre de chargement
        if (MovingBar)
        {
            //Quand l'objet est pose on va pouvoir faire tourner l'objet dans lequel il est introduit
            if (value < 2.29f && ObjectPut!= null)
            {
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);

                //On va faire augmenter notre jauge ici
                if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w > lastRotation || (lastRotation - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w > 1 && lastRotation > 0))
                {
                    float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;

                    if (theValue < 0)
                    {
                        if (theValue > lastRotation)
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

                //On va faire decrementer notre jauge ici
                else if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w != lastRotation)
                {
                    float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;

                    if (theValue < 0)
                    {
                        if (theValue < lastRotation)
                        {
                            theValue = theValue - lastRotation;
                        }
                    }
                    else
                    {
                        if (theValue < lastRotation)
                        {
                            theValue = lastRotation - theValue;
                        }
                        theValue *= -1;
                    }

                    Debug.Log("lost");
                    value += theValue;
                }
                lastRotation = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;
            }
            else if(animator != null)
            {
                animator.SetBool("Play", true);
                animator.SetBool("Logo", true);

            }
        }
        if(draggedObject != null)
        {
            if(draggedObject.tag == "Slider")
            {
               
                if (posInit == 10000.0f)
                {
                    posInit = draggedObject.transform.position.x;
                    draggedObject.transform.parent = draggedObject.transform.parent.transform.parent;
                }
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
                //Debug.Log(posInit);
                //Debug.Log(posMaxInit);
                draggedObject.transform.position = new Vector2( Camera.main.ScreenToWorldPoint(Input.mousePosition).x, draggedObject.transform.position.y);

                if(draggedObject.transform.position.x > posInit)
                {
                    draggedObject.transform.position = new Vector2(posInit, draggedObject.transform.position.y);

                }
                if (draggedObject.transform.position.x < posMaxInit)
                {
                    draggedObject.transform.position = new Vector2(posMaxInit, draggedObject.transform.position.y);

                }
                float maxValue = posInit - posMaxInit;
                totalSliderValue = 1-(posInit - draggedObject.transform.position.x) / maxValue;

                if (totalSliderValue >= 0.25f)
                {
                    mySpriteBar.sprite = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * totalSliderValue), tex.height), new Vector2(0.5f / totalSliderValue/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f), 100.0f);
                    ValueSlider();
                }




                float maxValue = posInit - posMaxInit;
                float pourcentageValue = 1 - (posInit - draggedObject.transform.position.x) / maxValue;
                //Aide de AgeTDev sur https://answers.unity.com/questions/1245599/how-to-get-all-sprites-used-in-a-2d-animator.html    
                
               
            }
            else
            {
                draggedObject.transform.position = new Vector2( Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
              
            }
        }
        /* if (Input.GetMouseButtonDown(0))
         {
             OnClicked();
         }




         if (Input.GetMouseButtonUp(0))
         {
             StopClick();
         }*/

    }

    public void DragSimple(RaycastHit hit)
    {
        GameManager.Instance.ObjectHover = hit.transform.gameObject;
    }
    public void DragOnUsableObject(RaycastHit hit)
    {
        GameManager.Instance.ObjectHover = hit.transform.gameObject;
        Debug.Log(hit.point);
        draggedObject.transform.position = hit.point;
    }

    //Permet d'avoir un curseur qui nous suit
    //Pour changer de curseur il faut changer la texture info en curseur
    //Ce move curseur sera utile UNIQUEMENT quand on va arreter le curseur

    public void OnClicked()
    {
        if (GameManager.Instance.ObjectHover.tag == "Object" || GameManager.Instance.ObjectHover.tag == "Hammer" || GameManager.Instance.ObjectHover.tag == "Slider")
        {
            draggedObject = GameManager.Instance.ObjectHover;
            if (draggedObject.GetComponent<ObjectToDrag>() != null && GameManager.Instance.ObjectHover.tag != "Slider")
            {
                //Permet de faire tomber l'UI lorsqu'on aura clicker suffisamment dessus
                if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity > 0)
                {
                    draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity--;
                    if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity == 0)
                    {
                        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                        foreach (Transform children in draggedObject.transform)
                        {
                            Debug.Log(children);
                            children.GetComponent<Rigidbody2D>().gravityScale = 1;
                        }
                    }
                    draggedObject = null;
                }
                else
                {
                    dragged = true;
                    if (draggedObject.GetComponent<ObjectToDrag>().canPutObject)
                    {

                    }
                    else if (ObjectPut.tag == "Simon" && draggedObject == ObjectPut)
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

        //Donne le nom de l'UI que l'on a clique dessus
        else if (GameManager.Instance.ObjectHover.tag == "Simon")
        {
            if (ObjectPut != null)
            {
                if (GameManager.Instance.ObjectHover == ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn)
                {
                    foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                    {
                        if (SimonUI.name == "PlayButton")
                        {
                            float timing = 0;
                            if (SimonUI.GetComponent<SpriteRenderer>().color == Color.white)
                            {
                                SimonUI.GetComponent<SpriteRenderer>().color = oldColor;
                            }
                            else
                            {
                                switch (nbrOfTimeWeTouch)
                                {
                                    case 0:
                                        Debug.Log("touche une fois");
                                        timing = 0.5f;
                                        break;
                                    case 1:
                                        Debug.Log("touche une seconde fois");
                                        timing = 0.4f;
                                        break;
                                    case 2:
                                        Debug.Log("Detruit l'UI");
                                        timing = 0.3f;
                                        //nous permet de rendre la souris invisible et non utilisable
                                        Cursor.lockState = CursorLockMode.Locked;
                                        
                                        //On va utiliser un faux curseur pour empecher le joueur de l'utiliser
                                        cursor.SetActive(true);
                                        cursor.transform.position = new Vector3(GetComponent<Raycast>().HitToStopMouse.point.x, GetComponent<Raycast>().HitToStopMouse.point.y, 0f);
                                        break;
                                }
                                StartCoroutine(TouchUI(SimonUI, timing));
                                SimonUI.GetComponent<SpriteRenderer>().color = Color.white;
                                nbrOfTimeWeTouch++;
                            }

                        }
                    }
                }
            }

            else
            {
                GetComponent<Simon>().AddToComparative(GameManager.Instance.ObjectHover.name);
            }

        }
        else if(GameManager.Instance.ObjectHover.tag == "ButtonON")
        {
            GameManager.Instance.AllText.GetComponent<ElevateText>().RotateIt();
        }
    }
    public void StopClick()
    {
        Debug.Log("eaz");
        MovingBar = false;
        if (draggedObject != null)
        {
            if (draggedObject.GetComponent<ObjectToDrag>() != null)
            {
                if (draggedObject.GetComponent<ObjectToDrag>().canPutObject && GameManager.Instance.ObjectHover == draggedObject.GetComponent<ObjectToDrag>().objectToPutOn)
                {
                    draggedObject.transform.position = new Vector3(GameManager.Instance.ObjectHover.transform.position.x, GameManager.Instance.ObjectHover.transform.position.y, GameManager.Instance.ObjectHover.transform.position.z);
                    draggedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    draggedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    draggedObject.GetComponent<ObjectToDrag>().isPut = true;
                    ObjectPut = draggedObject;

                }

                StartCoroutine(draggedObject.GetComponent<ObjectToDrag>().BecomeDestroyable());
            }
        }
        dragged = false;
        draggedObject = null;


    }

    IEnumerator TouchUI(GameObject SimonUI, float timing)
    {
        yield return new WaitForSeconds(timing);
        SimonUI.GetComponent<SpriteRenderer>().color = oldColor;
    }

    public void ValueSlider()
    {
        if(totalSliderValue <= 0.5f)
        {
           //_Rigidbody.AddForce 
            

        }
        else
        {
            
        }
    }
}
