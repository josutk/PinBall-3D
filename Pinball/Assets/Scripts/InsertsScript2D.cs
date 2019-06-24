using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertsScript2D : MonoBehaviour
{
    ParticleSystem particles;
    bool flag;
 
    // Start is called before the first frame update
    void Start()
    {
       
        particles = GetComponent<ParticleSystem>();
        particles.Stop(true);   
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    
        particles.Play(true);
        collision.rigidbody.AddForce(-collision.contacts[0].normal * 35, ForceMode2D.Impulse);
    }
}
