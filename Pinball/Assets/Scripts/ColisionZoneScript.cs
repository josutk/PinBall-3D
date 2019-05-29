using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionZoneScript : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject sphere;
    public Vector3 initPosition;

    void Start() {
        Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
    }

    void OnCollisionEnter(Collision collision) {
       if(!GameObject.FindGameObjectWithTag("Sphere")) {
            collision.rigidbody.velocity = Vector3.zero;
            collision.transform.position = initPosition;
        }
    }
}
