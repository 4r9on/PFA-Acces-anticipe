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



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.breakableUI.Remove(gameObject);
            if (GameManager.Instance.breakableUI.Count == 0 )
            {
                GameManager.Instance.GetComponent<LaunchBat>().ExitAppThenRestart();
            }
            Destroy(gameObject);
        }
        //Si notre curseur touche l'UI alors on va divise en 2 parties l'UI
        //Plus tard on changera le curseur par un objet que notre curseur va recuperer comme un marteau
        if (collision.gameObject.tag == "Hammer" && canBreakIt)
        {
            
            //Le but ici est de trouver a quel endroit est-ce qu'on a touche l'UI 
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            if (Mathf.Abs(collision.GetContact(0).normal.x) < Mathf.Abs(collision.GetContact(0).normal.y))
            {
               sliceX(collision, thisSprite);
            }
            else
            {
               sliceY(collision, thisSprite);
            }
          /*  // Debug.Log(collision.contacts.LongLength);
            float pourcentOfSliceX = (collision.GetContact(0).point.x - thisSprite.bounds.min.x) / (thisSprite.bounds.max.x - thisSprite.bounds.min.x);

            //On va creer un morceau de notre UI en enlevant l'autre partie
            mySprite = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f/pourcentOfSliceX/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*//*, 0.5f), 100.0f);
            sr.sprite = mySprite;
            
            //On va faire suivre le collider pour eviter d'avoir un collider qui ne suit pas le morceau d'UI
            BoxCollider2D theCollider = GetComponent<BoxCollider2D>();
            float sizeXTotal = theCollider.size.x;
            theCollider.size = (mySprite.bounds.size);
            theCollider.offset = new Vector2((-sizeXTotal + theCollider.size.x)/2, 0);


            //On va creer notre seconde partie de l'UI
            Debug.Log("another one");
            GameObject newUI = Instantiate(NewUI);
            newUI.transform.parent = transform.parent;
            newUI.transform.position = transform.position;
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
            newTex.Apply();*/
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

    void sliceX(Collision2D collision, SpriteRenderer thisSprite)
    {

        float pourcentOfSliceX = (collision.GetContact(0).point.x - thisSprite.bounds.min.x) / (thisSprite.bounds.max.x - thisSprite.bounds.min.x);

        //On va creer un morceau de notre UI en enlevant l'autre partie
        mySprite = Sprite.Create(tex, new Rect(0, 0, (int)(tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f / pourcentOfSliceX/*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f), 100.0f);
        sr.sprite = mySprite;

        //On va faire suivre le collider pour eviter d'avoir un collider qui ne suit pas le morceau d'UI
        BoxCollider2D theCollider = GetComponent<BoxCollider2D>();
        float sizeXTotal = theCollider.size.x;
        theCollider.size = (mySprite.bounds.size);
        theCollider.offset = new Vector2((-sizeXTotal + theCollider.size.x) / 2, 0);


        //On va creer notre seconde partie de l'UI
        GameObject newUI = Instantiate(NewUI);
        GameManager.Instance.breakableUI.Add(newUI);
        newUI.transform.parent = transform.parent;
        newUI.transform.position = transform.position;
        Sprite newSprite = newUI.GetComponent<SliceSprite>().mySprite;
        print(-(int)(tex.width - tex.width * pourcentOfSliceX));
        print(tex.width);
        newSprite = Sprite.Create(tex, new Rect(0, 0, -(int)(tex.width - tex.width * pourcentOfSliceX), tex.height), new Vector2(0.5f / (1 - pourcentOfSliceX), 0.5f), 100.0f);

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
        newUI.GetComponent<SliceSprite>().tex = new Texture2D(-(int)newSprite.rect.width, (int)newSprite.rect.height);
        Color[] newTexturePixels = newSprite.texture.GetPixels((int)newSprite.textureRect.x, (int)newSprite.textureRect.y, -(int)newSprite.textureRect.width, (int)newSprite.textureRect.height);
        newUI.GetComponent<SliceSprite>().tex.SetPixels(newTexturePixels);
        newUI.GetComponent<SliceSprite>().tex.filterMode = FilterMode.Point;
        newUI.GetComponent<SliceSprite>().tex.Apply();

        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        newUI.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        if (collision.GetContact(0).normal.y > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * 5;
            newUI.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * 5;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2) * 5;
            newUI.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2) * 5;
        }

    }
    void sliceY(Collision2D collision, SpriteRenderer thisSprite)
    {
        //On va creer un morceau de notre UI en enlevant l'autre partie
        float pourcentOfSlice = (collision.GetContact(0).point.y - thisSprite.bounds.min.y) / (thisSprite.bounds.max.y - thisSprite.bounds.min.y);
        mySprite = Sprite.Create(tex, new Rect(0, 0, tex.width, (int)(tex.height * pourcentOfSlice)), new Vector2(0.5f /*((thisSprite.bounds.max.x - thisSprite.bounds.min.x)/3*/, 0.5f / pourcentOfSlice), 100.0f);
        sr.sprite = mySprite;

        //On va faire suivre le collider pour eviter d'avoir un collider qui ne suit pas le morceau d'UI
        BoxCollider2D theCollider = GetComponent<BoxCollider2D>();
        float sizeTotal = theCollider.size.y;
        theCollider.size = (mySprite.bounds.size);
        theCollider.offset = new Vector2(0, (-sizeTotal + theCollider.size.y) / 2);


        //On va creer notre seconde partie de l'UI
        GameObject newUI = Instantiate(NewUI);
        newUI.transform.parent = transform.parent;
        newUI.transform.position = transform.position;
        Sprite newSprite = newUI.GetComponent<SliceSprite>().mySprite;

        newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, -(int)(tex.height - tex.height * pourcentOfSlice)), new Vector2(0.5f, 0.5f / (1 - pourcentOfSlice)), 100.0f);

        newUI.GetComponent<SliceSprite>().sr.sprite = newSprite;

        newUI.GetComponent<BoxCollider2D>().size = (newSprite.bounds.size);
        newUI.GetComponent<BoxCollider2D>().offset = new Vector2(0, (sizeTotal - newUI.GetComponent<BoxCollider2D>().size.y) / 2);  

        tex = new Texture2D((int)mySprite.rect.width, (int)mySprite.rect.height);
        Color[] TexturePixels = mySprite.texture.GetPixels((int)mySprite.textureRect.x, (int)mySprite.textureRect.y, (int)mySprite.textureRect.width, (int)mySprite.textureRect.height);
        tex.SetPixels(TexturePixels);
        tex.filterMode = FilterMode.Point;
        tex.Apply();

        Texture2D newTex = newUI.GetComponent<SliceSprite>().tex;
        newUI.GetComponent<SliceSprite>().tex = new Texture2D((int)newSprite.rect.width, -(int)newSprite.rect.height);
        Color[] newTexturePixels = newSprite.texture.GetPixels((int)newSprite.textureRect.x, (int)newSprite.textureRect.y, (int)newSprite.textureRect.width, -(int)newSprite.textureRect.height);
        newUI.GetComponent<SliceSprite>().tex.SetPixels(newTexturePixels);
        newUI.GetComponent<SliceSprite>().tex.filterMode = FilterMode.Point;
        newUI.GetComponent<SliceSprite>().tex.Apply();
        // newUI.GetComponent<SliceSprite>().tex = newTex;

        


        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        newUI.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        if (collision.GetContact(0).normal.x > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * 5;
            newUI.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2) * 5;
            Debug.Log("x.+");
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * 5;
            newUI.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2) * 5;
            Debug.Log("x.-");
        }
        




       
    }

    void ChangeForceAfterHit(GameObject TheOtherSide)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2) * 5;
        TheOtherSide.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2) * 5;
    }
}
