using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderAnim : MonoBehaviour
{
    // Start is called before the first frame update

    public void StopTheAnimator()
    {
       
        GetComponent<Animator>().enabled = false;
        transform.position = new Vector2 (transform.position.x, -1.83f);
    }
}
