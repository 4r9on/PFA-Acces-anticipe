using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    public bool dragged = false;
    public GameObject draggedObject;
    public GameObject ObjectPut;
    public Camera cam;
    bool MovingBar;
    public float lastRotation = 0;
    public float value;
    public GameObject cursor;
    public bool CanMoveCursor = true;
    public RaycastHit lastHit;
    public int nbrOfTimeWeTouch;
    Color oldColor;
    public GameObject slider;
    private SpriteRenderer sr;
    public SpriteRenderer mySpriteBar;
    public SpriteRenderer testBar;
    public Texture2D tex;
    public Animator animator;

    public bool canThrowHandle;
    public bool goToTheRightBar;
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
    public Rigidbody2D vis1;

    public GameObject diReturnJukebox;

    public List<GameObject> AnimPaused = new List<GameObject>();

    Scene sceneBeforeSettings;
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
            if (MovingBar)
            {
                GameManager.Instance.CancelLoopingObjects(GameManager.Instance.nameOfLoopingObject);
            }
            MovingBar = false;
        }
        //Barre de chargement
        if (MovingBar)
        {

            //Quand l'objet est pose on va pouvoir faire tourner l'objet dans lequel il est introduit
            if (value < 1000f && ObjectPut != null)
            {
                GameManager.Instance.lightsOnTableau1[1].intensity = 0;
                if (goToTheRightBar)
                {
                    ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized);
                    ObjectPut.transform.up = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position).normalized;
                }
                else
                {
                    ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.up = (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
                    ObjectPut.transform.up = (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
                }

                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);
                ObjectPut.transform.eulerAngles = new Vector3(0, 0, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.z);

                //On va faire augmenter notre jauge ici
                if (!firstTimeUseTheLoadingBar)
                {
                    if ((ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z < lastRotation && lastRotation < 350) || (lastRotation < 30 && ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z > 300))
                    {
                        if (GameManager.Instance.nameOfLoopingObject != GameManager.Instance.Gauge.name)
                        {
                            GameManager.Instance.Gauge.GetComponent<SoundDesign>().PhaseOfSound = 1;
                            GameManager.Instance.SoundLoop(GameManager.Instance.Gauge, 0.5f);
                        }
                        float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z;

                        if (lastRotation < 30 && ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z > 300)
                        {
                            theValue = 360 - theValue + lastRotation;
                        }
                        else
                        {

                            theValue = lastRotation - theValue;
                        }
                        value -= theValue;
                        if (value < -600)
                        {
                            value = -600;
                        }
                    }



                    //On va faire decrementer notre jauge ici
                    else if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z != lastRotation || (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z < 30 && lastRotation > 300))
                    {
                        if (GameManager.Instance.nameOfLoopingObject != GameManager.Instance.Gauge.name)
                        {
                            GameManager.Instance.Gauge.GetComponent<SoundDesign>().PhaseOfSound = 1;
                            GameManager.Instance.SoundLoop(GameManager.Instance.Gauge, 0.5f);
                        }

                        float theValue = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z;

                        if (ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z < 30 && lastRotation > 300)
                        {
                            Debug.Log(theValue);
                            Debug.Log(lastRotation);
                            theValue = 360 - lastRotation + theValue;
                        }
                        else
                        {
                            theValue = theValue - lastRotation;
                        }
                        value += theValue;
                        if (value < -600)
                        {
                            value = -600;
                        }
                    }
                    else
                    {
                        GameManager.Instance.CancelLoopingObjects(GameManager.Instance.nameOfLoopingObject);
                    }
                    ChangeLoadingBarScale(value, 1000f, -500f);
                    float valuepourcent = (value - -500f) / (1000f - -500f);

                    lastRotation = ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.rotation.eulerAngles.z;

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
            else if (!canThrowHandle)
            {

                GameManager.Instance.CancelLoopingObjects(GameManager.Instance.nameOfLoopingObject);
                ChangeLoadingBarScale(5.3f, 5.3f, -2.65f);
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

                slider.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                GameManager.Instance.NewSound(slider, slider.GetComponent<SoundDesign>().TheVolume);


                MovingBar = false;
                canThrowHandle = true;
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().objectToPutOn.GetComponent<ObjectToDrag>().canSlide = true;
                ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles = new Vector3(ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.x, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.y, -90);
                ObjectPut.transform.eulerAngles = new Vector3(ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.x, ObjectPut.GetComponent<ObjectToDrag>().objectToPutOn.transform.eulerAngles.y, -90);
                slider.transform.parent = slider.transform.parent.parent;
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
                    foreach (GameObject child in GameManager.Instance.ObjectToMakeVisibleOnBeginning)
                    {
                        if (child.tag == "SettingsOpen")
                        {
                            child.transform.parent = GameManager.Instance.tableau1.transform.parent;
                        }
                    }
                    TableauActual = 2;
                    GameManager.Instance.LoadNextLevel();
                }
                var OppacityView = DarkSquare.GetComponent<SpriteRenderer>().color;
                OppacityView.a = 1 - pourcentageActualPoint;
                DarkSquare.GetComponent<SpriteRenderer>().color = OppacityView;
                if (draggedObject != null)
                {
                    draggedObject.transform.localScale = new Vector2(draggedObject.transform.localScale.x, scaleValue * (1 - pourcentageActualPoint) + MinScale);
                    draggedObject.transform.position = new Vector2(draggedObject.transform.position.x, ((posMaxInit - posInit) * (1 - pourcentageActualPoint) / 2) + posInit);
                }
                //3.862632 min
                //17.5 max

                //   draggedObject.transform.position = new Vector2(draggedObject.transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 2);
            }

            else if (draggedObject.tag == "Slider" && draggedObject.GetComponent<ObjectToDrag>().canSlide)
            {
                if (posInit == 10000.0f)
                {
                    posInit = draggedObject.transform.position.x;

                }
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
                //Debug.Log(posInit);
                //Debug.Log(posMaxInit);
                draggedObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 2, draggedObject.transform.position.y);
                draggedObject.GetComponent<SoundDesign>().PhaseOfSound = 2;

                if (GameManager.Instance.nameOfLoopingObject != draggedObject.name)
                {
                    GameManager.Instance.SoundLoop(draggedObject, 0.5f);
                }
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
        if (GameManager.Instance.ObjectHover != null)
        {
            Debug.Log(GameManager.Instance.ObjectHover.tag);
            if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().chest)
            {
                if (GameManager.Instance.Digicode.activeInHierarchy == false)
                {
                    GameManager.Instance.Digicode.SetActive(true);
                    GameObject bookToChange = GameManager.Instance.AllBooks[Random.Range(0, GameManager.Instance.AllBooks.Count)];
                    List<GameObject> ChooseAnotherBook = GameManager.Instance.AllBooks;
                    ChooseAnotherBook.Remove(bookToChange);
                    GameManager.Instance.ChangeSpriteBook(bookToChange);
                    GameManager.Instance.ChangeSpriteBook(ChooseAnotherBook[Random.Range(0, ChooseAnotherBook.Count)]);
                }
                else
                {
                    GameManager.Instance.Digicode.SetActive(false);
                }

            }
            else if (GameManager.Instance.ObjectHover.tag == "Object" || GameManager.Instance.ObjectHover.tag == "Hammer" || GameManager.Instance.ObjectHover.tag == "Slider")
            {

                if (GameManager.Instance.ObjectHover.tag == "Slider" && !canThrowHandle)
                {
                    if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().sliderRight)
                    {
                        goToTheRightBar = true;
                    }
                    else
                    {
                        goToTheRightBar = false;
                    }
                    if(ObjectPut != null)
                    {
                        MovingBar = true;
                    }
                }
                else
                {
                    draggedObject = GameManager.Instance.ObjectHover;
                }
                if(draggedObject != null)
                {
                    if (draggedObject.GetComponent<Rigidbody2D>() != null)
                    {
                        draggedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    }
                }
                if (draggedObject.GetComponent<ObjectToDrag>() != null && GameManager.Instance.ObjectHover.tag != "Slider")
                {
                    if (draggedObject.GetComponent<ObjectToDrag>().painting)
                    {
                        GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
                    }
                    //Permet de faire tomber l'UI lorsqu'on aura clicker suffisamment dessus
                    if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity > 0)
                    {
                        if (draggedObject.GetComponent<ObjectToDrag>().painting)
                        {
                            flashingHand.transform.parent.parent.gameObject.SetActive(true);
                        }
                        draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity--;
                        if (draggedObject == GameManager.Instance.cog1)
                        {
                            GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
                        }
                        if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity != 0)
                        {
                            GameManager.Instance.DotWeenShakeObject(draggedObject, 0.2f, 0.06f, 20);
                        }
                        if (draggedObject.GetComponent<ObjectToDrag>().BornWithoutGravity == 0)
                        {
                            if (draggedObject.GetComponent<ObjectToDrag>().objectToPutOn == GameManager.Instance.Gauge)
                            {
                                draggedObject.transform.parent = draggedObject.transform.parent.parent;
                            }
                            if (draggedObject.GetComponent<ObjectToDrag>().sign)
                            {
                                draggedObject.GetComponent<Animator>().enabled = true;
                            }
                            else
                            {
                                draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                            }
                            foreach (Transform children in draggedObject.transform)
                            {
                                children.GetComponent<Rigidbody2D>().gravityScale = 1;
                            }
                        }
                        draggedObject = null;
                    }
                    else
                    {
                        if (draggedObject == GameManager.Instance.cog3)
                        {
                            draggedObject.GetComponent<SoundDesign>().PhaseOfSound = 1;
                            GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
                            StartCoroutine(draggedObject.GetComponent<ObjectToDrag>().MakeItRoll());
                        }
                        if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Moon)
                        {
                            GameManager.Instance.NewSound(GameManager.Instance.ObjectHover, GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().TheVolume);
                        }
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
                            if (draggedObject.name != "Stock CD")
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
                if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().isButtonPause)
                {
                    if(GameManager.Instance.ObjectHover.GetComponent<Animator>().GetBool("IsChanged") == true)
                    {
                        GameManager.Instance.ObjectHover.GetComponent<Animator>().SetBool("IsChanged", false);
                    }
                    else
                    {
                        Debug.Log("oui");
                        GameManager.Instance.ObjectHover.GetComponent<Animator>().SetBool("IsChanged", true);
                    }
                }
                
                GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().PhaseOfSound = 1;
                GameManager.Instance.NewSound(GameManager.Instance.ObjectHover, GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().TheVolume);

                if (GameManager.Instance.ObjectHover.name == "Button_Pause" && !multipleTouchOnTableau2)
                {
                    GetComponent<Simon>().BeginTheSimon();
                }
                else if ((GameManager.Instance.ObjectHover.name == "Button_Pause" || GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().CD) && GameManager.Instance.canTouchCd)
                {
                    nbrOfTimeWeTouch++;
                    GameManager.Instance.GetComponent<AudioSource>().Play();
                }
                if (ObjectPut != null)
                {
                    foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                    {

                        if (SimonUI.GetComponent<ObjectToDrag>().CD)
                        {
                            if (SimonUI == GameManager.Instance.ObjectHover)
                            {
                                //nbrOfTimeWeTouch++;
                            }
                        }
                    }
                }

                else
                {
                    if (GameManager.Instance.ObjectHover.name != "Button_Pause")
                    {
                        if (GetComponent<Simon>().infiniteGame.Count > 0)
                        {
                            GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().PhaseOfSound = 2;
                            GameManager.Instance.NewSound(GameManager.Instance.ObjectHover, GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().TheVolume);
                        }
                        GetComponent<Simon>().AddToComparative(GameManager.Instance.ObjectHover.name);

                    }

                }

            }
            else if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Digicode > 0)
            {
                bool stopSearch = false;
                if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Digicode > 10)
                {
                    if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Digicode == 11)
                    {
                        for (int i = GameManager.Instance.DigicodeView.Count - 1; i > -1; i--)
                        {
                            if (GameManager.Instance.DigicodeView[i].GetComponent<SpriteRenderer>().sprite != null && !stopSearch)
                            {
                                GameManager.Instance.DigicodeView[i].GetComponent<SpriteRenderer>().sprite = null;
                                stopSearch = true;
                            }
                        }
                    }
                    else
                    {
                        GameManager.Instance.SearchSpriteInDigicode(stopSearch);
                    }
                }
                else
                {

                    GameManager.Instance.SearchSpriteInDigicode(stopSearch);

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
                    GameManager.Instance.ChangeDialogueMoment();
                }
            }


            else if (GameManager.Instance.ObjectHover.tag == "Light")
            {
                GameManager.Instance.NewSound(flashinglight, flashinglight.GetComponent<SoundDesign>().TheVolume);
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
                GameManager.Instance.NewSound(GameManager.Instance.ObjectHover, GameManager.Instance.ObjectHover.GetComponent<SoundDesign>().TheVolume);
            }

            else if (GameManager.Instance.ObjectHover.tag == "UV")
            {
                GameManager.Instance.FallTheHole(GameManager.Instance.ObjectHover);
            }

            else if (GameManager.Instance.ObjectHover.tag == "Vis")
            {
                int a = -10;
                //animator.SetBool("Visser", true);
                Debug.Log("fgh");

                vis1.AddForce(transform.up * a);
                Debug.Log("f");


            }

            else if (GameManager.Instance.ObjectHover.tag == "Door")
            {
                if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().canEnterInTheDoor)
                {
                    GameManager.Instance.ObjectHover.GetComponent<Animator>().SetBool("Tourne", true);
                    GameManager.Instance.ObjectHover.GetComponent<SpriteRenderer>().sortingOrder = 33;
                }
            }

            /*  else if (GameManager.Instance.ObjectHover.tag == "ButtonLangue")
              {
                  StartCoroutine(SettingsLangue1());
              }

              else if (GameManager.Instance.ObjectHover.tag == "ButtonLangue2")
              {
                  StartCoroutine(SettingsLangue2());
              }*/
            else if (GameManager.Instance.ObjectHover.GetComponent<ObjectToDrag>().Background)
            {
                flashingHand.SetActive(false);
                GameManager.Instance.ObjectHover.SetActive(false);
                GameManager.Instance.leftWall.SetActive(false);
                GameManager.Instance.tableau3.GetComponent<Animator>().SetBool("PassedTo4", true);
                GameManager.Instance.ChangeDialogueMoment();
                GameManager.Instance.tableau4.SetActive(true);
                //diReturnJukebox.SetActive(true);

            }

            else if (GameManager.Instance.ObjectHover.tag == "ButtonMusic")
            {
                GameManager.Instance.SettingsNarrations(GameManager.Instance.ObjectHover.tag);
            }

            else if (GameManager.Instance.ObjectHover.tag == "SettingsOpen")
            {
                if (settingsWindow.activeInHierarchy)
                {
                    settingsWindow.SetActive(false);
                    if (GameManager.Instance.animWasStop)
                    {
                        foreach (GameObject child in AnimPaused.ToArray())
                        {
                            child.GetComponent<Animator>().SetFloat("Speed", 1f);
                            AnimPaused.Remove(child.gameObject);
                        }
                        GameManager.Instance.IdDialogueMoment = GameManager.Instance.LastIdDialogueMoment;
                        GameManager.Instance.IdDialogue = GameManager.Instance.LastIdDialogue;
                        GameManager.Instance.bocksMomentSpeak = GameManager.Instance.dialogueMomentList[GameManager.Instance.IdDialogueMoment];
                        GameManager.Instance.dialogueList.Clear();
                        foreach (Transform child in GameManager.Instance.bocksMomentSpeak.transform)
                        {
                            GameManager.Instance.dialogueList.Add(child.gameObject);
                        }
                        GameManager.Instance.IdDialogueMoment++;
                    }
                }
                else
                {
                    settingsWindow.SetActive(true);
                    if (GameManager.Instance.dialogueList[GameManager.Instance.IdDialogue - 1] != null)
                    {
                        foreach (Transform child in GameManager.Instance.dialogueList[GameManager.Instance.IdDialogue - 1].transform)
                        {
                            // Debug.Log(child.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString());
                            child.GetComponent<Animator>().SetFloat("Speed", 0f);
                            AnimPaused.Add(child.gameObject);
                        }
                        GameManager.Instance.LastIdDialogueMoment = GameManager.Instance.IdDialogueMoment - 1;
                        GameManager.Instance.LastIdDialogue = GameManager.Instance.IdDialogue;

                        GameManager.Instance.IdDialogueMoment = 7;
                        GameManager.Instance.bocksMomentSpeak = GameManager.Instance.dialogueMomentList[GameManager.Instance.IdDialogueMoment];
                        GameManager.Instance.dialogueList.Clear();
                        foreach (Transform child in GameManager.Instance.bocksMomentSpeak.transform)
                        {
                            GameManager.Instance.dialogueList.Add(child.gameObject);
                        }
                        GameManager.Instance.animWasStop = true;


                    }


                }

            }

            else if (GameManager.Instance.ObjectHover.tag == "SettingsClose")
            {
                Application.Quit();
            }

            else if (GameManager.Instance.ObjectHover.tag == "ButtonLangue")
            {
                GameManager.Instance.SettingsNarrations(GameManager.Instance.ObjectHover.tag);
            }

            else if (GameManager.Instance.ObjectHover.tag == "Quit")
            {
                //Application.Quit();
            }

        }

    }
    public void StopClick()
    {
        if (draggedObject != null)
        {
            if (draggedObject.tag == "Object" || draggedObject.tag == "Hammer" || draggedObject.tag == "Slider")
            {
                if (draggedObject.GetComponent<Rigidbody2D>() != null)
                {
                    draggedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                }

            }
            if (draggedObject.tag == "Slider" && canThrowHandle)
            {
                GameManager.Instance.CancelLoopingObjects(GameManager.Instance.nameOfLoopingObject);
                float maxValue = posInit - posMaxInit;
                totalSliderValue = (posInit - draggedObject.transform.position.x) / maxValue;

                if (totalSliderValue > 0.75f)
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

            if (draggedObject != null)
            {


                if (draggedObject.GetComponent<ObjectToDrag>() != null)
                {
                    if (draggedObject.GetComponent<ObjectToDrag>().CD)
                    {

                        GameManager.Instance.DotWeenShakeObject(draggedObject, 0.1f, 0.03f, 10);
                        GameManager.Instance.DotWeenShakeObject(GameManager.Instance.DiskPlayer, 0.1f, 0.03f, 10);
                    }
                }

                if (draggedObject == GameManager.Instance.cog1 && GameManager.Instance.ObjectHover == GameManager.Instance.Gauge)
                {
                    draggedObject.GetComponent<SoundDesign>().PhaseOfSound = 3;
                    GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
                    GameManager.Instance.Gauge.layer = 9;
                    GameManager.Instance.Gauge.GetComponent<ObjectToDrag>().enabled = false;
                }
                if (draggedObject == GameManager.Instance.cog3)
                {
                    draggedObject.transform.GetChild(0).GetComponent<AudioSource>().Pause();
                    draggedObject.GetComponent<SoundDesign>().PhaseOfSound = 2;
                    GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
                }
            }
        }
        if (GameManager.Instance.ObjectHover != null)
        {

            if (GameManager.Instance.ObjectHover.tag == "Simon" && nbrOfTimeWeTouch > 0)
            {

                foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                {
                    if (SimonUI.name == "Button_Pause" && SimonUI == GameManager.Instance.ObjectHover && multipleTouchOnTableau2 && GameManager.Instance.canTouchCd)
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
                    if (draggedObject.GetComponent<ObjectToDrag>().Moon)
                    {
                        draggedObject.GetComponent<ShadowCaster2D>().enabled = false;
                        draggedObject.GetComponent<SoundDesign>().PhaseOfSound = 2;
                        GameManager.Instance.NewSound(draggedObject, draggedObject.GetComponent<SoundDesign>().TheVolume);
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
                        GameManager.Instance.DotWeenShakeCamera(0.1f, 0.2f, 10);
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
