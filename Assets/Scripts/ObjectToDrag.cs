using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectToDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool destroyOnGravity;
    public bool canPutObject;
    public bool isPut;
    public int BornWithoutGravity;
    public int Digicode;
    public GameObject objectToPutOn;
    public GameObject objectCreateAfterFalling;
    public bool painting;
    public bool cog;
    public bool CD;
    public bool Moon;
    public bool child;
    public bool chest;
    public bool canSlide;
    public bool sliderRight;
    public bool S2ATSlide;
    public bool Background;
    public bool wasGravited;
    public bool isButtonPause;
    public bool vis;
    public bool canEnterInTheDoor;

    public List<GameObject> visList;

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
        if (cog)
        {
            if (GameManager.Instance.dAD.draggedObject == gameObject)
            {
                MakeTheCogRoll(900);
            }

            transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetChild(0).position = transform.position;
        }
    }

    public IEnumerator BecomeDestroyable()
    {
        yield return new WaitForSeconds(1f);
        destroyOnGravity = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "DeadZone" && collision.tag != "Explosion")
        {
            GameObject newExplosion = Instantiate(GameManager.Instance.Explosions[Random.Range(0, GameManager.Instance.Explosions.Count)]);
            newExplosion.GetComponent<SoundDesign>().PhaseOfSound = 1;
            GameManager.Instance.NewSound(newExplosion, newExplosion.GetComponent<SoundDesign>().TheVolume);
            newExplosion.transform.position = new Vector2(Random.Range(-4.0f, 4.0f), transform.position.y + 1);
            newExplosion.transform.eulerAngles = new Vector3(newExplosion.transform.eulerAngles.x, newExplosion.transform.eulerAngles.y, Random.Range(0, 360));
            StartCoroutine(DestroyExplosion(newExplosion));
            if (collision.gameObject.GetComponent<S2AT>())
            {
                GameManager.Instance.ChangeDialogueMoment();
                GameManager.Instance.blackSquare.SetActive(true);
            }
            Destroy(collision.gameObject);

        }

        
    }
    public IEnumerator DestroyExplosion(GameObject Explosion)
    {
        yield return new WaitForSeconds(1.12f);
        Destroy(Explosion);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Hammer" && collision.gameObject.tag == "JukeBoxCollider")
        {
            GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        }
        if (painting)
        {
            foreach (Transform children in transform)
            {
                children.parent = children.parent.parent;
                Debug.Log(children.parent.parent);
            }
        }
        if (painting && collision.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<SoundDesign>().PhaseOfSound = 2;
            GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
            GameManager.Instance.DotWeenShakeCamera(0.2f, 0.1f, 30);
            /*  children.GetComponent<Rigidbody2D>().gravityScale = 0;
              children.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
             */

        }
        //Permet de détruire certains objets quand on les laisse tomber
        else if (collision.gameObject.tag == "Ground" && destroyOnGravity)
        {
            if (gameObject == GameManager.Instance.StockCD)
            {
                GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
            }
            if (BornWithoutGravity > 0)
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
                if (gameObject == GameManager.Instance.StockCD || gameObject.GetComponent<ObjectToDrag>().painting)
                {
                    GameManager.Instance.DotWeenShakeCamera(0.1f, 0.4f, 30);
                }
                if (objectCreateAfterFalling != null)
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
        else if (collision.gameObject.tag == "Ground" && gameObject == GameManager.Instance.cog1)
        {
            gameObject.GetComponent<SoundDesign>().PhaseOfSound = 2;
            GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
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
        if ((gameObject.tag == "UV" && GameManager.Instance.LampMask.enabled) || (gameObject.tag != "UV"))
        {
            GameManager.Instance.Raycaster2D.eventMask = 374;
            GameManager.Instance.GetComponent<DragAndDrop>().OnClicked();
            //Output the name of the GameObject that is being clicked

        }


    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("unclick");
        if (cog)
        {
            MakeTheCogRoll(0);
        }
        if (gameObject.tag == "Simon")
        {
            gameObject.GetComponent<Animator>().SetBool("IsClicked", false);

            if (CD || (GameManager.Instance.dAD.multipleTouchOnTableau2 && GameManager.Instance.ObjectHover.name == "Button_Pause"))
            {
                GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
            }
        }
        GameManager.Instance.Raycaster2D.eventMask = 503;

        if (gameObject.layer == 6)
        {
            GameManager.Instance.ObjectHover = gameObject;

            if (gameObject.transform.childCount > 0)
            {
                if (gameObject.transform.GetChild(0).GetComponent<HammerPhysics>() != null)
                {
                    GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
                }
            }
        }

        else if (GameManager.Instance.GetComponent<DragAndDrop>().draggedObject == gameObject)
        {
            Debug.Log(gameObject);
            GameManager.Instance.GetComponent<DragAndDrop>().StopClick();
        }


    }

    void MakeTheCogRoll(float speedOfTheRoll)
    {
        //the false
        GetComponent<Rigidbody2D>().angularVelocity = speedOfTheRoll;
        //the true
        transform.GetChild(0).GetComponent<Rigidbody2D>().angularVelocity = speedOfTheRoll;
    }

    public IEnumerator MakeItRoll()
    {
        yield return new WaitForSeconds(gameObject.GetComponent<SoundDesign>().clipList1[0].length);
        if (gameObject.GetComponent<SoundDesign>().PhaseOfSound == 1)
        {
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }

    }

    public void shakeCameraAnim()
    {
        GameManager.Instance.DotWeenShakeCamera(0.1f, 0.6f, 30);
        GetComponent<SoundDesign>().PhaseOfSound = 4;
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
    }

    public void DestroyTheCog()
    {
        GameManager.Instance.cog3.SetActive(true);
        Destroy(GameManager.Instance.cog1);
    }

    public void NowYouCanEnterInTheDoor()
    {
        canEnterInTheDoor = true;
    }
    public void YouEnterInTheDoor()
    {
        foreach (GameObject Credits in GameManager.Instance.Credit)
        {
            Credits.SetActive(true);
        }
        GameManager.Instance.ChangeDialogueMoment();
        GameManager.Instance.Door.SetActive(false);
        GameManager.Instance.cleanScene();
    }

}
