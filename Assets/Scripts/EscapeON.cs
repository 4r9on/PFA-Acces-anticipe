using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscapeON : MonoBehaviour
{
    public List<Transform> spawnPoint;

    public GameObject pointOn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Count)];
            GameObject instantiated = Instantiate(pointOn);
            instantiated.transform.position = randomPoint.position;
            Destroy(this.gameObject);
        }
    }


    public void FledOn()
    {
        
    }




}
