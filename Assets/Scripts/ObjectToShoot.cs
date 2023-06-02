using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectToShoot : MonoBehaviour
{
    public GameObject leftHandle;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void mouv()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("aaaaaaa");
            //transform.position = leftHandle.transform.position;
        }
    }

}
