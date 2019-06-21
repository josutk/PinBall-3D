using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript2D : MonoBehaviour
{
    // Start is called before the first frame update
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
    void Update()
    {
        if((flipper == true && Input.GetKey(KeyCode.LeftArrow)))
        {
            GetComponent<HingeJoint2D>().useMotor = true;
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
        }
        else if ((flipper == false && Input.GetKey(KeyCode.RightArrow))) {
            GetComponent<HingeJoint2D>().useMotor = true;
            
            //GetComponent<HingeJoint2D>().
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
        }
        else
        {
            motor2D.motorSpeed = -speed;
            myHingeJoint.motor = motor2D;
            GetComponent<HingeJoint2D>().useMotor = false;
            
        }
    }
}
