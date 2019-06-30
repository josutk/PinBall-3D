﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public int angle
        {   set 
            {
                angle = value - 2;   
            }

            get
            {
                return angle;
            }
        }
    };

    public Buttons buttons;
    public Launcher launcher;
    public Angle angle;

    public Buttons previousButtons;
    public Launcher previousLauncher;
    public Angle previousAngle;

    public Buttons PreviousButtons
    { get; }

    private int[] previousMessage = new int[2]{-1, -1};
    private int[] message = new int[2]{-1, -1};

    public bool usingMSP = false;

    void Start()
    {
        if(usingMSP) UART.Start();

        buttons = new Buttons(false, false, false);
    }

    void Update()
    {
        if(!usingMSP)
        {
            buttons.leftButton = Input.GetKeyDown(KeyCode.A);
           
            buttons.rightButton = Input.GetKeyDown(KeyCode.D);

            buttons.select = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            message = UART.GetMessage();

            // It doesn't have anything to do with MSP.
            if((message[0] != previousMessage[0]) || (message[1] != previousMessage[1]))
            {

                //TODO(Roger): Check which message (message[0] or message[1] is different and parse accordingly)
                previousMessage[0] = message[0];
                previousMessage[1] = message[1];

                SavePrevious();
                ParseInput();    
            }
        }
    } 

    private bool IsLevelLoaded()
    {
        int numberOfScenes = SceneManager.sceneCount;

        for(int i = 0; i < numberOfScenes; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if(scene.isLoaded && !scene.name.Equals(Constants.FGARCADE_SCENE))
            {
                return true;
            }
        }

        return false;
    }

    private void SavePrevious()
    {
        previousButtons = buttons;
        previousAngle = angle;
        previousLauncher = launcher;
    }

    private void ParseInput()
    {
        buttons.select = Convert.ToBoolean(message[0] & 00000001);

        buttons.leftButton = Convert.ToBoolean(message[0] & 00000010);

        buttons.rightButton = Convert.ToBoolean(message[0] & 00000100);

        launcher.force = (message[0] >> 3) & 00000111;

        // Angle 2 is zero, less than two is left, more than two is right.
        // We save 2 as zero.
        angle.angle = message[1] & 01111111;
    }

    public void ChangeSound(Int32 volume) => UART.ChangeSound(volume);

    public void ChangeLights(Int32 speed) => UART.ChangeLights(speed);
}
