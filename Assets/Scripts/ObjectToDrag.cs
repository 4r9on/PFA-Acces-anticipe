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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BecomeDestroyable()
    {
        yield return new WaitForSeconds(1f);
        destroyOnGravity = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Permet de détruire certains objets quand on les laisse tomber
        if(collision.gameObject.tag == "Ground" && destroyOnGravity)
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
        Debug.Log("here we are");   
        GameManager.Instance.GetComponent<DragAndDrop>().OnClicked();
        //Output the name of the GameObject that is being clicked
       
        GameManager.Instance.GetComponent<Physics2DRaycaster>().eventMask = 118;
       
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
      //  Debug.Log(gameObject.name);
        GameManager.Instance.GetComponent<Physics2DRaycaster>().eventMask = int.MaxValue;
        if (gameObject.layer == 6)
        {
            GameManager.Instance.ObjectHover = gameObject;
        }
        else if(GameManager.Instance.GetComponent<DragAndDrop>().draggedObject == gameObject)
        {
            GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
        }
        
    }
}
