using DG.Tweening.Core.Easing;
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
    public bool cog;
    public bool CD;
    public bool Moon;
    public bool child;
    public bool canSlide;
    public bool S2ATSlide;
    public bool Background;
    public bool wasGravited;

    private int click;
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
        if(cog && GameManager.Instance.dAD.draggedObject == gameObject)
        {
            MakeTheCogRoll(900);
            transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetChild(0).position = transform.localPosition;
        }
        Debug.Log(GameManager.Instance.Raycaster2D.eventMask.value);
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
            foreach (Transform children in transform)
            {
                children.parent = children.parent.parent;
                Debug.Log(children.parent.parent);
            }
        }
        if (painting && collision.gameObject.tag == "Ground")
        {

            GameManager.Instance.DotWeenShakeCamera(0.2f, 0.1f, 30);
            /*  children.GetComponent<Rigidbody2D>().gravityScale = 0;
              children.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
             */

        }
        //Permet de détruire certains objets quand on les laisse tomber
        else if (collision.gameObject.tag == "Ground" && destroyOnGravity)
        {
            if(BornWithoutGravity > 0)
            {
                    GameManager.Instance.DotWeenShakeCamera(0.2f, 0.1f, 30);
                
                if (gameObject == GameManager.Instance.StockCD)
                {
                    gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.StocksCD[1].GetComponent<SpriteRenderer>().sprite;
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                BornWithoutGravity--;
            }
            else
            {
                if(objectCreateAfterFalling != null)
                {
                    GameObject newObject = Instantiate(objectCreateAfterFalling);
                    newObject.transform.position = gameObject.transform.position;
                    newObject.GetComponent<ObjectToDrag>().objectToPutOn = objectToPutOn;
                    if (newObject.GetComponent<ObjectToDrag>().CD)
                    {
                        GameManager.Instance.ObjectHover = null;
                        GameManager.Instance.dAD.draggedObject = null;
                        GameManager.Instance.dAD.dragged = false;
                        GameManager.Instance.Raycaster2D.eventMask = 503;
                        GameManager.Instance.CD = newObject;
                        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.StocksCD[2].GetComponent<SpriteRenderer>().sprite;
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                        Destroy(this);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }

            }
            
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
        if (cog)
        {
            StartCoroutine(MakeItRoll());
        }
        if (child)
        {
            child = false;
            transform.SetParent(transform.parent.transform.parent, true);
        }
        if ((gameObject.tag == "UV" && GameManager.Instance.LampMask.enabled) || (gameObject.tag != "UV"))
        {
            GameManager.Instance.Raycaster2D.eventMask = 374;
            GameManager.Instance.GetComponent<DragAndDrop>().OnClicked();
            //Output the name of the GameObject that is being clicked

        }


    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (cog)
        {
            MakeTheCogRoll(0);
        }
        if (gameObject.tag == "Simon")
        {
            gameObject.GetComponent<Animator>().SetBool("IsClicked", false);

            if (CD || (GameManager.Instance.dAD.multipleTouchOnTableau2 && GameManager.Instance.ObjectHover.name=="Button_Pause"))
            {
                GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
            }
        }
        GameManager.Instance.Raycaster2D.eventMask = 503;

        if (gameObject.layer == 6)
        {
            

            Debug.Log(GameManager.Instance.GetComponent<DragAndDrop>().draggedObject);
            Debug.Log(gameObject);
            GameManager.Instance.ObjectHover = gameObject;



        }

        else if (GameManager.Instance.GetComponent<DragAndDrop>().draggedObject == gameObject)
        {
            Debug.Log(gameObject);
            GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
        }


    }

    void MakeTheCogRoll(float speedOfTheRoll)
    {
        transform.GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = speedOfTheRoll;
    }

    IEnumerator MakeItRoll()
    {
        MakeTheCogRoll(30);
        yield return new WaitForSeconds(0.2f);
        if (GetComponent<Rigidbody2D>().angularVelocity == 0)
        {
            MakeTheCogRoll(70);
            yield return new WaitForSeconds(0.2f);
            if (GetComponent<Rigidbody2D>().angularVelocity == 0)
            {
                MakeTheCogRoll(150);
                yield return new WaitForSeconds(0.2f);
                if (GetComponent<Rigidbody2D>().angularVelocity == 0)
                {
                    MakeTheCogRoll(400);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

    }

    public void shakeCameraAnim()
    {
        GameManager.Instance.DotWeenShakeCamera(0.2f, 0.6f, 30);
    }

    public void DestroyTheCog()
    {
        GameManager.Instance.cog3.SetActive(true);
        Destroy(GameManager.Instance.cog1);
    }


}
