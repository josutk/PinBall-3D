using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFlipper2D : MonoBehaviour
{

    public float speed = 0f;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;

    void Start()
    {
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
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
}
