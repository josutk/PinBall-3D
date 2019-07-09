using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFlipper2D : MonoBehaviour
{
    public float speed = 0f;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;
    private SignalHandlerScript signalHandler;

    void Start()
    {
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
        signalHandler = GameObject
                      .FindGameObjectWithTag(Constants.SIGNAL_HANDLER_TAG)
                      .GetComponent<SignalHandlerScript>();
    }
    void Update()
    {
       if (signalHandler.buttons.rightButton)
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
