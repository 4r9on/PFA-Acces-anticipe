using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Simon : MonoBehaviour
{
    public List<string> infiniteGame = new List<string>();
    public List<string> ComparativeGame = new List<string>();
    public TextMeshProUGUI UIText;
    public GameObject Jukebox;

    // Start is called before the first frame update
    void Start()
    {

        AddLights();
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

    void AddLights()
    {
        if (infiniteGame.Count == 5)
        {
            RemoveStringFromList(infiniteGame);
            Jukebox.SetActive(true);
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
                    infiniteGame.Add("Quit");
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
        //Permet d'afficher la couleur qu'on va devoir appuyer
        yield return new WaitForSeconds(0.2f);
        foreach (string light in infiniteGame)
        {
            UIText.text = light;
            yield return new WaitForSeconds(0.2f);
            UIText.text = "";
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        UIText.text = "";

    }
    public void AddToComparative(string theNew)
    {
        //Indique quel bouton on a appuyer
        switch (theNew)
        {
            case "PlayButton":
                GetComponent<Simon>().ComparativeGame.Add("Play");
                break;
            case "QuitButton":
                GetComponent<Simon>().ComparativeGame.Add("Quit");
                break;
            case "SettingsButton":
                GetComponent<Simon>().ComparativeGame.Add("Settings");
                break;
        }
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
}
