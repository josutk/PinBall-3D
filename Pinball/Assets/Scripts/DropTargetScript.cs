using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetScript : MonoBehaviour
{
        
    private List<SingleDropTargetScript> scripts;
    
    public AudioClip musicClip;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        scripts = new List<SingleDropTargetScript>();
        
        foreach(Transform child in transform)
        {
            scripts.Add(child.gameObject.GetComponent<SingleDropTargetScript>());
        }

        musicSource.clip = musicClip;
    }  

    // Update is called once per frame
    void Update()
    {
        if(AreAllSingleDropTargetsDown())
        {
            foreach(SingleDropTargetScript script in scripts)
            {
                SetupMovementUp(script);
            }

            musicSource.Play();
        }
    }

    private void SetupMovementUp(SingleDropTargetScript script)
    {
        script.startTime = Time.time;
        script.journeyLength = Vector3.Distance(script.belowTablePosition, script.originalPosition);
        script.status = SingleDropTargetScript.Status.RISING;
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
