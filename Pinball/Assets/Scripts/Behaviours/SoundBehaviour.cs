using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour
{
    // Audio source that will play with collision with the ball.
    // If no audio source is passed, it tries to get one from the
    // Current object.
    public AudioSource audioSource;

    void Start()
    {
        if(audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(CollisionHelper.DidCollideWithSphere(collision.gameObject.tag))
        {
            audioSource.Play();
        }
    }
}
