using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFlipper2D : MonoBehaviour
{

    public float speed = 0f;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;
    public bool flipper;

    void Start()
    {
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((flipper == true && Input.GetKey(KeyCode.LeftArrow)))
        {
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
        }
        else
        {
            motor2D.motorSpeed = -speed;
            myHingeJoint.motor = motor2D;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            collision.rigidbody.AddForce(-collision.contacts[0].normal * 40, ForceMode2D.Impulse);
        }
    }
}
