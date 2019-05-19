using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetScript : MonoBehaviour
{
        
    List<SingleDropTargetScript> scripts;
    
    // Start is called before the first frame update
    void Start()
    {
        scripts = new List<SingleDropTargetScript>();
        
        foreach(Transform child in transform)
        {
            scripts.Add(child.gameObject.GetComponent<SingleDropTargetScript>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AreAllSingleDropTargetsDown())
        {
            foreach(SingleDropTargetScript script in scripts)
            {
                script.startTime = Time.time;
                script.status = SingleDropTargetScript.Status.RISING;
            }
        }
        
    }

    bool AreAllSingleDropTargetsDown()
    {
        foreach(SingleDropTargetScript script in scripts)
        {
            switch(script.status)
            {
                case SingleDropTargetScript.Status.ABOVE_TABLE:
                    return false;
                case SingleDropTargetScript.Status.DROPPING: 
                    return false;
                case SingleDropTargetScript.Status.RISING:
                    return false;
                default:
                    break;
            }
        }

        return true;
    }
}
