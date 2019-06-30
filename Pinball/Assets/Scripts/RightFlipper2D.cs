using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFlipper2D : MonoBehaviour
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
        if ((flipper == false && Input.GetKey(KeyCode.RightArrow)))
        {

            //GetComponent<HingeJoint2D>().useMotor = true;
            motor2D.motorSpeed = -speed;
            myHingeJoint.motor = motor2D;

        }
        else
        {
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
            // GetComponent<HingeJoint2D>().useMotor = false;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(Input.GetKey(KeyCode.RightArrow)){
            collision.rigidbody.AddForce(-collision.contacts[0].normal * 40, ForceMode2D.Impulse);
        }
    }
}
