using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightGauge : MonoBehaviour
{
    public Animator Night;

    Animator animator;

    public Slider slider;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(slider.normalizedValue == 1)
        {
            animator.Play("Night");
            Debug.Log("aaaaaa");
        }
    }

    /*public void OnValueChanged()
    {
        Animator.Play("Night", -1, slider.normalizedValue);
    }*/
}
