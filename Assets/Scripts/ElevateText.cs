using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 1);
        foreach (GameObject ONGameObject in GameManager.Instance.ON)
        {
            if (ONGameObject.GetComponent<Rigidbody2D>() != null)
            {
                ONGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            }
        }
        /*foreach (Transform child in transform)
        {
            if (t.GetComponent<Rigidbody2D>() != null)
            {
                t.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            }
        }*/

        StartCoroutine(StopCredit());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.eulerAngles);
        if(transform.eulerAngles.z >= 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 180) ;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<Rigidbody2D>() != null)
                {
                    child.GetComponent<Rigidbody2D>().gravityScale = 1;
                }
            }
            Debug.Log("yes");
        }
    }

    public void RotateIt()
    {
        GetComponent<Rigidbody2D>().angularVelocity = 100;
    }


    IEnumerator StopCredit()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (GameObject ONGameObject in GameManager.Instance.ON)
        {
            if(ONGameObject.GetComponent<Rigidbody2D>() != null)
            {
                ONGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
     /*   foreach (Transform t in transform)
        {
            if (t.GetComponent<Rigidbody2D>() != null)
            {
                t.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }*/
    }
}
