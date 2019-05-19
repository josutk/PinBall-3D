using System.Collections;
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
    public float collisionForce = 3.0F;

    public Vector3 originalPosition;
    public Vector3 belowTablePosition;

    public Status status = Status.ABOVE_TABLE;
    public float journeyLength;
    public float startTime;

    public AudioClip musicClip;

    public AudioSource musicSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        belowTablePosition = new Vector3(transform.position.x, -0.26f, transform.position.z);
        
        musicSource.clip = musicClip;
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

        if(CollidedWithSphere(collision))
        {
            PushBack(collision);
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

    private void PushBack(Collision collision)
    {
        //Debug.Log($"collision.contacts[0].point = {collision.contacts[0].point}");
        //Debug.Log($"transform.position = {transform.position}");

        // Get collision normal vector
        Vector3 newDirection = collision.contacts[0].normal;
        //Debug.Log($"newDirection = {newDirection}");

        // Change direction
        newDirection = -newDirection.normalized;

        // Disable any vertical movement
        newDirection.y = 0;

        // Apply impulse
        collision.rigidbody.AddRelativeForce(collisionForce * newDirection, ForceMode.Impulse);
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
        Debug.Log($"status == {status.ToString()}");

        float distCovered = (Time.time - startTime) * dropSpeed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(start, end, fracJourney);

        Debug.Log($"fracJourney == {fracJourney}");

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