using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvisibleObjectScript : MonoBehaviour {
    public string destroytag = "Sphere";
    //void OnBecameInvisible() {
    //    Destroy(gameObject);
    //}

    void OnTriggerEnter(Collider other) {
        if(other.tag == destroytag) {
            Destroy(other.gameObject);
        }        
    }

    //void OnCollisionEnter(Collision collision) {
    //    Destroy(collision.gameObject);        
    //}    

}
