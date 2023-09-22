using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;
using DG.Tweening;
using Cinemachine;
using Cinemachine.Utility;

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
    public GameObject camCine;
    public List<AudioClip> TableauxMusic = new List<AudioClip>();
    public GameObject SoundDesign;
    public GameObject ObjectLoopingSound;
    public string nameOfLoopingObject;
    public List<GameObject> LoopingObjects = new List<GameObject>();

    //Tableau 1
    public GameObject Gauge;
    public GameObject S2AT;
    public GameObject S2ATWithWriting;
    public List<Light2D> lightsOnTableau1 = new List<Light2D>();
    public ParticleSystem particlesTableau1;
    public ParticleSystem littleParticlesTableau1;
    public List<Vector2> S2ATPoints;
    List<GameObject> ObjectToRemoveAfterGauge = new List<GameObject>();
    public VideoPlayer introS2AT;
    public List<GameObject> ObjectToMakeVisibleOnBeginning = new List<GameObject>();
    public float pourcentageToMakeObjectVisible;
    public GameObject cog1;
    public bool stopTheBeginning;


    //Tableau 2
    public float MaxVolume = 0.488f;
    public bool isTableau2;
    public ParticleSystem particlesTableau2;
    public GameObject CD;
    public GameObject narratorAnimIdle;
    public List<GameObject> narratorsAnim = new List<GameObject>();
    public List<GameObject> StocksCD = new List<GameObject>();
    public GameObject lightOnScene2;
    public GameObject lightBrokenOnScene2;
    public GameObject Jukebox;
    public GameObject JukeboxBroken;
    public GameObject DiskPlayer;
    public bool canTouchCd = true;
    int antiBugAnimBoutonPause;
    public List<GameObject> feedbackPositive = new List<GameObject>();
    public List<GameObject> feedbackNegative = new List<GameObject>();
    public GameObject ButtonPause;

    //Tableau 3
    public GameObject cog3;
    public GameObject night;
    public List<GameObject> DayNight = new List<GameObject>();
    public List<GameObject> DigicodeView = new List<GameObject>();
    public List<GameObject> AllBooks = new List<GameObject>();
    public List<Sprite> BooksSpriteXL = new List<Sprite>();
    public List<Sprite> BooksSpriteBig = new List<Sprite>();
    public List<Sprite> BooksSpriteMedium = new List<Sprite>();
    public List<Sprite> BooksSpriteSmall = new List<Sprite>();
    public GameObject dayLight;
    public GameObject Digicode;
    public GameObject nightLight;
    public SpriteMask LampMask;
    public GameObject LeftWallAnimation;
    public GameObject ButtonInWall;
    public GameObject BackgroundTableau4;
    public GameObject leftWall;

    //Tableau 4
    public GameObject Egg;
    public GameObject RopePlay;
    public List<GameObject> narratorsAnim4 = new List<GameObject>();
    public GameObject Hammer;
    public GameObject JukeboxBroken4;
    public GameObject ColliderOfJukeboxBroken;
    public List<GameObject> JukeboxPhase = new List<GameObject>();
    int JukeBoxHP = 20;

    //Tableau 5
    public List<GameObject> Credit = new List<GameObject>();
    public GameObject Door;
    public GameObject blackSquare;
    public GameObject TalkingAboutDoor;
    public GameObject CreditText;
    public List<GameObject> Explosions = new List<GameObject>();

    public List<GameObject> ON = new List<GameObject>();

    public Physics2DRaycaster Raycaster2D;
    string tableau;

    public GameObject tableau1;
    public GameObject tableau2;
    public GameObject tableau3;
    public GameObject tableau4;
    public GameObject tableau5;
    public GameObject endGame;

    public DragAndDrop dAD;

    //Option
    public GameObject French;
    public GameObject English;
    private int language;

    public List<GameObject> dialogueList;
    public List<GameObject> dialogueMomentList;
    public int IdDialogue;
    public int IdDialogueMoment;
    public int LastIdDialogueMoment;
    public int LastIdDialogue;
    public GameObject bocksSpeak;
    public GameObject bocksMomentSpeak;
    public bool animWasStop;

    public GameObject diReturnGame;

    public GameObject forground;
    public GameObject camera;

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
            introS2AT.gameObject.SetActive(false);
            //diReturnGame.SetActive(true);
            IdDialogueMoment = 3;
            ChangeDialogueMoment();
        }
        introS2AT.loopPointReached += IntroS2AT_loopPointReached;
    }

    // Update is called once per frame
    void Update()
    {
        if(dAD.draggedObject == null && Raycaster2D.eventMask != 503)
        {
            Raycaster2D.eventMask = 503;
        }
        if (pourcentageToMakeObjectVisible > 0)
        {
            if (isTableau2)
            {
                var emission = particlesTableau2.emission;
                emission.rateOverTime = 20 * pourcentageToMakeObjectVisible;
            }
            else if (pourcentageToMakeObjectVisible > 0 && !isTableau2)
            {
                foreach (GameObject ObjectInBegin in ObjectToMakeVisibleOnBeginning)
                {
                    if (!stopTheBeginning)
                    {
                        var emission = particlesTableau1.emission;
                        emission.rateOverTime = 20 * pourcentageToMakeObjectVisible;
                        emission = littleParticlesTableau1.emission;
                        emission.rateOverTime = 20 * pourcentageToMakeObjectVisible;
                        lightsOnTableau1[3].intensity = pourcentageToMakeObjectVisible;
                    }

                    var oppacity = ObjectInBegin.GetComponent<SpriteRenderer>().color;
                    oppacity.a = pourcentageToMakeObjectVisible;
                    ObjectInBegin.GetComponent<SpriteRenderer>().color = oppacity;

                }
                if (!stopTheBeginning)
                {
                    dAD.ChangeLoadingBarScale(pourcentageToMakeObjectVisible / 25, 1, 0);
                    for (int i = 0; i < 3; i++)
                    {
                        lightsOnTableau1[i].intensity = dAD.LightMaxValue[i] * pourcentageToMakeObjectVisible / 25;
                    }
                }

            }
            if (pourcentageToMakeObjectVisible == 1)
            {
                GetComponent<Animator>().enabled = false;
                pourcentageToMakeObjectVisible = 0;
            }
        }

    }

    private void IntroS2AT_loopPointReached(VideoPlayer source)
    {
        BeginTheGame();
        dAD.TableauActual = 1;
        LoadNextLevel();
        source.gameObject.SetActive(false);
    }

    public void BeginTheGame()
    {
        GetComponent<Animator>().enabled = true;
    }

    public void AfterGainSimon()
    {
        //faire l'anim ou le narrateur va appuyer sur le bouton pause
        //faire tomber le disque
        narratorsAnim[2].SetActive(true);
        canTouchCd = false;
        dAD.multipleTouchOnTableau2 = true;
        Dialogue();
    }

    public void LoadNextLevel()
    {
        tableau1.SetActive(false);
        tableau2.SetActive(false);
        tableau3.SetActive(false);
        tableau4.SetActive(false);
        tableau5.SetActive(false);
        if (dAD.TableauActual == 1)
        {
            tableau1.SetActive(true);
        }
        else if (dAD.TableauActual == 2)
        {
            ChangeDialogueMoment();
            tableau2.SetActive(true);
            forground.transform.Translate(-17.58f, 0, 0);
            // camera.transform.Translate(17.58f, 0, 0);
        }
        else if (dAD.TableauActual == 3)
        {
            tableau3.SetActive(true);
            forground.SetActive(false);
            camera.transform.Translate(27.5115f, 0, 0);
            //camera.transform.Translate(-34.58f, 0, 0);

        }
        else if (dAD.TableauActual == 4)
        {
            tableau4.SetActive(true);
            forground.transform.Translate(17.58f, 0, 0);
        }
        else if (dAD.TableauActual == 5)
        {
            tableau5.SetActive(true);
            forground.SetActive(false);
        }
        cleanScene();

        if (dAD.TableauActual != 2)
        {
            changeMusic(dAD.TableauActual);
        }
        else
        {
            isTableau2 = true;
            GetComponent<AudioSource>().Pause();
            GetComponent<Animator>().enabled = true;
        }

    }

    public void changeMusic(int MusicTableau)
    {
        switch (MusicTableau)
        {
            case 1:
                GetComponent<AudioSource>().volume = 0.333f;
                break;
            case 2:
                GetComponent<AudioSource>().volume = 0.488f;
                break;
            case 3:
                GetComponent<AudioSource>().volume = 0.55f;
                break;
            case 4:
                GetComponent<AudioSource>().volume = 0.5f;
                break;
            case 5:
                GetComponent<AudioSource>().volume = 0.396f;
                break;
            case 6:
                GetComponent<AudioSource>().volume = 0.5f;
                break;
            case 7 :
                GetComponent<AudioSource>().volume = 0.5f;
                break;
        }
        GetComponent<AudioSource>().clip = TableauxMusic[MusicTableau - 1];
        GetComponent<AudioSource>().Play();
    }

    public void cleanScene()
    {
        Raycaster2D.eventMask = 503;
        dAD.ObjectPut = null;
        dAD.draggedObject = null;
        ObjectHover = null;
    }

    public void TouchCD(int numberOfTouch)
    {
        //  Narrator.GetComponent<Animator>().SetInteger("nrbOfTouch", numberOfTouch);
        switch (numberOfTouch)
        {
            case 1:
                if (antiBugAnimBoutonPause == 0)
                {
                    antiBugAnimBoutonPause++;
                    Debug.Log("touche une fois");
                    canTouchCd = false;
                    Dialogue();
                    narratorAnimIdle.SetActive(false);
                    narratorsAnim[3].SetActive(true);
                    // timing = 0.5f;
                }
                break;
            case 2:
                if (antiBugAnimBoutonPause == 1)
                {
                    antiBugAnimBoutonPause++;
                    Debug.Log("touche une seconde fois");
                    canTouchCd = false;
                    Dialogue();

                }
                // timing = 0.4f;
                break;
            case >=3:
                if (antiBugAnimBoutonPause == 2)
                {
                    antiBugAnimBoutonPause++;
                    Debug.Log("Detruit l'UI");
                    Dialogue();
                }
                // timing = 0.3f;
                //nous permet de rendre la souris invisible et non utilisable

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
        NewSound(S2ATWithWriting, S2ATWithWriting.GetComponent<SoundDesign>().TheVolume);
        S2ATWithWriting.transform.localScale = Vector3.one;
        S2ATWithWriting.transform.localPosition = new Vector3(0.013f, 1.752f, S2ATWithWriting.transform.position.z);
        GetComponent<DragAndDrop>().MinScale = S2AT.transform.localScale.y;
        GetComponent<DragAndDrop>().MaxScale = 17.62906f;
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
        foreach (GameObject Objects in ObjectToRemoveAfterGauge)
        {
            Objects.SetActive(false);
        }
    }

    public void S2ATQuit()
    {
        cleanScene();
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
        foreach (GameObject ObjetcsDay in DayNight)
        {
            ObjetcsDay.GetComponent<Animator>().enabled = true;
            if (ObjetcsDay.GetComponent<SoundDesign>() != null)
            {
                NewSound(ObjetcsDay, ObjetcsDay.GetComponent<SoundDesign>().TheVolume);
            }
        }
        dayLight.SetActive(false);
        nightLight.SetActive(true);
        night.SetActive(true);
        LampMask.enabled = true;
    }

    public void SearchSpriteInDigicode(bool stopSearch)
    {
        for (int i = 0; i < DigicodeView.Count; i++)
        {
            if (DigicodeView[i].GetComponent<SpriteRenderer>().sprite == null && !stopSearch)
            {
                if(ObjectHover.GetComponent<ObjectToDrag>().Digicode <= 10)
                {
                    DigicodeView[i].GetComponent<SpriteRenderer>().sprite = ObjectHover.GetComponent<SpriteRenderer>().sprite;
                }
                stopSearch = true;
            }
            else if(i == DigicodeView.Count-1 && !stopSearch && ObjectHover.GetComponent<ObjectToDrag>().Digicode > 10)
            {
                foreach(Object obj in DigicodeView)
                {
                    obj.GetComponent<SpriteRenderer>().sprite = null;
                }
                Debug.Log("Accept Code");
                stopSearch = true;
            }
        }
    }
    public void FallTheHole(GameObject UVCross)
    {
        UVCross.SetActive(false);
        ObjectHover = null;
        Raycaster2D.eventMask = 503;
        LeftWallAnimation.GetComponent<Animator>().enabled = true;
        LeftWallAnimation.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void breakingTheWall()
    {
        foreach (Transform child in LeftWallAnimation.transform.parent)
        {
            if (child.gameObject != LeftWallAnimation)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Animator>().enabled = true;
                NewSound(child.gameObject, child.gameObject.GetComponent<SoundDesign>().TheVolume);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        ButtonInWall.SetActive(false);
        BackgroundTableau4.SetActive(true);
    }

    public void DestroyJukebox()
    {
        lightOnScene2.SetActive(false);
        lightBrokenOnScene2.SetActive(true);
        Jukebox.SetActive(false);
        JukeboxBroken.SetActive(true);
    }
    public void Langue()
    {
        if (language == 1)
        {
            French.SetActive(true);
            English.SetActive(false);

        }
        if (language == 0)
        {
            French.SetActive(false);
            English.SetActive(true);
        }
    }

    public void DotWeenShakeObject(GameObject theGameObjectToShake, float time, float strenght, int vibrato)
    {
        DOTween.Shake(() => theGameObjectToShake.transform.position, x => theGameObjectToShake.transform.position = x, time, strenght, vibrato, 45, false);
    }
    public void DotWeenShakeCamera(float time, float strenght, int vibrato)
    {
        camCine.GetComponent<CinemachineVirtualCamera>().enabled = false;
        Camera.main.DOShakePosition(time, strenght, vibrato, fadeOut: false);
        StartCoroutine(enabledTheCamera());
    }

    IEnumerator enabledTheCamera()
    {
        yield return new WaitForSeconds(1);
        camCine.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    public void loseHP()
    {
        JukeBoxHP--;
        Debug.Log(JukeBoxHP);
        int phase = 0;
        DotWeenShakeCamera(0.1f, 0.6f, 30);
        if (JukeBoxHP == 15 || JukeBoxHP == 10 || JukeBoxHP == 5 || JukeBoxHP == 0)
        {
            if (JukeBoxHP == 10)
            {
                phase = 1;
                Destroy(Egg);
                Destroy(RopePlay);
            }
            if (JukeBoxHP == 5)
            {
                phase = 2;
            }
            if (JukeBoxHP == 0)
            {
                
                
            }
            else
            {
                JukeboxBroken4.GetComponent<SpriteRenderer>().sprite = JukeboxPhase[phase].GetComponent<SpriteRenderer>().sprite;
            }

        }
    }

    public void Dialogue()
    {
        bocksSpeak = dialogueList[IdDialogue];
        
        if (IdDialogue - 1 >= 0)
        {
            if (dialogueList[IdDialogue - 1].GetComponent<Narration>() != null)
            {
                dialogueList[IdDialogue - 1].GetComponent<Narration>().endAnAnim();
            }

        }
        if(bocksMomentSpeak != null)
        {
            if(bocksMomentSpeak == bocksSpeak.transform.parent.gameObject)
            {
                bocksSpeak.SetActive(true);
                IdDialogue++;
            }
        }
       
       
    }

    public void ChangeDialogueMoment()
    {
        bocksMomentSpeak = dialogueMomentList[IdDialogueMoment];
        dialogueList.Clear();
        foreach (Transform child in bocksMomentSpeak.transform)
        {
            dialogueList.Add(child.gameObject);
        }
        if (IdDialogueMoment - 1 >= 0)
        {
            //dialogueMomentList[IdDialogueMoment - 1].GetComponent<Narration>().endAnAnim();
            dialogueMomentList[IdDialogueMoment - 1].SetActive(false);
        }
        bocksMomentSpeak.SetActive(true);
        IdDialogueMoment++;
    }

    public void NewSound(GameObject gameObjectWithTheSound, float Volume)
    {
        Debug.Log(gameObjectWithTheSound.GetComponent<SoundDesign>().PhaseOfSound);
        Debug.Log(gameObjectWithTheSound.name);
        GameObject newSoundDesign = Instantiate(SoundDesign);
        switch (gameObjectWithTheSound.GetComponent<SoundDesign>().PhaseOfSound)
        {
            case 1:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList1[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList1.Count)];
                break;
            case 2:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList2[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList2.Count)];
                break;
            case 3:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3.Count)];
                break;
            case 4:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList4[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3.Count)];
                break;
        }
        newSoundDesign.GetComponent<AudioSource>().volume = Volume;
        newSoundDesign.GetComponent<AudioSource>().Play();
    }

    public void SoundLoop(GameObject gameObjectWithTheSound, float timing)
    {
        GameObject newSoundDesign = Instantiate(ObjectLoopingSound);
        switch (gameObjectWithTheSound.GetComponent<SoundDesign>().PhaseOfSound)
        {
            case 1:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList1[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList1.Count)];
                newSoundDesign.GetComponent<SoundDesign>().clipList1 = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList1;
                break;
            case 2:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList2[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList2.Count)];
                newSoundDesign.GetComponent<SoundDesign>().clipList1 = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList2;
                break;
            case 3:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3.Count)];
                newSoundDesign.GetComponent<SoundDesign>().clipList1 = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3;
                break;
            case 4:
                newSoundDesign.GetComponent<AudioSource>().clip = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList4[Random.Range(0, gameObjectWithTheSound.GetComponent<SoundDesign>().clipList3.Count)];
                newSoundDesign.GetComponent<SoundDesign>().clipList1 = gameObjectWithTheSound.GetComponent<SoundDesign>().clipList4;
                break;
        }
        nameOfLoopingObject = gameObjectWithTheSound.name;
        newSoundDesign.GetComponent<SoundDesign>().Timing = timing;
        newSoundDesign.name = nameOfLoopingObject;
        LoopingObjects.Add(newSoundDesign);
    }

    public void CancelLoopingObjects(string nameOfLoopingObjects)
    {
        print(LoopingObjects.Count);
        for (int i = 0; i < LoopingObjects.Count; i++)
        {
            if (LoopingObjects[i].name == nameOfLoopingObjects)
            {
                Destroy(LoopingObjects[i].gameObject);
                LoopingObjects.Remove(LoopingObjects[i]);

                nameOfLoopingObject = null;
            }
        }


    }
    public void SettingsNarrations(string tags)
    {
        switch (tags)
        {
            case "ButtonLangue":
                bocksMomentSpeak.SetActive(true);
                IdDialogue = 0;
                Dialogue();
                break;
            case "ButtonMusic":
                bocksMomentSpeak.SetActive(true);
                IdDialogue = 1;
                Dialogue();
                break;
        }
    }

    public void ChangeSpriteBook(GameObject book)
    {
        bool sizeIsFind = false;
        sizeIsFind = FindTheGoodSize(book, BooksSpriteXL);
        if (!sizeIsFind)
        {
            sizeIsFind = FindTheGoodSize(book, BooksSpriteBig);
        }
        if (!sizeIsFind)
        {
            sizeIsFind = FindTheGoodSize(book, BooksSpriteMedium);
        }
        if (!sizeIsFind)
        {
            sizeIsFind = FindTheGoodSize(book, BooksSpriteSmall);
        }
    }

    public bool FindTheGoodSize(GameObject book, List<Sprite> BooksSpriteSize)
    {
        bool sizeIsFind = false;
        foreach (Sprite bookSprite in BooksSpriteSize)
        {
            if (book.GetComponent<SpriteRenderer>().sprite == bookSprite)
            {
                sizeIsFind = true;
                List<Sprite> booksSprite = new List<Sprite>();
                booksSprite.Remove(bookSprite);
                book.GetComponent<SpriteRenderer>().sprite = BooksSpriteSize[Random.Range(0, BooksSpriteSize.Count)];
            }
        }
        return sizeIsFind;
    }
}

