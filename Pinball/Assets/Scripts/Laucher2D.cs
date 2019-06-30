using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laucher2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

            collision.rigidbody.AddForce(-collision.contacts[0].normal * 100, ForceMode2D.Impulse);
      
    }
}
