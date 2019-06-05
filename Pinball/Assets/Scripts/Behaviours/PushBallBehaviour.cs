using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallBehaviour: MonoBehaviour
{
    public float minIncrement;
    public float maxIncrement;

    void OnCollisionEnter(Collision collision) {
        if (DidCollideWithSphere(collision)) PushBack(collision);
    }

    private void PushBack(Collision collision) {
        // Get collision normal vector
        Vector3 newDirection = collision.contacts[0].normal;
        float collisionForce = collision.relativeVelocity.magnitude;
        Debug.Log($"CollisionForce ${collisionForce}");

        // Change direction
        newDirection = -newDirection.normalized;

        // Disable any vertical movement
        newDirection.y = 0;

        float forceToBeAdded = collisionForce * minIncrement;

        Debug.Log($"Force to be Added {forceToBeAdded}");

        if(forceToBeAdded > maxIncrement)
        {
            forceToBeAdded = maxIncrement;
        }
        
        if(forceToBeAdded < minIncrement)
        {
            forceToBeAdded = minIncrement;
        }

        // Apply impulse
        collision.rigidbody.AddRelativeForce(forceToBeAdded * newDirection, ForceMode.VelocityChange);
    }

    private bool DidCollideWithSphere(Collision collision) {
        if(collision.gameObject.tag == Constants.SPHERE_TAG) return true;
        else return false;
    }
}
