using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPhysics : MonoBehaviour
{
    public GameObject direction;
    float lastPosition;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().angularVelocity = 0;
      /*  GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (lastPosition != transform.parent.position.x && (lastPosition > transform.parent.position.x+0.05f || lastPosition < transform.parent.position.x-0.05f))
        {
            Debug.Log("pas same");
            Debug.Log(lastPosition - transform.parent.position.x + 0.05f);
            Debug.Log(lastPosition + transform.parent.position.x - 0.05f);
            if (lastPosition < transform.parent.position.x)
            {
                transform.GetComponent<Rigidbody2D>().angularVelocity = 300;
            }
            else 
            {
                transform.GetComponent<Rigidbody2D>().angularVelocity = -300;
            }
                lastPosition = transform.parent.position.x;

        }*/
        /*   transform.up = direction.transform.position;
           transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);*/
    }
}
