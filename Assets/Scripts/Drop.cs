using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Camera cam;
    public GameObject scene;

    public float camSpeed;

    public Vector3 rotateValue;

    public Transform[] spawPoints;
    public List<GameObject> objectsDrop;
    public int dropeur;
    public GameObject test;

    private bool turn;

    private void Update()
    {

    }



    public void Return()
    {
        cam.transform.Rotate(0, 0, 180, 0);
        scene.transform.Rotate(0, 0, 180, 0);
        Debug.Log("Tourner");
        turn = true;
       
        if(turn)
        {
            Transform randomPoint = spawPoints[Random.Range(0, spawPoints.Length)];
            GameObject instantiated = Instantiate(test);
            instantiated.transform.position = randomPoint.position;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(test);
        Debug.Log("toucher");
    }



    /* public void Dropeur()
    {
        rotateValue = new Vector3(180, 0, 0);
        transform.eulerAngles = transform.eulerAngles + rotateValue;
        transform.eulerAngles += rotateValue * camSpeed;
        Debug.Log("retourner");
    }*/


}
