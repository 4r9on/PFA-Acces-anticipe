using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPhysics : MonoBehaviour
{
    public GameObject direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       GetComponent<Rigidbody2D>().rotation =  100;
     /*   transform.up = direction.transform.position;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);*/
    }
}
