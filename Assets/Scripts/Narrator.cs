using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedOnCD()
    { 
        GameManager.Instance.CD.GetComponent<Animator>().SetBool("IsClicked", true);
    }

    public void UnClickedOnCD()
    {
        GameManager.Instance.CD.GetComponent<Animator>().SetBool("IsClicked", false);
    }

    public void RemoveDiskPlayer()
    {
        GameManager.Instance.DiskPlayer.SetActive(false);
        GameManager.Instance.CD.SetActive(false);
       // GameManager.Instance.CD.GetComponent<Rigidbody2D>().velocity = new Vector2 (10, 10);
       // GameManager.Instance.CD.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void DestroyJukebox()
    {
        GameManager.Instance.DestroyJukebox();
    }


    public void EndOfScene2()
    {
        for (int i = 0; i < GameManager.Instance.narratorsAnim.Count - 1; i++)
        {
            if (GameManager.Instance.narratorsAnim[i] == gameObject)
            {
                GameManager.Instance.narratorsAnim[i + 1].SetActive(true);
                DestroyHands();
            }
        }

    }

    public void DestroyHands()
    {
        gameObject.SetActive(false);
    }
}
