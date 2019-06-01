using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorScript : MonoBehaviour {
    public float collisionForce = 7;
    public float ScrollX = 0.5f;
    public float ScrollY = 0.5f;

    public Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Change the direction of texture scrolls
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);

    }

    public void OnCollisionEnter(Collision collision) {
        if (CollidedWithSphere(collision)) {
            PushBack(collision);
        }
    }

    public void PushBack(Collision collision) {
        // Get collision normal vector
        Vector3 newDirection = collision.contacts[0].normal;

        // Change direction
        // newDirection = -newDirection.normalized;

        // Disable any vertical movement
        newDirection.y = 0;

        // Apply impulse
        collision.rigidbody.AddRelativeForce(collisionForce * newDirection, ForceMode.Impulse);
    }

    public bool CollidedWithSphere(Collision collider) {
        if (collider.gameObject.tag == "Sphere") {
            return true;
        }
        else {
            return false;
        }
    }

}
