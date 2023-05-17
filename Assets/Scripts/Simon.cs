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

            int nbrCorrect = 0;
            for (int i = 0; i < ComparativeGame.Count; i++)
            {

                if (ComparativeGame[i] == infiniteGame[i])
                {

                    nbrCorrect++;

                    if (nbrCorrect == ComparativeGame.Count)
                    {

                        foreach (string Comparative in ComparativeGame.ToList())
                        {
                            ComparativeGame.Remove(Comparative);
                        }
                        AddLights();
                    }
                    else
                    {

                    }

                }
            }


        }
    }

    void AddLights()
    {
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
    IEnumerator ShowLight()
    {
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
        if (ComparativeGame[ComparativeGame.Count - 1] != infiniteGame[ComparativeGame.Count - 1])
        {
            UIText.text = "Defeat";
            foreach (string theGame in infiniteGame.ToList())
            {
                infiniteGame.Remove(theGame);
            }
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

                        foreach (string Comparative in ComparativeGame.ToList())
                        {
                            ComparativeGame.Remove(Comparative);
                        }
                        AddLights();
                    }
                }
            }
        }
    }
}
