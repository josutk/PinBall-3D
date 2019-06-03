using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticCollision : MonoBehaviour
{
    //private ElasticCollison elastic = new ElasticCollison();
    public int increaseVelocity = 15;
    public Color startColor, changeColor;

    // Start is called before the first frame update
    void Start(){        
    }

    // Update is called once per frame
    void Update(){        
    }

    void OnCollisionEnter(Collision collision) {

        if (sphereColision(collision)) {            
            gameObject.GetComponent<Renderer>().material.color = changeColor;
            Vector3 vector = collision.rigidbody.velocity;
            vector.x = (collision.transform.position.x - transform.position.x) * increaseVelocity;
            vector.y = (collision.transform.position.y - transform.position.y) * increaseVelocity;
            vector.z = (collision.transform.position.z - transform.position.z) * increaseVelocity;
            collision.rigidbody.velocity = vector;

        
        }
    }

    void OnCollisionExit(Collision other){        
        if (sphereColision(other)) {
            gameObject.GetComponent<Renderer>().material.color = startColor;
        }
    }

    public bool sphereColision( Collision collision) {

    if (collision.gameObject.tag == "Sphere"){
        return true;
    }
    return false;
    }
}
