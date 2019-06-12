
// Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.
// Replace the code in Program.cs with this code.

using System;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using UnityEngine;

public class UART 
{
    static SerialPort port;
    static byte[] ok_message = new byte[1]{ 0xFF };
    static byte[] request = new byte[1]{ 0xF0 };
    static byte[] message = new byte[2];

    private static int[] receivedMessage = new int[2]{-1, -1};

    private static void OpenPort()
    {
        if(port.IsOpen)
        {
            port.Close();
            OpenPort();
        }
        else
        {
            port.Open();
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }
    }

    private static void Configure()
    {
        port = new SerialPort();
        port.PortName = ("/dev/ttyACM1");
        port.BaudRate = (9600);
        port.Parity = (Parity.None);
        port.DataBits = (8);
        port.StopBits = (StopBits.One);
        port.Handshake = (Handshake.RequestToSend);

        port.ReadTimeout = 250;
        port.WriteTimeout = 250;

        port.DtrEnable = true;
        port.RtsEnable = true;

        OpenPort();
    }

    private static void InitializeCommunication()
    {
        byte[] byteToSend = new byte[1]{0xDF};

        port.Write(byteToSend, 0, 1);
    }

    public static void ChangeSound(Int32 volume)
    {
        message[0] = Convert.ToByte(volume); 
    }

    public static void ChangeLights(Int32 speed)
    {
        message[1] = Convert.ToByte(speed);
    }

    public static void SendMessage()
    {
        port.Write(request, 0, 1);
        port.Write(message, 0, 2);
    }

    public static void Start()
    {
        Configure();

        Thread thread = new Thread(Read);
        thread.Start();

        InitializeCommunication();
    }

    private static void Read()
    {
        while(true)
        {
            try 
            {
                receivedMessage[0] = port.ReadByte();

                receivedMessage[1] = port.ReadByte();

                if(receivedMessage[0] != -1 || receivedMessage[1] != -1)
                    UnityEngine.Debug.Log($"GetMessage receivedMessage: {receivedMessage[0]} {receivedMessage[1]}");
            }
            catch(TimeoutException) {
            }
        }
    }

    public static int[] GetMessage()
    {
        

        return receivedMessage;
    }
}