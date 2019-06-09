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
        public bool rightButton;
        public bool leftButton;
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

    private Rigidbody Ball;

    void Start()
    {
        UART.Start();
    }

    void Update()
    {
        message = UART.GetMessage();

        // This if was created because of the way that I did the classes.
        // It doesn't have anything to do with MSP.
        if(!message.Equals(previousMessage))
        {
            //TODO(Roger): Check which message (message[0] or message[1] is different and parse accordingly)
            previousMessage = message;

            ParseInput();    
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
        buttons.leftButton = Convert.ToBoolean(message[0] & 00000010);
        buttons.rightButton = Convert.ToBoolean(message[0] & 00000100);

        launcher.force = (message[0] >> 3) & 00000111;

        angle.angle = message[1] & 01111111;
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
