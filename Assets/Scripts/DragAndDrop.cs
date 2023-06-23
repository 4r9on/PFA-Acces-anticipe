using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public SpriteRenderer testBar;
    public Texture2D tex;
    public Animator animator;

    bool canThrowHandle;
    bool firstTimeUseTheLoadingBar = true;

    public float MaxScaleLoadingBar;
    public float posInit;
    public float posInitLoadingBar;
    public float posMaxInit;
    public float posMaxInitLoadingBar;
    public float posInitTop;
    public float MinScale;
    public float MaxScale;
    public float ScaleInit;

    public List<float> LightMaxValue = new List<float>();

    //public AnimatorController clip;
    public float totalSliderValue;
    public float totalSliderValueTop;
    private Rigidbody2D _Rigidbody;

    //public AnimationWindow a;
    public bool canPressTheButton;

    public GameObject Night;
    public GameObject NightMain;

    public GameObject DarkSquare;
    public Slider sliderLogo;
    public GameObject Logo;
    public int TableauActual;

    public GameObject flashinglight;
    public GameObject flashingHand;
    public GameObject digicode;
    public GameObject coliderDigiCode;
    public GameObject Woll1;
    public GameObject Woll2;

    public bool multipleTouchOnTableau2;

    public GameObject tableau5;
    public GameObject door;

    //Settings
    public GameObject buttonMusicOption;
    public GameObject buttonMusicOption2;
    public int counterMusic;
    public GameObject dMusicSettings1;
    public GameObject dMusicSettings2;
    public GameObject buttonLangue1;
    public GameObject buttonLangue2;
    public GameObject dButtonLangue1;
    public GameObject dButtonLangue2;
    public GameObject flag;
    public GameObject settingsWindow;

    public GameObject diReturnJukebox;

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // ChangeLoadingBarScale(0.33f, 1, 0);

        animator = GetComponent<Animator>();

        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "PlayButton")
            {
                oldColor = SimonUI.GetComponent<SpriteRenderer>().color;
            }
        }
        foreach (Light2D light in GameManager.Instance.lightsOnTableau1)
        {
            LightMaxValue.Add(light.intensity);
            light.intensity = 0;
        }


        //animatorBar.SetBool("Play", true);
        //animatorLogo.SetBool("Logo", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MovingBar = false;
        }
        //Barre de chargement
        if (MovingBar)
        {

            //Quand l'objet est pose on va pouvoir faire tourner l'objet dans lequel il est introduit
            if (value < 5.3f && ObjectPut != null)
            {
                GameManager.Instance.lightsOnTableau1[1].intensity = 0;
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                ObjectPut.transform.up = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);
                ObjectPut.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);

                //On va faire augmenter notre jauge ici
                if (!firstTimeUseTheLoadingBar)
                {
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

                        value += theValue;
                    }
                    ChangeLoadingBarScale(value, 5.3f, -2.65f);
                    float valuepourcent = (value - -2.65f) / (5.3f - -2.65f);

                    lastRotation = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.w;

                    for (int i = 0; i < 3; i++)
                    {
                        GameManager.Instance.lightsOnTableau1[i].intensity = LightMaxValue[i] * valuepourcent;
                    }

                    var emission = GameManager.Instance.particlesTableau1.emission;
                    emission.rateOverTime = 20 * (1 - valuepourcent);
                }
                else
                {
                    GameManager.Instance.stopTheBeginning = true;
                    firstTimeUseTheLoadingBar = false;
                }
            }
            else
            {
               
                GameManager.Instance.DotWeenShakeObject(GameManager.Instance.Gauge, 0.5f, 0.06f, 10);
                foreach (Light2D light in GameManager.Instance.lightsOnTableau1)
                {
                    if (light.gameObject.GetComponent<Animator>())
                    {
                        light.gameObject.GetComponent<Animator>().enabled = true;
                    }
                }
                foreach (Transform child in GameManager.Instance.Gauge.transform)
                {
                    if (child.name == "Loading_bar")
                    {
                        ScaleInit = child.localScale.x;
                    }
                }
                foreach (GameObject gaugeComponent in GameManager.Instance.ObjectToMakeVisibleOnBeginning)
                {
                    if (gaugeComponent.tag == "Slider")
                    {
                        gaugeComponent.GetComponent<Animator>().enabled = true;
                    }
                }
                MovingBar = false;
                canThrowHandle = true;
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().canSlide = true;
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.x, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.y, -90);
                ObjectPut.transform.eulerAngles = new Vector3(ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.x, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.y, -90);
            }


        }
        if (draggedObject != null)
        {
            if (draggedObject.tag == "Slider" && draggedObject.GetComponent<ObjectToDrag>().S2ATSlide)
            {
                float valueMaxPoint = 0;
                float pourcentageActualPoint = 0;
                float scaleValue = MaxScale - MinScale;
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < GameManager.Instance.S2ATPoints[0].y && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > GameManager.Instance.S2ATPoints[1].y)
                {
                    valueMaxPoint = GameManager.Instance.S2ATPoints[0].y - GameManager.Instance.S2ATPoints[1].y;
                    pourcentageActualPoint = (GameManager.Instance.S2ATPoints[0].y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) / valueMaxPoint;
                }
                else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < GameManager.Instance.S2ATPoints[1].y)
                {
                    pourcentageActualPoint = 1;
                }
                else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > GameManager.Instance.S2ATPoints[0].y)
                {
                    pourcentageActualPoint = 0;
                    TableauActual = 2;
                    GameManager.Instance.LoadNextLevel();
                }
                var OppacityView = DarkSquare.GetComponent<SpriteRenderer>().color;
                OppacityView.a = 1 - pourcentageActualPoint;
                DarkSquare.GetComponent<SpriteRenderer>().color = OppacityView;
                draggedObject.transform.localScale = new Vector2(draggedObject.transform.localScale.x, scaleValue * (1 - pourcentageActualPoint) + MinScale);
                draggedObject.transform.position = new Vector2(draggedObject.transform.position.x, ((posMaxInit - posInit) * (1 - pourcentageActualPoint) / 2) + posInit);
                //3.862632 min
                //17.5 max

                //   draggedObject.transform.position = new Vector2(draggedObject.transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 2);
            }

            else if (draggedObject.tag == "Slider" && draggedObject.GetComponent<ObjectToDrag>().canSlide)
            {
                if (posInit == 10000.0f)
                {
                    posInit = draggedObject.transform.position.x;
                    draggedObject.transform.parent = draggedObject.transform.parent.transform.parent;
                }
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
                //Debug.Log(posInit);
                //Debug.Log(posMaxInit);
                draggedObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 2, draggedObject.transform.position.y);

                if (draggedObject.transform.position.x > posInit)
                {
                    draggedObject.transform.position = new Vector2(posInit, draggedObject.transform.position.y);

                }
                if (draggedObject.transform.position.x < posMaxInit)
                {
                    draggedObject.transform.position = new Vector2(posMaxInit, draggedObject.transform.position.y);

                }
                float maxValue = posInit - posMaxInit;
                totalSliderValue = 1 - (posInit - draggedObject.transform.position.x) / maxValue;

                foreach (Transform child in GameManager.Instance.Gauge.transform)
                {
                    if (child.name == "Loading_bar")
                    {
                        child.localScale = new Vector3(ScaleInit * totalSliderValue, child.localScale.y, child.localScale.z);
                        child.localPosition = new Vector2(0, CalculValuePourcentOfSliderPosition(1 - totalSliderValue, posInitLoadingBar, posMaxInitLoadingBar));
                    }
                }

                CalculValuePourcentOfSliderPosition(1 - totalSliderValue, posInitLoadingBar, posMaxInitLoadingBar);

                if (totalSliderValue >= 0.25f)
                {
                    mySpriteBar.sprite = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * totalSliderValue), tex.height), new Vector2(0.5f / totalSliderValue/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f), 100.0f);


                }
                if (totalSliderValue == 1.0f)
                {
                    // sliderNight.SetActive(true);
                    //  Logo.SetActive(false);
                    //logoSlider.SetActive(true);



                    /* foreach (AnimationClip Clips in clip.animationClips)
                     {
                         Debug.Log(Clips);
                         foreach (UnityEditor.EditorCurveBinding frames in AnimationUtility.GetObjectReferenceCurveBindings(Clips))
                         {
                             ObjectReferenceKeyframe[] keyFrames = AnimationUtility.GetObjectReferenceCurve(Clips, frames);

                             Debug.Log(keyFrames);
                             foreach (var frame in keyFrames)
                             {
                                 Debug.Log(frame.time);
                                 Sprite SpriteInFrame = (Sprite)frame.value;
                                 tex = SpriteInFrame.texture;
                                 Debug.Log(tex.name);
                                 Debug.Log(testBar.sprite.rect.width);
                                 SpriteInFrame = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * totalSliderValue), tex.height), new Vector2(0.5f / totalSliderValue/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3, 0.5f), 100.0f);
                                 testBar.sprite = SpriteInFrame;
                                 ValueSlider();
                                 Debug.Log(testBar.sprite.rect.width);

                                 AnimationUtility.SetObjectReferenceCurve(Clips, frames, keyFrames);
                             }
                              StartCoroutine(testWaiting(keyFrames));

                         }


                         List<Sprite> allSprite = new List<Sprite>();
                         allSprite.AddRange(GetSpritesFromClip(Clips));
                         foreach (Sprite sprite in GetSpritesFromClip(Clips))
                         {

                         }

                     }*/

                }


                //Aide de AgeTDev sur https://answers.unity.com/questions/1245599/how-to-get-all-sprites-used-in-a-2d-animator.html    


            }
            else if (draggedObject.tag != "Slider")
            {
                draggedObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            }
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
                    if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity != 0)
                    {
                        GameManager.Instance.DotWeenShakeObject(draggedObject, 0.2f, 0.06f, 20);
                    }
                    if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity == 0)
                    {
                        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                        foreach (Transform children in draggedObject.transform)
                        {
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
                    else if (ObjectPut != null)
                    {
                         if (ObjectPut.tag == "Simon" && draggedObject == ObjectPut)
                        {

                        }
                    }
                    else
                    {
                        if(draggedObject.name != "Stock CD")
                        {
                            draggedObject.GetComponent<ObjectToDrag>().destroyOnGravity = false;
                        }
                        
                    }
                }



            }

        }

        //Nous fera bouger la barre de chargement
        else if (GameManager.Instance.ObjectHover.tag == "Bar" && ObjectPut != null)
        {
            MovingBar = true;
        }

        //Donne le nom de l'UI que l'on a clique dessus
        else if (GameManager.Instance.ObjectHover.tag == "Simon")
        {
            GameManager.Instance.ObjectHover.GetComponent<Animator>().SetBool("IsClicked", true);

            if (GameManager.Instance.ObjectHover.name == "Button_Pause" && !multipleTouchOnTableau2)
            {
                GetComponent<Simon>().BeginTheSimon();
            }
            else if((GameManager.Instance.ObjectHover.name == "Button_Pause" || GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().CD) && GameManager.Instance.canTouchCd)
            {
                nbrOfTimeWeTouch++;
            }
            if (ObjectPut != null)
            {
                foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                {

                    if (SimonUI.GetComponent<ObjectToDrag>().CD)
                    {
                        if (SimonUI == GameManager.Instance.ObjectHover)
                        {
                            nbrOfTimeWeTouch++;
                        }
                    }
                }
            }

            else
            {
                if (GameManager.Instance.ObjectHover.name != "Button_Pause")
                {
                    GetComponent<Simon>().AddToComparative(GameManager.Instance.ObjectHover.name);
                }

            }

        }

        else if (GameManager.Instance.ObjectHover.tag == "ButtonON")
        {
            if (GameManager.Instance.ON[0].GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<SpriteRenderer>().sprite == GameManager.Instance.ON[0].GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<SpriteRenderer>().sprite)
            {

                foreach (GameObject ObjectON in GameManager.Instance.ON)
                {

                    Destroy(ObjectON.GetComponent<Rigidbody2D>());
                }
                GameManager.Instance.AllText.GetComponent<ElevateText>().RotateIt();
            }
        }

        else if (GameManager.Instance.ObjectHover.tag == "Light")
        {
            flashinglight.SetActive(false);
            flashingHand.SetActive(true);
            GameManager.Instance.ObjectHover = null;
            GameManager.Instance.Raycaster2D.eventMask = 503;
        }

        else if (GameManager.Instance.ObjectHover.tag == "Digi")
        {
            digicode.SetActive(true);
            coliderDigiCode.SetActive(true);

            if (GameManager.Instance.ObjectHover.tag == "Digi2")
            {
                digicode.SetActive(false);
                coliderDigiCode.SetActive(false);
            }
        }

        else if (GameManager.Instance.ObjectHover.tag == "Button")
        {
            GameManager.Instance.breakingTheWall();
        }

        else if (GameManager.Instance.ObjectHover.tag == "UV")
        {
            GameManager.Instance.FallTheHole(GameManager.Instance.ObjectHover);
        }

        else if (GameManager.Instance.ObjectHover.tag == "Door")
        {
            tableau5.SetActive(true);
            door.SetActive(false);
        }

        else if (GameManager.Instance.ObjectHover.tag == "Vis")
        {
            animator.SetBool("Visser", true);
        }

        else if (GameManager.Instance.ObjectHover.tag == "ButtonLangue")
        {
            StartCoroutine(SettingsLangue1());
        }

        else if (GameManager.Instance.ObjectHover.tag == "ButtonLangue2")
        {
            StartCoroutine(SettingsLangue2());
        }
        else if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Background)
        {
            GameManager.Instance.ObjectHover.SetActive(false);
            GameManager.Instance.leftWall.SetActive(false);
            GameManager.Instance.tableau3.GetComponent<Animator>().SetBool("PassedTo4", true);

            GameManager.Instance.tableau4.SetActive(true);
            diReturnJukebox.SetActive(true);
            Debug.Log("aaaaaaahhh");

        }

        else if (GameManager.Instance.ObjectHover.tag == "ButtonMusic")
        {
            StartCoroutine(SettingsMusic());
        }

        else if (GameManager.Instance.ObjectHover.tag == "SettingsOpen")
        {
            settingsWindow.SetActive(true);
        }

        else if (GameManager.Instance.ObjectHover.tag == "SettingsClose")
        {
            settingsWindow.SetActive(false);
        }

        else if (GameManager.Instance.ObjectHover.tag == "SettingsOpen")
        {
            settingsWindow.SetActive(true);
        }

        else if (GameManager.Instance.ObjectHover.tag == "Quit")
        {
            //Application.Quit();
        }



    }
    public void StopClick()
    {
        if (draggedObject != null)
        {
            if (draggedObject.tag == "Slider" && canThrowHandle)
            {
                float maxValue = posInit - posMaxInit;
                totalSliderValue = (posInit - draggedObject.transform.position.x) / maxValue;

                if (totalSliderValue > 0.75)
                {
                    draggedObject.transform.position = new Vector2(posInit, draggedObject.transform.position.y);
                    GameManager.Instance.Gauge.GetComponent<Animator>().enabled = true;
                    ObjectPut.GetComponent<Animator>().enabled = true;
                    ObjectPut.transform.parent = ObjectPut.transform.parent.parent.parent;
                    ObjectPut = null;
                    GameObject[] gameObjectsToRemove = new GameObject[] { draggedObject };
                    StartCoroutine(GameManager.Instance.TakeAwayTheGauge(gameObjectsToRemove));
                    GameManager.Instance.cleanScene();
                }
                foreach (Transform child in GameManager.Instance.Gauge.transform)
                {
                    if (child.name == "Loading_bar")
                    {
                        child.localScale = new Vector3(ScaleInit, child.localScale.y, child.localScale.z);
                        child.localPosition = new Vector2(0, CalculValuePourcentOfSliderPosition(0, posInitLoadingBar, posMaxInitLoadingBar));
                    }
                }
            }
        }
        if (GameManager.Instance.ObjectHover != null)
        {
            
            if (GameManager.Instance.ObjectHover.tag == "Simon" && nbrOfTimeWeTouch > 0)
            {
                
                foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                {
                    if (SimonUI.name == "Button_Pause" && SimonUI == GameManager.Instance.ObjectHover && multipleTouchOnTableau2)
                    {
                        GameManager.Instance.TouchCD(nbrOfTimeWeTouch);
                    }

                    if (SimonUI.GetComponent<ObjectToDrag>().CD && SimonUI == GameManager.Instance.ObjectHover)
                    {
                        GameManager.Instance.TouchCD(nbrOfTimeWeTouch);
                    }
                }
            }
        }

        if (draggedObject != null)
        {
            if (draggedObject.GetComponent<ObjectToDrag>() != null)
            {



                if (draggedObject.GetComponent<ObjectToDrag>().canPutObject && GameManager.Instance.ObjectHover == draggedObject.GetComponent<ObjectToDrag>().objectToPutOn)
                {
                    if (draggedObject.GetComponent<ObjectToDrag>().objectToPutOn == GameManager.Instance.Gauge)
                    {
                        GameManager.Instance.DotWeenShakeObject(GameManager.Instance.Gauge, 0.1f, 0.01f, 10);
                    }
                    if (draggedObject.GetComponent<ObjectToDrag>().Moon)
                    {

                        GameManager.Instance.NightFall();
                        if (draggedObject.transform.parent.GetComponent<ObjectToDrag>() != null)
                        {
                            if (draggedObject.transform.parent.GetComponent<ObjectToDrag>().painting == true)
                            {
                                draggedObject.transform.parent = draggedObject.transform.parent.parent;
                            }
                        }

                    }
                    draggedObject.transform.position = new Vector3(GameManager.Instance.ObjectHover.transform.position.x, GameManager.Instance.ObjectHover.transform.position.y, GameManager.Instance.ObjectHover.transform.position.z);
                    draggedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    draggedObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    draggedObject.GetComponent<ObjectToDrag>().isPut = true;
                    if (draggedObject.GetComponent<ObjectToDrag>().CD)
                    {
                        draggedObject.tag = "Simon";
                        draggedObject.layer = 6;
                        GameManager.Instance.SimonUI.Add(draggedObject);
                        draggedObject.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().enabled = false;
                    }
                    if (draggedObject.tag != "Simon")
                    {
                        draggedObject.tag = "Untagged";
                    }



                    if (draggedObject.GetComponent<ObjectToDrag>().objectToPutOn.tag == "ButtonON")
                    {
                        GameManager.Instance.DotWeenShakeCamera(0.2f, 0.5f, 20);
                        foreach (GameObject ObjectON in GameManager.Instance.ON)
                        {
                            if (ObjectON == draggedObject)
                            {
                                draggedObject.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<SpriteRenderer>().sprite = draggedObject.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<SpriteRenderer>().sprite;
                                GameManager.Instance.ON.Remove(ObjectON);
                                Destroy(ObjectON);
                            }
                        }
                        draggedObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    }
                    ObjectPut = draggedObject;
                    StartCoroutine(draggedObject.GetComponent<ObjectToDrag>().BecomeDestroyable());
                }
            }
            dragged = false;
            draggedObject = null;


        }
    }

    IEnumerator TouchUI(GameObject SimonUI, float timing)
    {
        yield return new WaitForSeconds(timing);
    }

    public void ValueSlider()
    {
        if (totalSliderValue <= 0.40f)
        {
            totalSliderValue = 1;


            //_Rigidbody.AddForce 


        }
        else
        {
            // totalSliderValue = ;
        }
    }

    float CalculValuePourcentOfSliderScale(float scaleValue, float pourcentageActualPoint)
    {
        return (scaleValue * (1 - pourcentageActualPoint) + MinScale);
    }
    float CalculValuePourcentOfSliderPosition(float pourcentageActualPoint, float Min, float Max)
    {
        return (((Max - Min) * (1 - pourcentageActualPoint)) + Min);
    }

    public void ChangeLoadingBarScale(float valueGive, float maxValue, float minValue)
    {
        foreach (Transform child in GameManager.Instance.Gauge.transform)
        {
            if (child.name == "Loading_bar")
            {
                float pourcentageOfTheValue = ((maxValue - minValue) - (valueGive - minValue)) / (maxValue - minValue);
                //MaxScaleLoadingBar = child.localScale.x;
                float scaleValue = MaxScale - MinScale;
                child.localScale = new Vector3(CalculValuePourcentOfSliderScale(scaleValue, pourcentageOfTheValue), child.localScale.y, child.localScale.z);
                /*if (posInitLoadingBar < 0)
                {
                    posInitLoadingBar *= -1;
                }
                if (posMaxInitLoadingBar < 0)
                {
                    posMaxInitLoadingBar *= -1;
                }
                posMaxInitLoadingBar = posMaxInitLoadingBar + posInitLoadingBar;
                posInitLoadingBar = 0;
                */
                child.localPosition = new Vector2(0, CalculValuePourcentOfSliderPosition(pourcentageOfTheValue, posInitLoadingBar, posMaxInitLoadingBar));
                // ((posInitLoadingBar - posMaxInitLoadingBar) * (1 - 0.33f) / 2) + posInitLoadingBar
            }
        }
    }

    /* IEnumerator testWaiting(ObjectReferenceKeyframe[] keyFrames)
     {
         foreach (var frame in keyFrames)
         {

             Sprite SpriteInFrame = (Sprite)frame.value;
             tex = SpriteInFrame.texture;
             Debug.Log(tex.name);
             Debug.Log(testBar.sprite.rect.width);
             SpriteInFrame = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * totalSliderValue), tex.height), new Vector2(0.5f / totalSliderValue/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3, 0.5f), 100.0f);
             testBar.sprite = SpriteInFrame;
             ValueSlider();
             Debug.Log(testBar.sprite.rect.width);

             yield return new WaitForSeconds(0.1f);
         }
     }*/

    IEnumerator SettingsMusic()
    {
        buttonMusicOption.SetActive(false);
        dMusicSettings1.SetActive(true);
        yield return new WaitForSeconds(3);

        dMusicSettings1.SetActive(false);
        dMusicSettings2.SetActive(true);
        Debug.Log("tyu");

        yield return new WaitForSeconds(3);
        dMusicSettings2.SetActive(false);
        Debug.Log("wxc");

        buttonMusicOption.SetActive(true);
    }

    IEnumerator SettingsLangue1()
    {
        buttonLangue1.SetActive(false);
        flag.SetActive(true);
        dButtonLangue1.SetActive(true);
        yield return new WaitForSeconds(3);
        dButtonLangue1.SetActive(false);
        dButtonLangue2.SetActive(true);
        flag.SetActive(false);
        buttonLangue1.SetActive(true);
    }

    IEnumerator SettingsLangue2()
    {
        buttonLangue2.SetActive(false);
        dButtonLangue1.SetActive(true);
        flag.SetActive(true);
        yield return new WaitForSeconds(3);
        dButtonLangue1.SetActive(false);
        dButtonLangue2.SetActive(true);
        flag.SetActive(false);
        buttonLangue2.SetActive(true);
    }
}
