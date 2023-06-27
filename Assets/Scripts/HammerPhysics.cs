using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HammerPhysics : MonoBehaviour
{
    public GameObject direction;
    float lastPosition;
    bool HasntMove;
    bool StopMove;
    bool toTheLeft;
    float PourcentMovement;
    int ID;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.parent.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(PourcentMovement > 0)
        {
            PourcentMovement -= Time.deltaTime;
            if (PourcentMovement < 0)
            {
                PourcentMovement = 0;
            }
        }
        if (HasntMove)
        {
            if(toTheLeft)
            {
                transform.RotateAround(transform.parent.position, Vector3.back, 500 * Time.deltaTime);
            }
            else
            {
                transform.RotateAround(transform.parent.position, Vector3.forward, 500 * Time.deltaTime);
            }
           
        }
        else if(!StopMove) 
        {
            if (transform.eulerAngles.z < 10 || transform.eulerAngles.z > 350)
            {
                transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0);
                StopMove = true;
            }
            else
            {
                if (transform.eulerAngles.z > 130)
                {
                    transform.RotateAround(transform.parent.position, Vector3.forward, 200 * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(transform.parent.position, Vector3.back, 200 * Time.deltaTime);
                }
            }
            
        }
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = transform.parent.position;
        transform.GetChild(0).position = transform.parent.position;
        transform.GetChild(0).eulerAngles = Vector3.zero;
        //GetComponent<Rigidbody2D>().angularVelocity = 0;
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        if (lastPosition != transform.parent.position.x && (lastPosition > transform.parent.position.x+0.05f || lastPosition < transform.parent.position.x-0.05f))
        {
            PourcentMovement = 1;
            StopMove = false;
            if (lastPosition < transform.parent.position.x)
            {
                toTheLeft = false;
                transform.RotateAround(transform.parent.position, Vector3.forward, 200 * Time.deltaTime);
                StartCoroutine(movementToSides());
            }
            else 
            {
                toTheLeft = true;
                transform.RotateAround(transform.parent.position, Vector3.back, 200 * Time.deltaTime);
                StartCoroutine(movementToSides());
            }
                lastPosition = transform.parent.position.x;

        }
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
    IEnumerator movementToSides()
    {
        ID++;
        int ActualId = ID;
        HasntMove = true;
        yield return new WaitForSeconds(1f);
        if(ID == ActualId)
        {

            HasntMove = false;
        }
        //make a move to a side
    }
}
