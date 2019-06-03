using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorBehaviour : MonoBehaviour
{
    public Color startColor;
 
    public Color changeColor;

    // if true, will try to change the color of the parent object. If false, will try to
    // change the color of the current object. Defaults to false.
    public bool useParent = false;

    void OnCollisionEnter(Collision collision)
    {
        if(CollisionHelper.DidCollideWithSphere(collision)) ChangeColor();
    }

    void OnCollisionExit(Collision collision)
    {
        if(CollisionHelper.DidCollideWithSphere(collision)) UnchangeColor();
    }

    private void ChangeColor()
    {
        if(useParent == false)
        {
            GetComponent<Renderer>().material.color = changeColor; 

            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach(Renderer renderer in renderers)
            {
                renderer.material.color = changeColor;
            }
        } 
        else
        {
            GameObject parent = transform.parent.gameObject;
            parent.GetComponent<Renderer>().material.color = changeColor;

            Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();

            foreach(Renderer renderer in renderers)
            {
                renderer.material.color = changeColor;
            }
        }
    }

    private void UnchangeColor()
    {
        if(useParent == false)
        {
            GetComponent<Renderer>().material.color = startColor; 

            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach(Renderer renderer in renderers)
            {
                renderer.material.color = startColor;
            }
        } 
        else
        {
            GameObject parent = transform.parent.gameObject;
            parent.GetComponent<Renderer>().material.color = startColor;

            Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();

            foreach(Renderer renderer in renderers)
            {
                renderer.material.color = startColor;
            }
        }
    }
}
