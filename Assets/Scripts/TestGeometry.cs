// Obtain the vertices from the script and modify the position
// of one of them. Use OverrideGeometry() for this.
using UnityEngine;

public class TestGeometry : MonoBehaviour
{
    public Texture2D tex;
    private Sprite newSprite;
    private SpriteRenderer spriteR;
    private Rect buttonPos1;
    private Rect buttonPos2;

    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        // Create a blank Texture and Sprite to override later on.
        Texture2D text = new Texture2D(tex.width, tex.height);
        spriteR.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero, 100);
        newSprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), Vector2.zero, 100);

        buttonPos1 = new Rect(10.0f, 10.0f, 200.0f, 30.0f);
        buttonPos2 = new Rect(10.0f, 50.0f, 200.0f, 30.0f);
    }

    void OnGUI()
    {
        if (GUI.Button(buttonPos1, "Draw Debug"))
            DrawDebug();

        if (GUI.Button(buttonPos2, "Perform OverrideGeometry"))
            ChangeSprite();
    }

    // Show the sprite triangles
    void DrawDebug()
    {
        Sprite sprite = spriteR.sprite;

        ushort[] t = sprite.triangles;
        Vector2[] v = sprite.vertices;
        int a, b, c;

        // draw the triangles using grabbed vertices
        for (int i = 0; i < t.Length; i = i + 3)
        {
            a = t[i];
            b = t[i + 1];
            c = t[i + 2];
            Debug.DrawLine(v[a], v[b], Color.white, 100.0f);
            Debug.DrawLine(v[b], v[c], Color.white, 100.0f);
            Debug.DrawLine(v[c], v[a], Color.white, 100.0f);
        }
    }

    // Edit the vertices obtained from the sprite.  Use OverrideGeometry to
    // submit the changes.
    void ChangeSprite()
    {
        Sprite o = spriteR.sprite;
        Vector2[] sv = o.vertices;
        
        for (int i = 0; i < sv.Length; i++)
        {
        
            sv[i].x = Mathf.Clamp((o.vertices[i].x - o.bounds.center.x - (o.textureRectOffset.x / o.texture.width) + o.bounds.extents.x) / (2.0f * o.bounds.extents.x) * o.rect.width, 0.0f, o.rect.width);

            sv[i].y = Mathf.Clamp((o.vertices[i].y - o.bounds.center.y - (o.textureRectOffset.y / o.texture.height) + o.bounds.extents.y) / (2.0f * o.bounds.extents.y) * o.rect.height, 0.0f, o.rect.height);
            Debug.Log(o.textureRectOffset.y);
            // make a small change to the 3rd vertex
            /* if (i == 2)
                sv[i].x = sv[i].x - 50;*/

            if (sv[i].x < 0)
            {
               // sv[i].x = sv[i].x * -1;
                //sv[i].x = 0;
            }
            if (sv[i].y < 0)
            {
                //sv[i].y = sv[i].y * -1;
              // sv[i].y =0;
            }
           // Debug.Log(sv[i].x);
        }
    //    Debug.Log("test");
        spriteR.sprite.OverrideGeometry(sv, o.triangles);
    }
}