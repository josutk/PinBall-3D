using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget2DScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool renderState;
    void Start()
    {
        renderState = true;
        GetComponent<Renderer>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (renderState == true)
        {
            GetComponent<Renderer>().enabled = false;
            renderState = false;
            Physics2D.IgnoreCollision(collider.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }else
        {
            GetComponent<Renderer>().enabled = true;
            renderState = true;
        }
    }
}
