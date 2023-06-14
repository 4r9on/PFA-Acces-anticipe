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

            StartCoroutine(ShowLight());
        }
    }
    IEnumerator ShowLight()
    {
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
                        break;
                    case "Credits":
                        LightUp.color = new Color(1, 0.7112604f, 0, 1);
                        LightRight.color = new Color(1, 0.7112604f, 0, 1);
                        LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                        break;
                    case "Settings":
                        LightUp.color = new Color(0, 0.2810159f, 1, 1);
                        LightRight.color = new Color(0, 0.2810159f, 1, 1);
                        LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                        break;
                }
                LightUp.GetComponent<Animator>().enabled = false;
                LightUp.GetComponent<Animator>().enabled = false;
                LightUp.GetComponent<Animator>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                if (LightID == AllID[AllID.Count - 1])
                {

                    LightUp.GetComponent<Animator>().enabled = true;
                    LightUp.GetComponent<Animator>().enabled = true;
                    LightUp.GetComponent<Animator>().enabled = true;
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
                    break;
                case "Credits":
                    LightUp.color = new Color(1, 0.7112604f, 0, 1);
                    LightRight.color = new Color(1, 0.7112604f, 0, 1);
                    LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                    break;
                case "Settings":
                    LightUp.color = new Color(0, 0.2810159f, 1, 1);
                    LightRight.color = new Color(0, 0.2810159f, 1, 1);
                    LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                    break;
            }
            LightUp.GetComponent<Animator>().enabled = false;
            LightUp.GetComponent<Animator>().enabled = false;
            LightUp.GetComponent<Animator>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            if (LightID == AllID[AllID.Count - 1])
            {
                LightUp.GetComponent<Animator>().enabled = true;
                LightUp.GetComponent<Animator>().enabled = true;
                LightUp.GetComponent<Animator>().enabled = true;
                LightUp.color = AllLight[0];
                LightRight.color = AllLight[1];
                LightLeft.color = AllLight[2];
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public void AddToComparative(string theNew)
    {

        //Indique quel bouton on a appuyer
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
            UIText.text = "Defeat";
            RemoveStringFromList(infiniteGame);
            RemoveStringFromList(ComparativeGame);
            AddLights();
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
                        AddLights();
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
        int LightID = ID;
        ID++;
        AllID.Add(LightID);
        switch (ButtonName)
        {
            case "Button_Play":
                LightUp.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                LightRight.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                LightLeft.color = new Color(0.9528302f, 0.0759992f, 0, 1);
                break;
            case "Button_Credit":
                LightUp.color = new Color(1, 0.7112604f, 0, 1);
                LightRight.color = new Color(1, 0.7112604f, 0, 1);
                LightLeft.color = new Color(1, 0.7112604f, 0, 1);
                break;
            case "Button_Option":
                LightUp.color = new Color(0, 0.2810159f, 1, 1);
                LightRight.color = new Color(0, 0.2810159f, 1, 1);
                LightLeft.color = new Color(0, 0.2810159f, 1, 1);
                break;
        }
        LightUp.GetComponent<Animator>().enabled = false;
        LightUp.GetComponent<Animator>().enabled = false;
        LightUp.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (LightID == AllID[AllID.Count - 1])
        {
            LightUp.GetComponent<Animator>().enabled = true;
            LightUp.GetComponent<Animator>().enabled = true;
            LightUp.GetComponent<Animator>().enabled = true;
            LightUp.color = AllLight[0];
            LightRight.color = AllLight[1];
            LightLeft.color = AllLight[2];
            yield return new WaitForSeconds(0.5f);
        }


    }

    public void BeginTheSimon()
    {
        RemoveStringFromList(infiniteGame);
        RemoveStringFromList(ComparativeGame);
        AddLights();
    }
}
