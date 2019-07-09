using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameScript gameScript;

    private int rotationCount = 0;
    private float rotationAmount = -180;
    private Quaternion lastFrameAngle;

    // Component score
    public const int SCORE = 100;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
        //GameObject game = GameObject.FindGameObjectWithTag("Game");

        //gameScript = game.GetComponent<GameScript>();

        //rb.angularVelocity = new Vector3(0.4f, 0, 0);
    }

    void Update()
    {

        //Debug.Log($"AnglesE = {Quaternion.Angle(transform.rotation, lastFrameAngle)}");
        
        if(IsMoving)
        {
            // Get how much was rotated from last frame and add it
            rotationAmount += Quaternion.Angle(transform.rotation, lastFrameAngle);

            // If we finally rotated 360 degrees, this should be 1
            rotationCount = (int) rotationAmount / 360;

           // Debug.Log($"Rotation Count: {rotationCount}");
           // Debug.Log($"Rotation Amount: {rotationAmount}");

            // If it is, change it back to zero and add the score.
            if(rotationCount == 1)
            {
                rotationCount = 0;
                rotationAmount = 0;

                audioSource.Play();
            }

            lastFrameAngle = transform.rotation;
        }
    }

    bool IsMoving
    {
        get
        {
            if (rb.angularVelocity != Vector3.zero) return true;
            else return false;
        }
    }
}
