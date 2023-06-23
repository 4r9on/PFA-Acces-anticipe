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
    public bool isTableau2;
    public ParticleSystem particlesTableau2;
    public GameObject CD;
    public List<GameObject> narratorsAnim = new List<GameObject>();
    public List<GameObject> StocksCD = new List<GameObject>();
    public GameObject lightOnScene2;
    public GameObject lightBrokenOnScene2;
    public GameObject Jukebox;
    public GameObject JukeboxBroken;
    public GameObject DiskPlayer;
    public bool canTouchCd = true;

    //Tableau 3
    public GameObject cog3;
    public List<GameObject> DayNight = new List<GameObject>();
    public GameObject dayLight;
    public GameObject nightLight;
    public SpriteMask LampMask;
    public GameObject LeftWallAnimation;
    public GameObject ButtonInWall;
    public GameObject BackgroundTableau4;
    public GameObject leftWall;

    //Tableau 4
    public List<GameObject> narratorsAnim4 = new List<GameObject>();
    public GameObject Hammer;
    public GameObject JukeboxBroken4;
    public GameObject ColliderOfJukeboxBroken;
    public List<GameObject> JukeboxPhase = new List<GameObject>();
    int JukeBoxHP = 20;

    public List<GameObject> ON = new List<GameObject>();

    public Physics2DRaycaster Raycaster2D;
    string tableau;

    public GameObject tableau1;
    public GameObject tableau2;
    public GameObject tableau3;
    public GameObject tableau4;
    public GameObject tableau5;

    public DragAndDrop dAD;

    //Option
    public GameObject French;
    public GameObject English;
    private int language;

    public List<GameObject> dialogueList;
    public int i;
    public GameObject bocksSpeak;
    public bool truc;

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
            diReturnGame.SetActive(true);
        }
        introS2AT.loopPointReached += IntroS2AT_loopPointReached;
    }

    // Update is called once per frame
    void Update()
    {
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
        canTouchCd = true;
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
            tableau2.SetActive(true);
            tableau2.transform.Translate(17.58f, 0, 0);
            camera.transform.Translate(17.58f, 0, 0);
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
        }
        GetComponent<AudioSource>().clip = TableauxMusic[MusicTableau - 1];
        GetComponent<AudioSource>().Play();
    }

    public void cleanScene()
    {
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
                Debug.Log("touche une fois");
                canTouchCd = false;
                Dialogue();

                narratorsAnim[3].SetActive(true);
                // timing = 0.5f;
                break;
            case 2:
                Debug.Log("touche une seconde fois");
                canTouchCd = false;
                Dialogue();

                narratorsAnim[1].SetActive(true);

                // timing = 0.4f;
                break;
            case 3:
                Debug.Log("Detruit l'UI");
                Dialogue();

                narratorsAnim[4].SetActive(true);
                narratorsAnim[5].SetActive(true);

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
        NewSound(S2ATWithWriting);
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
        }
        dayLight.SetActive(false);
        nightLight.SetActive(true);
        LampMask.enabled = true;
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
        Debug.Log("aaa");
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
        if (JukeBoxHP == 15 || JukeBoxHP == 10 || JukeBoxHP == 5 || JukeBoxHP == 0)
        {
            if (JukeBoxHP == 10)
            {
                phase = 1;
            }
            if (JukeBoxHP == 5)
            {
                phase = 2;
            }
            if (JukeBoxHP == 0)
            {
                PlayerPrefs.SetInt("GetCrashed", 1);
                GetComponent<LaunchBat>().ExitAppThenRestart();
            }
            else
            {
                JukeboxBroken4.GetComponent<SpriteRenderer>().sprite = JukeboxPhase[phase].GetComponent<SpriteRenderer>().sprite;
            }

        }
    }

    public void Dialogue()
    {
        truc = true;
        if (truc == true)
        {
            bocksSpeak.SetActive(true);
            bocksSpeak = dialogueList[i];
            i++;
            truc = false;

        }
    }

    public void NewSound(GameObject gameObjectWithTheSound)
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


}

