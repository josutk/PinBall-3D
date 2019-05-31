﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticCollison : MonoBehaviour
{
    // Start is called before the first frame update
    private int increaseVelocity = 15;

    void Start(){}

    // Update is called once per frame
    void Update(){}

    void OnCollisionEnter(Collision collision)
    {
        if (sphereColision(collision)) {
            Vector3 vector = collision.rigidbody.velocity;
            vector.x = (collision.transform.position.x - transform.position.x) * increaseVelocity;
            vector.y = (collision.transform.position.y - transform.position.y) * increaseVelocity;
            vector.z = (collision.transform.position.z - transform.position.z) * increaseVelocity;
            collision.rigidbody.velocity = vector;
        }
    }

    public bool sphereColision( Collision collision) {

        if (collision.gameObject.name == "Sphere")
        {
            return true;
        }
        return false;
    } 
}