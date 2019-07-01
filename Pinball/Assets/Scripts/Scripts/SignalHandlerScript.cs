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
        // Necessary so we don't accidentaly cause Stack Overflow.
        private int mAngleX;
        private int mAngleZ;
        public int angleX
        {   set 
            {
                mAngleX = value - 2;   
            }

            get
            {
                return mAngleX;
            }
        }

        public int angleZ
        { 
            get
            {
                return mAngleZ;
            }
            
            set
            {
                mAngleZ = value - 2;
            }
        }

        public Angle(int x, int z)
        {
            mAngleX = x;
            mAngleZ = z;
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

    public bool freeze = false;

    void Start()
    {
        if(usingMSP) UART.Start();

        buttons = new Buttons(false, false, false);
    }

    void Update()
    {
        if(!usingMSP)
        {
            if(!freeze)
            {
                buttons.leftButton = Input.GetKeyDown(KeyCode.A);
           
                buttons.rightButton = Input.GetKeyDown(KeyCode.D);

                buttons.select = Input.GetKeyDown(KeyCode.Space);
            }
            else
            {
                ParseInput();
            }
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
        if(!freeze)
        {
            int isButtons = message[0] >> 7;

            buttons.select = Convert.ToBoolean(message[isButtons] & 0b00000001);

            buttons.leftButton = Convert.ToBoolean(message[isButtons] & 0b00000010);

            buttons.rightButton = Convert.ToBoolean(message[isButtons] & 0b00000100);

            launcher.force = (message[isButtons] >> 3) & 0b00000111;

            if(Convert.ToBoolean(isButtons))
            {
                // Angle 2 is zero, less than two is left, more than two is right.
                // We save 2 as zero.
                angle.angleX = message[0] & 0b00000111;
                angle.angleZ = (message[0] >> 3) & 0b00000111;
            }
            else
            {
                angle.angleX = message[1] & 0b00000111;
                angle.angleZ = (message[1] >> 3) & 0b00000111;
            }

            Debug.Log($"Angulo X: {angle.angleX}");
            Debug.Log($"Ângulo Z: {angle.angleZ}");
        }
        else
        {
            Debug.Log("Frozen!");
            buttons.select = false;
            buttons = new Buttons(false, false, false);
            launcher.force = 0;
            angle.angleX = 2; // 0
            angle.angleZ = 2;
        }
    }

    public void ChangeSound(Int32 volume) => UART.ChangeSound(volume);

    public void ChangeLights(Int32 speed) => UART.ChangeLights(speed);

    private void OnApplicationQuit()
    {
        UART.Stop();
    }

}
