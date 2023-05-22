using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Si notre curseur touche l'UI alors on va divisé en 2 parties l'UI
        //Plus tard on changera le curseur par un objet que notre curseur va récupérer comme un marteau
        if(collision.gameObject.tag == "Hammer" && canBreakIt)
        {
            //Le but ici est de trouver à quel endroit est-ce qu'on a touché l'UI 
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            float pourcentOfSliceX = (collision.GetContact(0).point.x - thisSprite.bounds.min.x) / (thisSprite.bounds.max.x - thisSprite.bounds.min.x);

            //On va créer un morceau de notre UI en enlevant l'autre partie
            mySprite = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f/pourcentOfSliceX/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f), 100.0f);
            sr.sprite = mySprite;
            
            //On va faire suivre le collider pour éviter d'avoir un collider qui ne suit pas le morceau d'UI
            BoxCollider2D theCollider = GetComponent<BoxCollider2D>();
            float sizeXTotal = theCollider.size.x;
            theCollider.size = (mySprite.bounds.size);
            theCollider.offset = new Vector2((-sizeXTotal + theCollider.size.x)/2, 0);
          
            
            //On va créer notre seconde partie de l'UI
            GameObject newUI = Instantiate(NewUI);
            Sprite newSprite = newUI.GetComponent<SliceSprite>().mySprite;

            newSprite = Sprite.Create(tex, new Rect(0, 0, -(int)(tex.width - tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f / (1-pourcentOfSliceX), 0.5f), 100.0f);
            newUI.GetComponent<SliceSprite>().sr.sprite = newSprite;

            newUI.GetComponent<BoxCollider2D>().size = (newSprite.bounds.size);
            newUI.GetComponent<BoxCollider2D>().offset = new Vector2((sizeXTotal - newUI.GetComponent<BoxCollider2D>().size.x) / 2, 0);
            // GetComponent<Rigidbody2D>().gravityScale = 1.0f;

            tex = new Texture2D((int)mySprite.rect.width, (int)mySprite.rect.height);
            Color[] TexturePixels = mySprite.texture.GetPixels((int)mySprite.textureRect.x, (int)mySprite.textureRect.y, (int)mySprite.textureRect.width, (int)mySprite.textureRect.height);
            tex.SetPixels(TexturePixels);
            tex.filterMode = FilterMode.Point;
            tex.Apply();

            Texture2D newTex = newUI.GetComponent<SliceSprite>().tex;
            newTex = new Texture2D(-(int)newSprite.rect.width, (int)newSprite.rect.height);
            Color[] newTexturePixels = newSprite.texture.GetPixels((int)newSprite.textureRect.x, (int)newSprite.textureRect.y, -(int)newSprite.textureRect.width, (int)newSprite.textureRect.height);
            newTex.SetPixels(newTexturePixels);
            newTex.filterMode = FilterMode.Point;
            newTex.Apply();
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
        //Evite de casser en boucle l'UI
        yield return new WaitForSeconds(0.2f);
        canBreakIt = true;
    }
}
