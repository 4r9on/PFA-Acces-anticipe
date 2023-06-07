using System.Collections;
using System.Collections.Generic;
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
    
    public Physics2DRaycaster Raycaster2D;
    string tableau;
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
        //faire l'anim où le narrateur va appuyer sur le bouton pause
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

    public void TouchCD(int numberOfTouch)
    {
        switch (numberOfTouch)
        {
            case 0:
                Debug.Log("touche une fois");
                Narrator.GetComponent<Animator>().SetInteger();
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
}
