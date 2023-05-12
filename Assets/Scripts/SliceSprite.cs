using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSprite : MonoBehaviour
{
    public Texture2D tex;
    private Sprite mySprite;
    private SpriteRenderer sr;
    // Start is called before the first frame update

    private void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
    }
    void Start()
    {
        
        mySprite = Sprite.Create(tex, new Rect( 0, 0, 33, 30), new Vector2(0.5f, 0.5f), 100.0f);
        sr.sprite = mySprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
