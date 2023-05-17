using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliceSprite : MonoBehaviour
{
    public Texture2D tex;
    private Sprite mySprite;
    private SpriteRenderer sr;
    public GameObject NewUI;
    private bool canBreakIt;
    // Start is called before the first frame update

    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
     /*   mySprite = Sprite.Create(tex, new Rect( 0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        sr.sprite = mySprite;*/
        StartCoroutine(WaitToBreakIt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // other.OverlapPoint();
        List<Collider2D> list = new List<Collider2D>();
        Debug.Log(other.GetContacts(list));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cursor" && canBreakIt)
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            float pourcentOfSliceX = (collision.GetContact(0).point.x - thisSprite.bounds.min.x) / (thisSprite.bounds.max.x - thisSprite.bounds.min.x);
            Debug.Log(GetComponent<BoxCollider2D>().bounds.size);
            mySprite = Sprite.Create(tex, new Rect(0, 0, tex.width * pourcentOfSliceX, tex.height), new Vector2(0.5f/pourcentOfSliceX/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f), 100.0f);
            sr.sprite = mySprite;
            GetComponent<BoxCollider2D>().size = (mySprite.bounds.size);
           // GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f * pourcentOfSliceX/2, 0);
            GameObject newUI = Instantiate(NewUI);
            newUI.GetComponent<SliceSprite>().mySprite = Sprite.Create(tex, new Rect(0, 0, - (tex.width - tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f / (1-pourcentOfSliceX), 0.5f), 100.0f);
            newUI.GetComponent<SliceSprite>().sr.sprite = newUI.GetComponent<SliceSprite>().mySprite;
        }


        // Debug.Log(transformA.position);
        // Debug.Log(transformA.position); 
        // Vector3 b = transform.lossyScale.m;
        // Debug.Log(transform.position.x);
        // Debug.Log(extremPointX);
        //  Vector2 a = GetComponent<SpriteRenderer>().size;

        // Debug.Log(GetComponent<SpriteRenderer>().bounds.size);
        //   Debug.Log(collision.GetContact(0).normal.x);
        /*  mySprite = Sprite.Create(tex, new Rect(0, 0, 64* collision.GetContact(0).normal.x, 64), new Vector2(0.5f, 0.5f), 100.0f);
          sr.sprite = mySprite;*/
        // if (collision.GetContact(0).normal.x == 0) { }
    }

    IEnumerator WaitToBreakIt()
    {
        yield return new WaitForSeconds(0.2f);
        canBreakIt = true;
    }
}
