using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    private Rigidbody rb;

    public int turns
    { get; set; }

    // Component score
    private const int SCORE = 100;

    void Start()
    {
        Debug.Log("Hello, World!");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(rb.angularVelocity != Vector3.zero)
        {
            Debug.Log("Moving!");

            if(transform.rotation.x > 180 || transform.rotation.x < 1)
            {
                Debug.Log("Rotated!");
            }
        }
    }
}
