using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallBehaviour: MonoBehaviour
{
    public float velocityIncrement;

    void OnCollisionEnter(Collision collision) {
        if (DidCollideWithSphere(collision)) PushBack(collision);
    }

    private void PushBack(Collision collision) {
        // Get collision normal vector
        Vector3 newDirection = collision.contacts[0].normal;

        // Change direction
        newDirection = -newDirection.normalized;

        // Disable any vertical movement
        newDirection.y = 0;

        // Apply impulse
        collision.rigidbody.AddRelativeForce(velocityIncrement * newDirection, ForceMode.Impulse);
    }

    private bool DidCollideWithSphere(Collision collision) {
        if(collision.gameObject.tag == Constants.SPHERE_TAG) return true;
        else return false;
    }
}
