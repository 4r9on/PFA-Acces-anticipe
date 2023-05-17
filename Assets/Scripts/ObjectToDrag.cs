using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDrag : MonoBehaviour
{
    public bool destroyOnGravity;
    public bool canPutObject;
    public bool isPut;
    public int BornWithoutGravity;
    public GameObject objectToPutOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BecomeDestroyable()
    {
        yield return new WaitForSeconds(1f);
        destroyOnGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Permet de détruire certains objets quand on les laisse tomber
        if(collision.gameObject.tag == "Ground" && destroyOnGravity)
        {
            Destroy(gameObject);
        }
    }
}
