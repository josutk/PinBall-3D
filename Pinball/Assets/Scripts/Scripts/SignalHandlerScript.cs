using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalHandlerScript : MonoBehaviour
{
    public struct Buttons 
    {
        public bool select;
        public bool rightButton;
        public bool leftButton;

        public Buttons(bool select, bool rightButton, bool leftButton)
        {
            this.select = select;
            this.rightButton = rightButton;
            this.leftButton = leftButton;
        }
    };

    public struct Launcher
    {
        public int force;
    };

    public struct Angle
    {
        public int angle;
    };

    public Buttons buttons;
    public Launcher launcher;
    public Angle angle;    

    public bool usingMSP = false;

    // Update is called once per frame
    void Update()
    {

        if(!usingMSP)
        {
            buttons.leftButton = Input.GetKeyDown(KeyCode.A);
           
            buttons.rightButton = Input.GetKeyDown(KeyCode.D);

            buttons.select = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
