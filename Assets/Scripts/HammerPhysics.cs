using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerPhysics : MonoBehaviour
{
    public GameObject direction;
    float lastPosition;
    bool HasntMove;
    float MaxMovement;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.back, Time.deltaTime);
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = transform.parent.position;
        //GetComponent<Rigidbody2D>().angularVelocity = 0;
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        /*
        if (lastPosition != transform.parent.position.x && (lastPosition > transform.parent.position.x+0.05f || lastPosition < transform.parent.position.x-0.05f))
        {
            Debug.Log("pas same");
            Debug.Log(lastPosition - transform.parent.position.x + 0.05f);
            Debug.Log(lastPosition + transform.parent.position.x - 0.05f);
            if (lastPosition < transform.parent.position.x)
            {
                transform.parent.GetComponent<Rigidbody2D>().angularVelocity = 300;
            }
            else 
            {
                transform.parent.GetComponent<Rigidbody2D>().angularVelocity = -300;
            }
                lastPosition = transform.parent.position.x;

        }*/
        /*   transform.up = direction.transform.position;
           transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);*/
    }

    IEnumerator movementToGround()
    {
        HasntMove = true;
        yield return new WaitForSeconds(0.1f);
        HasntMove = false;
        //make a move to the ground
    }
}
