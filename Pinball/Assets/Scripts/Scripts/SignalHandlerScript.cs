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
    
    private int[] previousMessage = new int[2]{-1, -1};
    private int[] message = new int[2]{-1, -1};

    public bool usingMSP = false;

    public bool freeze = false;

    public bool fake = false;
    
    void Start()
    {
        if(usingMSP) UART.Start(fake);

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
                FrozenInput();
            }
        }
        else
        {
            if(fake)
            {
                if(Input.GetKeyDown(KeyCode.L))
                {
                    UART.generateLauncher = true;
                    Debug.Log("Generate UART Launcher");
                }

                if(Input.GetKeyDown(KeyCode.C))
                {
                    UART.generateButtonLeft = true;
                }

                if(Input.GetKeyDown(KeyCode.B))
                {
                    UART.generateButtonRight = true;
                }

                if(Input.GetKeyDown(KeyCode.S))
                {
                    UART.generateButtonSelect = true;
                }
            }

            int[] temporaryMessage;

            temporaryMessage = UART.GetMessage();

            if(temporaryMessage != UART.defaultMessage)
            {
                CorrectOrder(temporaryMessage);

                Debug.Log($"Corrected Orders {message[0]} {message[1]}");

                if (message[0] != previousMessage[0])
                {
                    previousMessage[0] = message[0];
                    SavePreviousButtons();
                    ParseButtonsAndForce();
                }

                if (message[1] != previousMessage[1])
                {
                    previousMessage[1] = message[1];
                    ParseAngle();
                }
            }
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
        buttons = new Buttons(false, false, false);
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
        Debug.Log("Parsing Buttons and Force!");

        buttons.select = Convert.ToBoolean(message[0] & 0b00000001);

        buttons.leftButton = Convert.ToBoolean(message[0] & 0b00000010);

        buttons.rightButton = Convert.ToBoolean(message[0] & 0b00000100);

        launcher.force = (message[0] >> 3) & 0b00000111;

        Debug.Log($"Launcher force {launcher.force}");
    }

    public void ChangeSound(Int32 volume) => UART.ChangeSound(volume);

    public void ChangeLights(Int32 speed) => UART.ChangeLights(speed);

    private void OnApplicationQuit()
    {
        UART.Stop();
    }
}
