using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFlipper2D : MonoBehaviour
{

    public float speed = 0f;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;
    private SignalHandlerScript signalHandler;
    private bool flipperState;
    void Start()
    {
        flipperState = false;
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
        signalHandler = GameObject
                       .FindGameObjectWithTag(Constants.SIGNAL_HANDLER_TAG_2D)
                       .GetComponent<SignalHandlerScript>();
        
    }

    void FixedUpdate()
    {
        if (signalHandler.fake)
        {
            flipperState = signalHandler.buttons.leftButton;
            if (signalHandler.buttons.leftButton != flipperState)
            {
                Debug.Log("entrei aqui esquerdo");
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
}
