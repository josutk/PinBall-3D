using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFlipper2D : MonoBehaviour
{
    public float speed = 0f;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;

    void Start()
    {
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motor2D.motorSpeed = -speed;
            myHingeJoint.motor = motor2D;

        }
        else
        {
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
        }
    }
}
