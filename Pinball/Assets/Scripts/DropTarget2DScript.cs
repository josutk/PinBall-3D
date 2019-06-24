using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget2DScript : MonoBehaviour
{
    // Start is called before the first frame update

    private bool renderState;
    void Start()
    {
        renderState = true;
        GetComponent<Renderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (renderState)
        {
            GetComponent<Renderer>().enabled = true;
        }else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (renderState == true)
        {
            renderState = false;
        }else
        {

        }
    }

    public void setRenderState(bool state) {
        renderState = state;
    }

    public bool getRenderState() {
        return renderState;
    }
}
