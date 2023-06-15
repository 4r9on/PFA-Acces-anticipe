using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

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

    //Tableau 1
    public GameObject Gauge;
    public GameObject S2AT;
    public GameObject S2ATWithWriting;
    public List<Light2D> lightsOnTableau1 = new List<Light2D>();
    public ParticleSystem particlesTableau1;
    public List<Vector2> S2ATPoints;
    List<GameObject> ObjectToRemoveAfterGauge = new List<GameObject>();

    //Tableau 3
    public List<GameObject> Day = new List<GameObject>();
    public List<GameObject> Night = new List<GameObject>();
    public SpriteMask LampMask;
    public GameObject LeftWallAnimation;
    public GameObject ButtonInWall;

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
        
        if (PlayerPrefs.GetInt("GetCrashed") == 1)
            {
            PlayerPrefs.SetInt("GetCrashed", 0);
            dAD.TableauActual = 5;
                LoadNextLevel();
            }
        
       
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
        tableau1.SetActive(false);
        tableau2.SetActive(false);
        tableau3.SetActive(false);
        tableau4.SetActive(false);
        tableau5.SetActive(false);
        if (dAD.TableauActual == 2)
        {
            tableau2.SetActive(true);
        }
        else if (dAD.TableauActual == 3)
        {
            tableau3.SetActive(true);
        }
        else if (dAD.TableauActual == 4)
        {
            tableau4.SetActive(true);
        }
        else if (dAD.TableauActual == 5)
        {
            tableau5.SetActive(true);
        }
        dAD.ObjectPut = null;
        dAD.draggedObject = null;
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
        S2ATWithWriting.GetComponent<Animator>().enabled = true;
        S2ATWithWriting.transform.localScale = Vector3.one;
        S2ATWithWriting.transform.position = new Vector3(0.013f, 1.752f, S2ATWithWriting.transform.position.z);
        GetComponent<DragAndDrop>().MinScale = S2AT.transform.localScale.y;
        foreach (GameObject go in GameObjectToRemove)
        {
            ObjectToRemoveAfterGauge.Add(go);
           /* go.SetActive(false);
            go.transform.parent = Gauge.transform;*/
        }
    }

    public void RemoveGauge()
    {
        Gauge.SetActive(false);
        foreach(GameObject Objects in ObjectToRemoveAfterGauge)
        {
            Objects.SetActive(false);
        }
    }

    public void S2ATQuit()
    {
        S2AT.SetActive(true);
        S2ATWithWriting.SetActive(false);
        foreach (Transform child in S2AT.transform.GetChild(0))
        {
            if (child.gameObject.name == "MaxPos")
            {
                GetComponent<DragAndDrop>().posMaxInit = child.position.y;
            }
            else
            {
                GetComponent<DragAndDrop>().posInit = S2AT.transform.GetChild(0).position.y;
            }
        }
    }

    public void NightFall()
    {
        foreach(GameObject ObjetcsDay in Day)
        {
            ObjetcsDay.SetActive(false);
        }
        foreach (GameObject ObjetcsNight in Night)
        {
            ObjetcsNight.SetActive(true);
        }
        LampMask.enabled = true;
    }

    public void FallTheHole()
    {
        LeftWallAnimation.GetComponent<Animator>().enabled = true;
        LeftWallAnimation.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void breakingTheWall()
    {
        foreach(Transform child in LeftWallAnimation.transform.parent)
        {
            if(child.gameObject != LeftWallAnimation)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Animator>().enabled = true;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        ButtonInWall.SetActive(false);
    }
}
