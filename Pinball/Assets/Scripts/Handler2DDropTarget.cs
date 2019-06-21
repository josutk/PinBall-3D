using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler2DDropTarget : MonoBehaviour
{
    // Start is called before the first frame update
    private List<DropTarget2DScript> scripts;

    void Start()
    {
        scripts = new List<DropTarget2DScript>();

        foreach (Transform child in transform)
        {
            scripts.Add(child.gameObject.GetComponent<DropTarget2DScript>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AreAllSingleDropTargetsDown())
        {
            foreach (DropTarget2DScript script in scripts)
            {
                //script.renderStatus = true;
            }
        }
    }

    bool AreAllSingleDropTargetsDown()
    {
        foreach (DropTarget2DScript script in scripts)
        {
           // if (!script.renderStatus)
           // {
             //   return false;
           // }
        }
        return true;
    }

}
