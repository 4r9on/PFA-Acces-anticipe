using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Open", true);
        animator.SetBool("Cadre rouge", true);
        animator.SetBool("LED Bleu", true);
        animator.SetBool("LED Orange", true);
        animator.SetBool("LED Rose", true);
        animator.SetBool("Fleche Rouge", true);
        animator.SetBool("Fleche Rouge 2", true);
        animator.SetBool("Fleche Rouge 3", true);
        animator.SetBool("Game Pancarte", true);
        animator.SetBool("Porte Tremble", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        //animator.SetBool("Porte Tremble", true);
    }
}
