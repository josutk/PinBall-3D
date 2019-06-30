using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2DScript : MonoBehaviour
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
        collision.rigidbody.AddForce(-collision.contacts[0].normal *2, ForceMode2D.Impulse);
    }
}
