using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
    public int DialogueMax;
    public int DialogueActual;
    public Animation anim;
    public bool NeedToPass;
    public int DialoguePlus   // property
    {
        get { return DialogueActual; }   // get method
        set
        {
            DialogueActual += 1;
            OpenAnotherDialogue();
        }  // set method
    }
    public bool FinishTheDialogue;
    public bool isAPhase;
    // Start is called before the first frame update
    void Start()
    {
        if(isAPhase)
        {
            DialoguePlus++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenAnotherDialogue()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Narration>().DialogueActual == DialogueActual)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void endAnAnim()
    {
        FinishTheDialogue = true;
        foreach (Transform child in transform)
        {
            child.GetComponent<Animator>().SetBool("EndNarration", true);
            if (child.gameObject.activeInHierarchy)
            {
                FinishTheDialogue = false;
            }
        }
        if (FinishTheDialogue)
        {
            gameObject.SetActive(false);
        }
    }

    public void BeginningOfAnim()
    {
        if(GameManager.Instance.dialogueList[GameManager.Instance.IdDialogue-1] == transform.parent.gameObject || !NeedToPass)
        {
            gameObject.GetComponent<SoundDesign>().PhaseOfSound = 1;
            GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    public void endSoundOfAnim()
    {
        gameObject.GetComponent<SoundDesign>().PhaseOfSound = 2;
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
    }

    public void endOfAnim()
    {
        gameObject.transform.parent.GetComponent<Narration>().DialoguePlus++;
        gameObject.SetActive(false);
    }

    public void endOfThePhase()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
