using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetScript : MonoBehaviour
{
    private float dropSpeed = 3;
    
    private float collisionForce = 7;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(IsBelowTheTable())
        {
            StopDrop();
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
            Drop();
        }
    }

    private void PushBack(Collision collision)
    {
        Debug.Log($"collision.contacts[0].point = {collision.contacts[0].point}");
        Debug.Log($"transform.position = {transform.position}");

        // Get collision normal vector
        Vector3 newDirection = collision.contacts[0].normal;
        Debug.Log($"newDirection = {newDirection}");

        // Change direction
        newDirection = -newDirection.normalized;

        // Disable any vertical movement
        newDirection.y = 0;

        // Apply impulse
        collision.rigidbody.AddRelativeForce(collisionForce * newDirection, ForceMode.Impulse);
    }

    private bool IsBelowTheTable()
    {
        if(transform.position.y < -0.25)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StopDrop()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, -0.25f, 0);
        
        Debug.Log($"Stand Up Position: {transform.position.y}");
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

    private void Drop()
    {
        // For some reason AddForce doesn't work here.
        rb.velocity = new Vector3(0, -dropSpeed, 0);
    }
}
