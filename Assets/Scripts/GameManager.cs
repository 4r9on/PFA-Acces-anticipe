using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    List<GameObject> ObjectPut = new List<GameObject>();
    public GameObject ObjectHover;
    public GameObject AllText;
    public List<GameObject> SimonUI = new List<GameObject>();
    public List<GameObject> breakableUI = new List<GameObject>();
    public GameObject StockCD;
    public GameObject Narrator;

    public GameObject Gauge;
    public GameObject S2AT;
    public GameObject S2ATWithWriting;

    public List<Vector2> S2ATPoints;

    public List<GameObject> ON = new List<GameObject>();
    
    public Physics2DRaycaster Raycaster2D;
    string tableau;

    public GameObject tableau1;
    public GameObject tableau2;  
    public GameObject tableau3;
    public GameObject tableau4;
    public GameObject tableau5;

    public DragAndDrop dAD;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfterGainSimon()
    {
        //faire l'anim ou le narrateur va appuyer sur le bouton pause
        //faire tomber le disque
        foreach(GameObject obj in SimonUI)
        {
            if (obj.name == "Button_Pause")
            {
                obj.SetActive(false);
            }
        }
        StockCD.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        StockCD.tag = "Object";
    }

    public void LoadNextLevel()
    {
        if (dAD.sliderLogo.value == 1.0f)
        {
            tableau1.SetActive(false);
            tableau2.SetActive(true);
        }
        /*else if ()
        {
            tableau2.SetActive(false); 
            tableau3.SetActive(true);
        }
        else if ()
        {
            tableau3.SetActive(false);
            tableau4.SetActive(true);
        }
        else if ()
        {
            tableau4.SetActive(false);
            tableau5.SetActive(true);
        }*/
    }
    public void TouchCD(int numberOfTouch)
    {
      //  Narrator.GetComponent<Animator>().SetInteger("nrbOfTouch", numberOfTouch);
        switch (numberOfTouch)
        {
            case 0:
                Debug.Log("touche une fois");
                
               // timing = 0.5f;
                break;
            case 1:
                Debug.Log("touche une seconde fois");
               // timing = 0.4f;
                break;
            case 2:
                Debug.Log("Detruit l'UI");
               // timing = 0.3f;
                //nous permet de rendre la souris invisible et non utilisable
                Cursor.lockState = CursorLockMode.Locked;

                //On va utiliser un faux curseur pour empecher le joueur de l'utiliser
              /*  cursor.SetActive(true);
                cursor.transform.position = new Vector3(GetComponent<Raycast>().HitToStopMouse.point.x, GetComponent<Raycast>().HitToStopMouse.point.y, 0f);*/
                break;
        }
    }

    public IEnumerator TakeAwayTheGauge(GameObject[] GameObjectToRemove)
    {
        yield return new WaitForSeconds(1);
        Gauge.SetActive(false);
        S2AT.SetActive(true);
        S2ATWithWriting.SetActive(false);
        GetComponent<DragAndDrop>().MinScale = S2AT.transform.localScale.y;
        foreach (GameObject go in GameObjectToRemove)
        {
            go.SetActive(false);
        }
        foreach (Transform child in S2AT.transform.GetChild(0))
        {
            if(child.gameObject.name == "MaxPos")
            {
                GetComponent<DragAndDrop>().posMaxInit = child.position.y;
            }
            else
            {
                GetComponent<DragAndDrop>().posInit = S2AT.transform.GetChild(0).position.y;
            }
        }
    }
}
