using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateText : MonoBehaviour
{
    public GameObject sprite;
    public Transform[] spawnPoints;

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;        
    public GameObject object4;        
    public GameObject object5;        
    public GameObject object6;  
    public GameObject object7;
    public GameObject object8; 
    public GameObject object9;   
    public GameObject object10;   
    public GameObject object11;   
    public GameObject object12;   
    public GameObject object13;
    public GameObject object14;

    private int nObject;

    public float delay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

                DropItem();
            }
        }
    }

    public void RotateIt()
    {
        foreach(Transform child in gameObject.transform)
        {
            child.GetComponent<BoxCollider2D>().enabled = true;
        }
       
        GameManager.Instance.NewSound(gameObject, gameObject.GetComponent<SoundDesign>().TheVolume);
        gameObject.transform.parent.GetComponent<Animator>().enabled = true;
        //GetComponent<Rigidbody2D>().angularVelocity = 100;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (GameObject ONGameObject in GameManager.Instance.ON)
        {
            if (ONGameObject.GetComponent<Rigidbody2D>() != null)
            {

                ONGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void elevateAllText()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        foreach (GameObject ONGameObject in GameManager.Instance.ON)
        {
            if (ONGameObject.GetComponent<Rigidbody2D>() != null)
            {
                ONGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            }
        }
    }


    public void StopCredit()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("westop");
        foreach (GameObject ONGameObject in GameManager.Instance.ON)
        {
            Debug.Log("maybeitstop");
            if (ONGameObject.GetComponent<Rigidbody2D>() != null)
            {
                Debug.Log("stop");
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

    public void DropItem()
    {
        /*Debug.Log("tombe");
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject instantiated = Instantiate(sprite);
        instantiated.transform.position = randomPoint.position;

        if(nObject == 0)
        {
            object1.SetActive(true);
            object2.SetActive(true);
            object3.SetActive(true);
            object4.SetActive(true);
            object5.SetActive(true);
            nObject++;
        }
        if (nObject == 1)
        {
            object6.SetActive(true);
            object7.SetActive(true);
            object8.SetActive(true);
            object9.SetActive(true);
            object10.SetActive(true);
            nObject++;
        }
        if (nObject == 2)
        {
            object11.SetActive(true);
            object12.SetActive(true);
            object13.SetActive(true);
            object14.SetActive(true);
        }



            /* int randomNumber = Random.Range(1, 101);
             List<Loot> possibleItems = new List<Loot>();
             foreach(Loot item in lootList)
             {
                 if(randomNumber <= item.dropChance)
                 {
                     possibleItems.Add(item);
                 }
             }
             if(possibleItems.Count > 0)
             {
                 Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
                 return droppedItem;
             }
             return null;*/

        }

    /*public void Instantiate(Vector3 spawnPosition)
    {
        Loot droppedItem = DropItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropForce = 300f;
            Vector2 dropdirection = new Vector2(Random.Range(-1f, -1f), Random.Range(-1f, -1f));
            lootGameObject.GetComponent<Rigidbody>().AddForce(dropdirection * dropForce, ForceMode2D.Impulse);
        }
    }*/
}
