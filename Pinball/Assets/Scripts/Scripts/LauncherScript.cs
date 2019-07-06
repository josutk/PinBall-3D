using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherScript: MonoBehaviour
{

    public float launchThreshold = 5f;
    GameObject sphere;

    private SignalHandlerScript signalHandler;

    private AudioSource audioSource;

    private void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(CollisionHelper.DidCollideWithSphere(other.tag))
        {
            sphere = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.tag))
        {
            if (signalHandler.launcher.force > 0)
            {
                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                float force = signalHandler.launcher.force * launchThreshold;
                rb.AddForce(force * Vector3.forward);
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(CollisionHelper.DidCollideWith2DSpere(other.tag))
        {
            sphere = null;
        }
    }
}
    
