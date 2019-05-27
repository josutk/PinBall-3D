using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopScript : MonoBehaviour
{
    private GameObject sphere;
    private GameObject[] table;

    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG);
        table = GameObject.FindGameObjectsWithTag(Constants.TABLE_TAG);
    }

    void OnTriggerEnter(Collider other)
    {
        if(DidCollideWithSphere(other))
        {
            foreach(GameObject t in table)
            {
                Physics.IgnoreCollision(other, t.GetComponent<Collider>());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(DidCollideWithSphere(other))
        {
            foreach(GameObject t in table)
            {
                Physics.IgnoreCollision(other, t.GetComponent<Collider>(), false);
            }
        }
    }

    bool DidCollideWithSphere(Collider other)
    {
        if(other.gameObject.tag == Constants.SPHERE_TAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
