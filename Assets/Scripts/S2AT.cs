using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2AT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveGauge()
    {
        GameManager.Instance.RemoveGauge();
    }

    public void S2ATQuit()
    {
        GameManager.Instance.S2ATQuit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.parent.GetComponent<ElevateText>().StopCredit();
        }
    }
}
