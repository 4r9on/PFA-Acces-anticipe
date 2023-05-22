using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public Slider slider;

    public Sprite leftBar0;
    public Sprite leftBar1;
    public Sprite leftBar2;
    public Sprite leftBar3;  
    public Sprite leftBar4;
    public Sprite leftBar5;
    public Sprite leftBar6;
    public Sprite leftBar7;
    public Sprite leftBar8;
    public Sprite leftBar9;
    public Sprite leftBar10;
    public Sprite leftBar11;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value != 0)
        {

        }



    }




    public void sliderValueChanged()
    {
        if(slider.value >= 10.0f)
        {
            Debug.Log("anim");

        }
        else if(slider.value <= 10.0f)
        {
            Debug.Log("diminu");
            slider.value -= 1f;
        }
    }
}
