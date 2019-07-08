using System;
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
    };

    public Buttons buttons;
    public Launcher launcher;
    public Angle angle;

    public Buttons previousButtons;
    
    private int[] previousMessage = new int[2]{-1, -1};
    private int[] message = new int[2]{-1, -1};

    public bool freeze = false;

    public bool fake = false;
    
    private void Start()
    {
        UART.Start(fake);

        buttons = new Buttons(false, false, false);
    }

    private void Update()
    {
        if(fake)
        {
            SimulateSignals();
        }
        
        int[] temporaryMessage = new int[2];
        temporaryMessage = UART.GetMessage();
        CorrectOrder(temporaryMessage);
        SavePreviousButtons();
        
        if (message[0] != previousMessage[0])
        {
            previousMessage[0] = message[0];
            ParseButtonsAndForce();
        }
            
        if (message[1] != previousMessage[1])
        {
            previousMessage[1] = message[1];
            ParseAngle();
        }

        if(freeze)
        {
            FrozenInput();
        }
    }

    private static void SimulateSignals()
    {
        /*
            There is a bug with using GetKeyDown, which will be the case for the launcher.
            Unfortunately, the variable generateLauncher becomes true AND false before the
            thread can read it and generate the signal. Probably won't happen with the MSP.
         */
        if (Input.GetKey(KeyCode.L))
        {
            UART.generateLauncher = true;
        }
        else
        {
            UART.generateLauncher = false;
        }

        if (Input.GetKey(KeyCode.C))
        {
            UART.generateButtonLeft = true;
        }
        else
        {
            UART.generateButtonLeft = false;
        }

        if (Input.GetKey(KeyCode.B))
        {
            UART.generateButtonRight = true;
        }
        else
        {
            UART.generateButtonRight = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            UART.generateButtonSelect = true;
        }
        else
        {
            UART.generateButtonSelect = false;
        }

        if(Input.GetKey(KeyCode.Return))
        {
            UART.generateAngle = true;
        }
        else
        {
            UART.generateAngle = false;
        }
    }

    private void CorrectOrder(int[] temporaryMessage)
    {
        // Buttons and force, then angle
        if (!Convert.ToBoolean(temporaryMessage[0] >> 7))
        {
            message[0] = temporaryMessage[0];
            message[1] = temporaryMessage[1];
        }
        else
        {
            message[0] = temporaryMessage[1];
            message[1] = temporaryMessage[0];
        }
    }

    private void SavePreviousButtons()
    {
        previousButtons = buttons;
    }

    private void FrozenInput()
    {   
        buttons.select = false;
        launcher.force = 0;
        angle.angleX = 2; // 0
        angle.angleZ = 2;
    }

    private void ParseAngle()
    {
        angle.angleX = message[1] & 0b00000111;
        angle.angleZ = (message[1] >> 3) & 0b00000111;
    }

    private void ParseButtonsAndForce()
    {
        buttons.select = Convert.ToBoolean(message[0] & 0b00000001);

        buttons.leftButton = Convert.ToBoolean(message[0] & 0b00000010);

        buttons.rightButton = Convert.ToBoolean(message[0] & 0b00000100);

        launcher.force = (message[0] >> 3) & 0b00000111;
    }

    public void ChangeSound(Int32 volume) => UART.ChangeSound(volume);

    public void ChangeLights(Int32 speed)
    {
        UART.ChangeLights(speed);
    }

    public void SendMessage() 
    { 
        UART.SendMessage();
    }

    private void OnApplicationQuit()
    {
        UART.Stop();
    }
}
