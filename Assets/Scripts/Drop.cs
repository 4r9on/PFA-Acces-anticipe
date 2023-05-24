using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Camera cam;
    public GameObject scene;

    public float camSpeed;
    public float angleY;

    private Vector3 rotateValue;

    public void Dropeur()
    {
        rotateValue = new Vector3(0, 0, 180);
        transform.eulerAngles = transform.eulerAngles + rotateValue;
        transform.eulerAngles += rotateValue * camSpeed;
    }


}
