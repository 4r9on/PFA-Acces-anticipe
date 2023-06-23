using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Narrator : MonoBehaviour
{
    bool KnowYouComeToScene4;
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
    }

    public void HitPauseButton()
    {
        GameManager.Instance.DotWeenShakeCamera(0.2f, 1f, 40);
        GameManager.Instance.narratorsAnim[9].SetActive(true);
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
    }
    public void SecondLampToLightOn()
    {
        GameManager.Instance.lightOnScene2.transform.GetChild(4).gameObject.SetActive(true);
    }
    public void LastLampToLightOn()
    {
        GameManager.Instance.lightOnScene2.transform.GetChild(3).gameObject.SetActive(true);
        GameManager.Instance.lightOnScene2.transform.GetChild(5).GetComponent<Light2D>().intensity = 0.48f;
        GameManager.Instance.lightOnScene2.transform.GetChild(5).GetComponent<Animator>().enabled = true;
        GameManager.Instance.lightOnScene2.transform.GetChild(6).gameObject.SetActive(true);
        GameManager.Instance.changeMusic(2);
    }

    public void ClickedOnCD()
    {
        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "Button_Pause")
            {
                SimonUI.GetComponent<Animator>().SetBool("IsClicked", true);
            }
        }
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
       DestroyHands();
    }

    public void RemoveDiskPlayer()
    {
        GameManager.Instance.DiskPlayer.SetActive(false);
        GameManager.Instance.CD.SetActive(false);
        // GameManager.Instance.CD.GetComponent<Rigidbody2D>().velocity = new Vector2 (10, 10);
        // GameManager.Instance.CD.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void DestroyJukebox()
    {
        GameManager.Instance.DestroyJukebox();
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
        GameManager.Instance.BackgroundTableau4.SetActive(true);
        GameManager.Instance.tableau2.SetActive(false);
        GameManager.Instance.dAD.TableauActual = 3;
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
            endOfScene4();
        }
    }
    public void HammerIsPutted()
    {
        GameManager.Instance.Hammer.SetActive(true);
        endOfScene4();
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

    public void DestroyHands()
    {
        gameObject.SetActive(false);
    }
}
