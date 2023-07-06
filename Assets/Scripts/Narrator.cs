using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Narrator : MonoBehaviour
{
    bool KnowYouComeToScene4;
    public bool secondTimeHandHitButton;
    public float value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!KnowYouComeToScene4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                KnowYouComeToScene4 = true;
            }
        }
        if(gameObject == GameManager.Instance.blackSquare)
        {
            if (value == 1)
            {
                Debug.Log("oppa1");
                GameManager.Instance.endGame.SetActive(true);
                GameManager.Instance.changeMusic(6);
                GameManager.Instance.tableau5.SetActive(false);
            }
            var OppacityView = gameObject.GetComponent<SpriteRenderer>().color;
            OppacityView.a =  value;
            gameObject.GetComponent<SpriteRenderer>().color = OppacityView;
           
        }
    }

    public void willHitTheButton()
    {
        GameManager.Instance.narratorAnimIdle.SetActive(false);
        GameManager.Instance.narratorsAnim[1].SetActive(true);
    }

    public void HitPauseButton()
    {
        GameManager.Instance.DotWeenShakeCamera(0.2f, 1f, 40);
        GameManager.Instance.narratorsAnim[9].SetActive(true);
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        GameManager.Instance.Dialogue();
        GameManager.Instance.GetComponent<AudioSource>().Pause();
        foreach (GameObject obj in GameManager.Instance.SimonUI)
        {
            if (obj.name == "Button_Pause")
            {
                obj.SetActive(false);
            }
        }
       // GameManager.Instance.StockCD.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance.StocksCD[0].GetComponent<SpriteRenderer>().sprite;
       // GameManager.Instance.StockCD.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Principal Subject";
        GameManager.Instance.StockCD.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GameManager.Instance.StockCD.tag = "Object";
        GameManager.Instance.StockCD.GetComponent<ObjectToDrag>().BornWithoutGravity = 1;
        GameManager.Instance.cog1.transform.parent = GameManager.Instance.tableau2.transform;
        GameManager.Instance.cog1.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }

    public void firstLampToLightOn()
    {
       GameManager.Instance.lightOnScene2.transform.GetChild(2).gameObject.SetActive(true);
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
    }
    public void SecondLampToLightOn()
    {
        GameManager.Instance.lightOnScene2.transform.GetChild(4).gameObject.SetActive(true);
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
    }
    public void LastLampToLightOn()
    {
        
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        GameManager.Instance.lightOnScene2.transform.GetChild(3).gameObject.SetActive(true);
        GameManager.Instance.lightOnScene2.transform.GetChild(5).GetComponent<Light2D>().intensity = 0.48f;
        GameManager.Instance.lightOnScene2.transform.GetChild(5).GetComponent<Animator>().enabled = true;
        GameManager.Instance.lightOnScene2.transform.GetChild(6).gameObject.SetActive(true);
        GameManager.Instance.lightOnScene2.transform.GetChild(7).gameObject.SetActive(true);
        GameManager.Instance.changeMusic(2);
        GameManager.Instance.GetComponent<AudioSource>().volume = 0;
    }

    public void ClickedOnCD()
    {
        if (secondTimeHandHitButton)
        {
            GameManager.Instance.DotWeenShakeCamera(0.2f, 0.5f, 20);
        }
        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "Button_Pause")
            {
                SimonUI.GetComponent<Animator>().SetBool("IsClicked", true);
                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 1;
                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
            }
        }
        GameManager.Instance.GetComponent<AudioSource>().Pause();
        // GameManager.Instance.CD.GetComponent<Animator>().SetBool("IsClicked", true);
    }

    public void UnClickedOnCD()
    {
       
        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "Button_Pause")
            {
                SimonUI.GetComponent<Animator>().SetBool("IsClicked", false);
            }
        }
        //GameManager.Instance.CD.GetComponent<Animator>().SetBool("IsClicked", false);
    }

    public void YouCanTouchCd()
    {
        GameManager.Instance.canTouchCd = true;
        if (secondTimeHandHitButton)
        {

            GameManager.Instance.Dialogue();
        }
        GameManager.Instance.narratorAnimIdle.SetActive(true);
        DestroyHands();
    }

    public void RemoveDiskPlayer()
    {
        GameManager.Instance.DiskPlayer.SetActive(false);
        GameManager.Instance.CD.SetActive(false);
        // GameManager.Instance.CD.GetComponent<Rigidbody2D>().velocity = new Vector2 (10, 10);
        // GameManager.Instance.CD.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    public void ExplosionSound()
    {
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        GameManager.Instance.GetComponent<AudioSource>().Pause();
        Camera.main.transform.parent.GetComponent<Animator>().enabled = true;
    }
    public void DestroyJukebox()
    {
        GameManager.Instance.DestroyJukebox();
        
    }

    public void NextDialogue()
    {
        GameManager.Instance.Dialogue();
    }
    public void EndOfScene2()
    {
        for (int i = 0; i < GameManager.Instance.narratorsAnim.Count - 1; i++)
        {
            if (GameManager.Instance.narratorsAnim[i] == gameObject)
            {
                GameManager.Instance.narratorsAnim[i + 1].SetActive(true);
               
                if (i == 7)
                {
                    GameManager.Instance.cog1.transform.parent = GameManager.Instance.cog1.transform.parent.parent;
                    GameManager.Instance.cog1.GetComponent<BoxCollider2D>().enabled = false;
                    GameManager.Instance.narratorsAnim[i + 1].SetActive(false);
                    GameManager.Instance.narratorsAnim[i + 1].transform.parent = GameManager.Instance.narratorsAnim[i + 1].transform.parent.parent.parent.parent ;
                    GameManager.Instance.narratorsAnim[i + 1].SetActive(true);
                    GameManager.Instance.tableau2.transform.position = new Vector2(-19.78f, 0);
                    GameManager.Instance.tableau2.transform.parent = GameManager.Instance.tableau3.transform;
                    GameManager.Instance.tableau3.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                }
                DestroyHands();
            }
        }
    }

    public void crashTheCog()
    {
        GameManager.Instance.cog1.GetComponent<Animator>().SetBool("CogWillFall", true);
    }

    public void scene2Disapear()
    {
        GameManager.Instance.cleanScene();
        GameManager.Instance.tableau2.SetActive(false);
        GameManager.Instance.dAD.TableauActual = 3;
        GameManager.Instance.forground.transform.Translate(-17.58f, 0, 0);
        GameManager.Instance.changeMusic(GameManager.Instance.dAD.TableauActual);

    }

    public void scene3Disapear()
    {
        GameManager.Instance.cleanScene();
        GameManager.Instance.tableau3.SetActive(false);
        GameManager.Instance.dAD.TableauActual = 4;
        GameManager.Instance.changeMusic(GameManager.Instance.dAD.TableauActual);
    }

    public void PutTheHammer()
    {
        if (KnowYouComeToScene4)
        {
            BeginNarrationTableau4();
        }
    }
    public void HammerIsPutted()
    {
        GameManager.Instance.Hammer.SetActive(true);
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        endOfScene4();
    }

    public void BeginNarrationTableau4()
    {
        KnowYouComeToScene4 = false;
            foreach (Transform child in GameManager.Instance.bocksMomentSpeak.transform)
            {
                child.gameObject.SetActive(true);
            }
        
        
    }

    public void EndOfMonologueTableau4()
    {
        GameManager.Instance.narratorsAnim4[0].SetActive(false);
        GameManager.Instance.narratorsAnim4[1].SetActive(true);
    }

    public void endOfScene4()
    {
        for (int i = 0; i < GameManager.Instance.narratorsAnim4.Count - 1; i++)
        {
            if (GameManager.Instance.narratorsAnim4[i] == gameObject)
            {
                GameManager.Instance.narratorsAnim4[i + 1].SetActive(true);
                DestroyHands();
            }
        }
    }

    public void QuitTheGame()
    {
         PlayerPrefs.SetInt("GetCrashed", 1);
         GameManager.Instance.GetComponent<LaunchBat>().ExitAppThenRestart();
    }

    public void DestroyHands()
    {
        gameObject.SetActive(false);
    }

    public void NarratorWantUsToLeftTheGame()
    {
        GameManager.Instance.changeMusic(7);
         gameObject.GetComponent<Narration>().endSoundOfAnim();
    }

    public void NarratorTalkAboutTheDoor()
    {
        GameManager.Instance.TalkingAboutDoor.SetActive(true);
    }
    public void DoorAppeared()
    {
        GameManager.Instance.Door.SetActive(true);
    }

    public void DeadZoneActive()
    {
        foreach (Transform child in gameObject.transform.parent)
        {
            if (child.tag == "DeadZone")
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void OpenButton(int id)
    {
        gameObject.GetComponent<SoundDesign>().PhaseOfSound = id;
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
    }

    public void afterOpencurtain()
    {
        GameManager.Instance.CreditText.GetComponent<ElevateText>().elevateAllText();
    }

    public void BeginTheExplosionCD()
    {
        GameManager.Instance.narratorAnimIdle.SetActive(false);
        GameManager.Instance.narratorsAnim[4].SetActive(true);
        GameManager.Instance.narratorsAnim[5].SetActive(true);
    }

}
