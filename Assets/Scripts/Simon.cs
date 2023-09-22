using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class Simon : MonoBehaviour
{
    public List<string> infiniteGame = new List<string>();
    public List<string> ComparativeGame = new List<string>();
    public List<Color> AllLight = new List<Color>();
    public TextMeshProUGUI UIText;
    public GameObject Jukebox;
    public Light2D LightUp;
    public Light2D LightLeft;
    public Light2D LightRight;
    public GameObject YellowContour;
    public GameObject RedContour;
    public GameObject BlueContour;
    bool waitIntro;
    public bool IsShowingLight;

    int ID;
    public List<int> AllID = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Light2D firstLight = LightUp;
        Light2D secondLight = LightRight;
        Light2D thirdLight = LightLeft;
        Color[] light2Ds = { firstLight.color, secondLight.color, thirdLight.color };
        AllLight.AddRange(light2Ds);
    }

    // Update is called once per frame
    void Update()
    {
        if (ComparativeGame.Count == infiniteGame.Count)
        {
            //Permet de voir si nous avons appuyer dans le bon ordre des boutons
            int nbrCorrect = 0;
            for (int i = 0; i < ComparativeGame.Count; i++)
            {

                if (ComparativeGame[i] == infiniteGame[i])
                {

                    nbrCorrect++;

                    if (nbrCorrect == ComparativeGame.Count)
                    {
                        //Si il est bon on va pouvoir rajouter une couleur
                        RemoveStringFromList(ComparativeGame);
                        AddLights();
                    }
                }
            }
        }
    }

    public void AddLights()
    {
        if (infiniteGame.Count == 5)
        {
            RemoveStringFromList(infiniteGame);
            GameManager.Instance.AfterGainSimon();
        }

        else
        {
            //Permet de rajouter une couleur au hasard
            switch (Random.Range(0, 3))
            {
                case 0:
                    infiniteGame.Add("Play");
                    break;
                case 1:
                    infiniteGame.Add("Credits");
                    break;
                case 2:
                    infiniteGame.Add("Settings");
                    break;
            }
            GameManager.Instance.GetComponent<AudioSource>().volume = GameManager.Instance.MaxVolume * infiniteGame.Count * 20 / 100;
            StartCoroutine(ShowLight());
        }
    }
    IEnumerator ShowLight()
    {
        ShutDownContour();
        IsShowingLight = true;
        int LightID = ID;
        ID++;
        AllID.Add(LightID);
        if (UIText != null)
        {
            //Permet d'afficher la couleur qu'on va devoir appuyer
            yield return new WaitForSeconds(0.5f);

            foreach (string light in infiniteGame)
            {
                /*   UIText.text = light;
                   yield return new WaitForSeconds(0.2f);
                   UIText.text = "";
                   yield return new WaitForSeconds(0.1f);*/

                switch (light)
                {
                    case "Play":
                        LightUp.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        LightRight.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        LightLeft.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        RedContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Play")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                    case "Credits":
                        LightUp.color = new Color(1, 0.7112604f, 0, 1);
                        LightRight.color = new Color(1, 0.7112604f, 0, 1);
                        LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                        YellowContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Credit")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                    case "Settings":
                        LightUp.color = new Color(0, 0.2810159f, 1, 1);
                        LightRight.color = new Color(0, 0.2810159f, 1, 1);
                        LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                        BlueContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Option")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                }
                LightUp.GetComponent<Animator>().enabled = false;
                LightRight.GetComponent<Animator>().enabled = false;
                LightLeft.GetComponent<Animator>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                if (LightID == AllID[AllID.Count - 1])
                {

                    LightUp.GetComponent<Animator>().enabled = true;
                    LightRight.GetComponent<Animator>().enabled = true;
                    LightLeft.GetComponent<Animator>().enabled = true;
                    LightUp.color = AllLight[0];
                    LightRight.color = AllLight[1];
                    LightLeft.color = AllLight[2];
                    yield return new WaitForSeconds(0.5f);
                }
            }
            /*  yield return new WaitForSeconds(0.2f);
              UIText.text = "";*/
        }
        yield return new WaitForSeconds(0.5f);
        if (LightID == AllID[AllID.Count - 1])
        {

            LightUp.GetComponent<Animator>().enabled = true;
            LightRight.GetComponent<Animator>().enabled = true;
            LightLeft.GetComponent<Animator>().enabled = true;
            LightUp.color = AllLight[0];
            LightRight.color = AllLight[1];
            LightLeft.color = AllLight[2];
            ShutDownContour();
            yield return new WaitForSeconds(0.5f);
        }
        if (infiniteGame.Count > 0)
        {
            foreach (string light in infiniteGame)
            {
                /*   UIText.text = light;
                   yield return new WaitForSeconds(0.2f);
                   UIText.text = "";
                   yield return new WaitForSeconds(0.1f);*/
                /*-   LightUp.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                   LightRight.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                   LightLeft.color = new Color(0.9528302f, 0.0759992f, 0, 1);*/

                switch (light)
                {
                    case "Play":
                        LightUp.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        LightRight.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        LightLeft.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                        RedContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Play")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                    case "Credits":
                        LightUp.color = new Color(1, 0.7112604f, 0, 1);
                        LightRight.color = new Color(1, 0.7112604f, 0, 1);
                        LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                        YellowContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Credit")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                    case "Settings":
                        LightUp.color = new Color(0, 0.2810159f, 1, 1);
                        LightRight.color = new Color(0, 0.2810159f, 1, 1);
                        LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                        BlueContour.SetActive(true);
                        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                        {
                            if (SimonUI.name == "Button_Option")
                            {
                                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                            }
                        }
                        break;
                }
                LightUp.GetComponent<Animator>().enabled = false;
                LightRight.GetComponent<Animator>().enabled = false;
                LightLeft.GetComponent<Animator>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                IsShowingLight = false;
                if (LightID == AllID[AllID.Count - 1])
                {
                    LightUp.GetComponent<Animator>().enabled = true;
                    LightRight.GetComponent<Animator>().enabled = true;
                    LightLeft.GetComponent<Animator>().enabled = true;
                    LightUp.color = AllLight[0];
                    LightRight.color = AllLight[1];
                    LightLeft.color = AllLight[2];
                    ShutDownContour();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

    }
    public void AddToComparative(string theNew)
    {

        //Indique quel bouton on a appuyer
        if (!IsShowingLight)
        {
            switch (theNew)
            {
                case "Button_Play":
                    GetComponent<Simon>().ComparativeGame.Add("Play");
                    break;
                case "Button_Credit":
                    GetComponent<Simon>().ComparativeGame.Add("Credits");
                    break;
                case "Button_Option":
                    GetComponent<Simon>().ComparativeGame.Add("Settings");
                    break;
            }
            StartCoroutine(PlayWithLight(theNew));
            //Permet de comparer le dernier bouton appuyer à la liste de couleur faite au hasard, si le bouton est mauvais alors le jeu es perdu
            if (ComparativeGame[ComparativeGame.Count - 1] != infiniteGame[ComparativeGame.Count - 1])
            {
                
                GameObject newFeedback = Instantiate(GameManager.Instance.feedbackNegative[Random.Range(0, GameManager.Instance.feedbackNegative.Count)]);
                foreach (Transform child in GameManager.Instance.ObjectHover.transform.parent)
                {
                    if(child.name == "Pos")
                    {
                        GameObject newChild = Instantiate(child.gameObject);
                        newChild.transform.position = new Vector2(Random.Range(-1.00f, 1.00f) + newChild.transform.position.x, Random.Range(-1.00f, 1.00f) + newChild.transform.position.y);
                        newFeedback.transform.parent = newChild.transform;
                    }
                }
                

                GameManager.Instance.GetComponent<AudioSource>().Pause();
                foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
                {
                    if (SimonUI.name == "Button_Pause")
                    {
                        SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 3;
                        GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
                    }
                }
                RemoveStringFromList(infiniteGame);
                RemoveStringFromList(ComparativeGame);
            }
            else
            {
                GameObject newFeedback = Instantiate(GameManager.Instance.feedbackPositive[Random.Range(0, GameManager.Instance.feedbackPositive.Count)], GameManager.Instance.ObjectHover.transform.position, Quaternion.identity);
                foreach (Transform child in GameManager.Instance.ObjectHover.transform.parent)
                {
                    if (child.name == "Pos")
                    {
                        GameObject newChild = Instantiate(child.gameObject);
                        newChild.transform.position = new Vector2(Random.Range(-1.00f, 1.00f) + newChild.transform.position.x, Random.Range(-1.00f, 1.00f) + newChild.transform.position.y);
                        newFeedback.transform.parent = newChild.transform;
                    }
                }
            }
            if (ComparativeGame.Count == infiniteGame.Count)
            {

                int nbrCorrect = 0;
                for (int i = 0; i < ComparativeGame.Count; i++)
                {

                    if (ComparativeGame[i] == infiniteGame[i])
                    {

                        nbrCorrect++;

                        if (nbrCorrect == ComparativeGame.Count)
                        {
                            RemoveStringFromList(ComparativeGame);
                            StartCoroutine(AddTimeAfterWin());
                        }
                    }
                }
            }
        }
    }

    void RemoveStringFromList(List<string> TheList)
    {
        foreach (string stringFromList in TheList.ToList())
        {
            TheList.Remove(stringFromList);
        }
    }

    IEnumerator PlayWithLight(string ButtonName)
    {
        ShutDownContour();
        int LightID = ID;
        ID++;
        AllID.Add(LightID);
        switch (ButtonName)
        {
            case "Button_Play":
                LightUp.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                LightRight.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                LightLeft.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                RedContour.SetActive(true);
                break;
            case "Button_Credit":
                LightUp.color = new Color(1, 0.7112604f, 0, 1);
                LightRight.color = new Color(1, 0.7112604f, 0, 1);
                LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                YellowContour.SetActive(true);
                break;
            case "Button_Option":
                LightUp.color = new Color(0, 0.2810159f, 1, 1);
                LightRight.color = new Color(0, 0.2810159f, 1, 1);
                LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                BlueContour.SetActive(true);
                break;
        }
        LightUp.GetComponent<Animator>().enabled = false;
        LightRight.GetComponent<Animator>().enabled = false;
        LightLeft.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (LightID == AllID[AllID.Count - 1])
        {
            LightUp.GetComponent<Animator>().enabled = true;
            LightRight.GetComponent<Animator>().enabled = true;
            LightLeft.GetComponent<Animator>().enabled = true;
            LightUp.color = AllLight[0];
            LightRight.color = AllLight[1];
            LightLeft.color = AllLight[2];
            ShutDownContour();
            yield return new WaitForSeconds(0.5f);
        }


    }

    void ShutDownContour()
    {
        RedContour.SetActive(false);
        BlueContour.SetActive(false);
        YellowContour.SetActive(false);
    }

    public void BeginTheSimon()
    {
        GameManager.Instance.GetComponent<AudioSource>().Play();
        foreach (GameObject SimonUI in GameManager.Instance.SimonUI)
        {
            if (SimonUI.name == "Button_Pause")
            {
                SimonUI.GetComponent<SoundDesign>().PhaseOfSound = 2;
                GameManager.Instance.NewSound(SimonUI, SimonUI.GetComponent<SoundDesign>().TheVolume);
            }
        }
        RemoveStringFromList(infiniteGame);
        RemoveStringFromList(ComparativeGame);
        StartCoroutine(waitIntroSimon());
    }

    IEnumerator waitIntroSimon()
    {
        if (!waitIntro)
        {
            waitIntro = true;
            yield return new WaitForSeconds(2f);
            AddLights();
            waitIntro = false;
        }
       
    }
    IEnumerator AddTimeAfterWin()
    {
            yield return new WaitForSeconds(0.7f);
            AddLights();
    }
}
