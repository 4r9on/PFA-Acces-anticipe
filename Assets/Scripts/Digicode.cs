using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Digicode : MonoBehaviour
{
    public TextMeshProUGUI code;
    public void ButtonClicked(int number)
    {
        code.text = "" + number; 
        Debug.Log(number);
    }  
}
