using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampe : MonoBehaviour
{
    public GameObject lampe;
    public GameObject button;

    private void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("b");
    }





    /*public void OnClic()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.ObjectHover.tag == "Light")
        {

        }
    }*/
}
