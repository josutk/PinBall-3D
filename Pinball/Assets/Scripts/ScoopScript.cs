using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopScript : MonoBehaviour
{
    private GameObject sphere;
    private GameObject[] table;
    private GameObject ball2D;

    // Constant related to the impulse given to the 2D ball.
    // This is used because the velocities doesn't translate well two 2D environments.
    public const float IMPULSE = 9;

    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG);
        table = GameObject.FindGameObjectsWithTag(Constants.TABLE_TAG);
        ball2D = GameObject.FindGameObjectWithTag(Constants.BALL_2D_TAG);
    }

    void OnTriggerEnter(Collider other)
    {
        if(DidCollideWithSphere(other))
        {
            foreach(GameObject t in table)
            {
                Physics.IgnoreCollision(other, t.GetComponent<Collider>());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(DidCollideWithSphere(other))
        {
            foreach(GameObject t in table)
            {
                Physics.IgnoreCollision(other, t.GetComponent<Collider>(), false);
            }

            ShowBall2D();
        }
    }

    private void ShowBall2D()
    {
        Rigidbody2D rb2D = ball2D.GetComponent<Rigidbody2D>(); 
        Rigidbody rb = sphere.GetComponent<Rigidbody>();

        rb2D.constraints = RigidbodyConstraints2D.None;

        rb2D.AddForce(new Vector2(0,  (-rb.velocity.z) * IMPULSE));   
    }

    private bool DidCollideWithSphere(Collider other)
    {
        if(other.gameObject.tag == Constants.SPHERE_TAG)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
