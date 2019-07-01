using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticCollision2D : MonoBehaviour
{
    public int pushForce = 0;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(CollisionHelper.DidCollideWith2DSpere(collision.gameObject.tag))
        {
            Debug.Log("Yes!");
            collision.rigidbody.AddForce(-collision.contacts[0].normal * pushForce, ForceMode2D.Impulse);
        }
    }
}
