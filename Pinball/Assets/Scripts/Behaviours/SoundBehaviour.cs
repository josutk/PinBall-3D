using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(CollisionHelper.DidCollideWithSphere(collision.gameObject.tag))
        {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
        }
    }
}
