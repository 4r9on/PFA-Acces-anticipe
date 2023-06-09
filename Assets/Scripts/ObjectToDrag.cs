using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectToDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool destroyOnGravity;
    public bool canPutObject;
    public bool isPut;
    public int BornWithoutGravity;
    public GameObject objectToPutOn;
    public GameObject objectCreateAfterFalling;
    public bool painting;
    public bool CD;
    public bool child;
    public bool canSlide;
    public bool S2ATSlide;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (child)
        {
            transform.localPosition = new Vector2(0, 0);
        }
    }

    public IEnumerator BecomeDestroyable()
    {
        yield return new WaitForSeconds(1f);
        destroyOnGravity = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (painting)
        {
            objectToPutOn.SetActive(true);
            objectToPutOn = null;
        }
        if (painting && collision.gameObject.tag == "Ground")
        {
            foreach(Transform children in transform)
            {
                children.GetComponent<Rigidbody2D>().gravityScale = 0;
                children.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
            //Permet de détruire certains objets quand on les laisse tomber
        else if (collision.gameObject.tag == "Ground" && destroyOnGravity)
        {
            GameObject newObject = Instantiate(objectCreateAfterFalling);
            newObject.transform.position = gameObject.transform.position;
            newObject.GetComponent<ObjectToDrag>().objectToPutOn = objectToPutOn;
            Destroy(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GameManager.Instance.ObjectHover = null;
        //Output the following message with the GameObject's name
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GameManager.Instance.ObjectHover = gameObject;
        //Output to console the GameObject's name and the following message
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (child)
        {
            child = false;  
            transform.SetParent(transform.parent.transform.parent, true);
        }
        
        GameManager.Instance.GetComponent<DragAndDrop>().OnClicked();
        //Output the name of the GameObject that is being clicked
        GameManager.Instance.Raycaster2D.eventMask = 118;
       
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (gameObject.tag == "Simon")
        {
            gameObject.GetComponent<Animator>().SetBool("IsClicked", false);
            
            if (CD)
            {
                GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
            }
        }
        GameManager.Instance.Raycaster2D.eventMask = int.MaxValue;
        if (gameObject.layer == 6)
        {
            Debug.Log(GameManager.Instance.GetComponent<DragAndDrop>().draggedObject);
            Debug.Log(gameObject);
            GameManager.Instance.ObjectHover = gameObject;
        }
       
        else if(GameManager.Instance.GetComponent<DragAndDrop>().draggedObject == gameObject)
        {
            Debug.Log(gameObject);
            GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
        }
      

    }


}
