using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignalHandlerScript : MonoBehaviour
{

    public struct Buttons 
    {
        public bool select;
        public bool rFlipper;
        public bool lFlipper;
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

    private Buttons previousButtons;
    private Launcher previousLauncher;
    private Angle previousAngle;

    private int[] previousMessage;
    private int[] message;

    private FlipperScript RFlipper;
    private FlipperScript LFlipper;

    private Rigidbody Ball;

    void Start()
    {
        UART.Start();
    }

    void Update()
    {
        bool isLevelLoaded = IsLevelLoaded();

        if(isLevelLoaded) FindInteractibles();

        message = UART.GetMessage();

        // This if was created because of the way that I did the classes.
        // It doesn't have anything to do with MSP.
        if(!message.Equals(previousMessage))
        {
            //TODO(Roger): Check which message (message[0] or message[1] is different and parse accordingly)
            previousMessage = message;

            ParseInput();    

            if(isLevelLoaded)
            {
                ApplyInteractions();
            }
            else
            {
                // Move Menu Screen.
            }

        }
    }

    private void FindInteractibles()
    {
        if(RFlipper == null)
        {
            RFlipper = GameObject.FindGameObjectWithTag(Constants.RIGHT_FLIPPER_TAG).GetComponent<FlipperScript>();
        }

        if(LFlipper == null)
        {
            LFlipper = GameObject.FindGameObjectWithTag(Constants.LEFT_FLIPPER_TAG).GetComponent<FlipperScript>();
        }

        if(Ball == null)
        {
            Ball = GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG).GetComponent<Rigidbody>();
        }
    }

    private bool IsLevelLoaded()
    {
        int numberOfScenes = SceneManager.sceneCount;

        for(int i = 0; i < numberOfScenes; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if(scene.isLoaded && !scene.name.Equals(Constants.FGA_ARCADE_LEVEL_NAME))
            {
                return true;
            }
        }

        return false;
    }

    private void ParseInput()
    {
        buttons.select = Convert.ToBoolean(message[0] & 00000001);
        buttons.lFlipper = Convert.ToBoolean(message[0] & 00000010);
        buttons.rFlipper = Convert.ToBoolean(message[0] & 00000100);

        launcher.force = (message[0] >> 3) & 00000111;

        angle.angle = message[1] & 01111111;
    }

    private void ApplyInteractions()
    {
        if(buttons.select != previousButtons.select)
        {
            previousButtons.select = buttons.select;

        }

        if(buttons.lFlipper != previousButtons.lFlipper)
        {
            previousButtons.lFlipper = buttons.lFlipper;

            LFlipper.Move = buttons.lFlipper;
        }

        if(buttons.rFlipper != previousButtons.rFlipper)
        {
            previousButtons.rFlipper = buttons.rFlipper;

            RFlipper.Move = buttons.rFlipper;
        }

        if(launcher.force != previousLauncher.force)
        {
            previousLauncher.force = launcher.force;
        }

        if(angle.angle != previousAngle.angle)
        {
            previousAngle.angle = angle.angle;
        }
    }
    
    public void ChangeSound(Int32 volume)
    {
        UART.ChangeSound(volume);
    }

    public void ChangeLights(Int32 speed)
    {
        UART.ChangeLights(speed);
    }
}
