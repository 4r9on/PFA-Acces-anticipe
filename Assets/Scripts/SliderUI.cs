using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public Slider slider;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
