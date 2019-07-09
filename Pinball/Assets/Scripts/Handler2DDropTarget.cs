using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler2DDropTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public List<DropTarget2DScript> scripts = new List<DropTarget2DScript>();

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (areAllSingleDropTargetsDown())
        {
            foreach (DropTarget2DScript script in scripts)
            {
                Debug.Log("aqui");
                script.setRenderState(true);
            }
        }
    }

    bool areAllSingleDropTargetsDown()
    {
        foreach (DropTarget2DScript script in scripts)
        {
            if (script.getRenderState())
            {
                //Debug.Log("FALSE");
                return false;
            }
        }
        Debug.Log("TRUE");
        return true;
    }
}
