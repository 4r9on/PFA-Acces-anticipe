using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public Texture2D tex;
    private SpriteRenderer mr;
    private Sprite mySprite;

    private void Awake()
    {
        mr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        mySprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //mr.sprite.rect.width;
    }

    void Update()
    {
        
    }

    public void MouvGauge()
    {



    }
}
