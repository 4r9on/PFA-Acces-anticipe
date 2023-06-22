using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    public List<GameObject> dialogueList2;

    public GameObject dialogue;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //animator.SetBool("Porte Tremble", true);
        
        StartCoroutine(Text());
    }

    IEnumerator Text()
    {
        Debug.Log("mlk");
        dialogue.SetActive(true);


        animator.SetBool("Open", true);
       // dialogueList2[0].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Cadre rouge", true);
       // dialogueList2[0].SetActive(false);
       // dialogueList2[1].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("LED Bleu", true);
       // dialogueList2[1].SetActive(false);
       // dialogueList2[2].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("LED Orange", true);
      //  dialogueList2[2].SetActive(false);
      //  dialogueList2[3].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("LED Rose", true);
       // dialogueList2[3].SetActive(false);
       // dialogueList2[4].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Fleche Rouge", true);
       // dialogueList2[4].SetActive(false);
       // dialogueList2[5].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Fleche Rouge 2", true);
       // dialogueList2[5].SetActive(false);
       // dialogueList2[6].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Fleche Rouge 3", true);
       // dialogueList2[6].SetActive(false);
       // dialogueList2[7].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Game Pancarte", true);
       // dialogueList2[7].SetActive(false);
       // dialogueList2[8].SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Porte Tremble", true);
        //dialogueList2[8].SetActive(false);
        Debug.Log("play");

    }


}
