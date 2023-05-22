using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    public DragAndDrop dragAndDrop;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked ;   
       // Cursor.visible = false;
       // StartCoroutine(waitStop());
    }

    // Update is called once per frame
    public IEnumerator waitStop()
    {
        yield return new WaitForSeconds(1f);
        dragAndDrop.CanMoveCursor = false;
    }
}
