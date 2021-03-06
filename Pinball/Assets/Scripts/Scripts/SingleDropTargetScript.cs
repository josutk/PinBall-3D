﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDropTargetScript : MonoBehaviour
{
    public enum Status
    {
        ABOVE_TABLE,
        BELOW_TABLE,
        DROPPING,
        RISING,
    };

    private Rigidbody rb;

    public float dropSpeed = 3.0F;

    public Vector3 originalPosition;
    public Vector3 belowTablePosition;

    // Y position where the component will be when it drops.
    public const float DROPPED_Y_POSITION = -0.22F;

    public Status status = Status.ABOVE_TABLE;
    public float journeyLength;
    public float startTime;

    private AudioSource musicSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        belowTablePosition = new Vector3(transform.position.x, DROPPED_Y_POSITION, transform.position.z);
        
        musicSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(status == Status.DROPPING)
        {
            Move(originalPosition, belowTablePosition);
        }
        else if(status == Status.RISING)
        {
            Move(belowTablePosition, originalPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CollidedWithTable(collision))
        {
            IgnoreTable(collision);
        }

        if(CollidedWithSphere(collision) && status != Status.BELOW_TABLE)
        {
            SetupMovementDown();
            musicSource.Play();
        }
    }

    private void SetupMovementDown()
    {
        startTime = Time.time;   
        journeyLength = Vector3.Distance(originalPosition, belowTablePosition);
        status = Status.DROPPING;
    }

    private bool CollidedWithTable(Collision collider)
    {
        if(collider.gameObject.tag == "Table")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void IgnoreTable(Collision collision)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
    }

    private bool CollidedWithSphere(Collision collider)
    {
        if(collider.gameObject.tag == "Sphere")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Move(Vector3 start, Vector3 end)
    {

        float distCovered = (Time.time - startTime) * dropSpeed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(start, end, fracJourney);


        if(fracJourney >= 1) // Ended
        {
            if(transform.position == belowTablePosition)
            {
                status = Status.BELOW_TABLE;
            }
            else if(transform.position == originalPosition)
            {
                status = Status.ABOVE_TABLE;
            } 
        }
    }
}