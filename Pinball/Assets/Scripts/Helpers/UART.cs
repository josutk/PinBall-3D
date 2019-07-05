
// Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.
// Replace the code in Program.cs with this code.

using System;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;

public class UART 
{
    static SerialPort port;
    static byte[] ok_message = new byte[1]{ 0xFF };
    static byte[] request = new byte[1]{ 0xF0 };
    static byte[] message = new byte[2] { 0, 0 };
    private static int[] receivedMessage = new int[2]{-1, -1};
    private static LastQueue<int[]> buffer = new LastQueue<int[]>();

    private static bool mShouldStopThread = false;

    private static Thread thread;

    private static Mutex mutex = new Mutex();

    public static bool generateLauncher;
    public static bool generateButtonLeft;
    public static bool generateButtonRight;
    public static bool generateButtonSelect;
    internal static bool generateAngle;

    private static void OpenPort()
    {
        if(port.IsOpen)
        {
            Debug.Log("Is already open!");
            port.Close();
            Thread.Sleep(250);
            port.Open();
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }
        else
        {
            Debug.Log("Open!");
            port.Open();
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }
    }

    private static void Configure()
    {
        port = new SerialPort();
        port.PortName = (SerialPort.GetPortNames()[0]);
        port.BaudRate = (9600);
        port.Parity = (Parity.None);
        port.DataBits = (8);
        port.StopBits = (StopBits.One);
        port.Handshake = (Handshake.RequestToSend);

        port.ReadTimeout = 500;
        port.WriteTimeout = 500;

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
        message[0] = Convert.ToByte(message[0] & 0b01111111);
        message[1] = Convert.ToByte(message[1] | 0b10000000);

        port.Write(request, 0, 1);
        port.Write(message, 0, 2);
    }

    public static void Start(bool fake = false)
    {
        if(fake)
        {
            thread = new Thread(FakeRead);
            thread.Start();
        }
        else
        {
            Configure();

            thread = new Thread(Read);
            thread.Start();

            InitializeCommunication();
        }
    }

    private static void FakeRead()
    {
        //Debug.Log("Starting Fake Thread!");

        while(!mShouldStopThread)
        {

            mutex.WaitOne();

            receivedMessage[0] = 0;
            receivedMessage[1] = 0;

            if (generateButtonLeft)
            {
                //Debug.Log("Button Left!");
                GenerateButtonLeft();
            }

            if (generateButtonRight)
            {
                GenerateButtonRight();
            }

            if (generateButtonSelect)
            {
                GenerateButtonSelect();
            }

            if (generateLauncher)
            {
                Debug.Log("UART Generate Launcher");
                GenerateLauncher();
            }

            if (generateAngle)
            {
                GenerateAngle();
            }

            AddToBuffer();

            mutex.ReleaseMutex();

            // if (receivedMessage[0] != 0 || receivedMessage[1] != 0)
            // {
            //     Debug.Log($"GetMessage receivedMessage: {receivedMessage[0]} {receivedMessage[1]}");
            // }
        }

        Debug.Log("Finished fake read!");
    }

    private static void AddToBuffer()
    {
        if (buffer.Count > 0)
        {
            int[] last = buffer.Last;

            if (last[0] != receivedMessage[0]
                || last[1] != receivedMessage[1])
            {
                buffer.Enqueue(new int[2] { receivedMessage[0], receivedMessage[1] });
            }
        }
        else
        {
            buffer.Enqueue(new int[2] { receivedMessage[0], receivedMessage[1] });
        }
    }

    private static void GenerateAngle()
    {
        receivedMessage[1] = receivedMessage[1] | 0b10001010;
    }

    private static void GenerateButtonSelect()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000001;
    }

    private static void GenerateButtonRight()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000100;
    }

    private static void GenerateLauncher()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00010000;

        // Debug.Log($"Message After Launcher {receivedMessage[0]}");
    }

    private static void GenerateButtonLeft()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000010;
        //Debug.Log($"Received Message: {receivedMessage[0]}");
    }

    public static void Stop()
    {
        Debug.Log("Quit!");

        mShouldStopThread = true;
        if(thread != null && thread.IsAlive)
        {
            thread.Join();
        }

        if(port != null && port.IsOpen)
        {
            byte[] messageToSend = new byte[1]{ 0xFD };
        
            port.Write(message, 0, 1);
            Thread.Sleep(500);
            port.Close();
        }
    }

    private static void Read()
    {
        while(!mShouldStopThread)
        {
            try 
            {
                mutex.WaitOne();
                receivedMessage[0] = port.ReadByte();

                receivedMessage[1] = port.ReadByte();

                AddToBuffer();
                mutex.ReleaseMutex();

                if(receivedMessage[0] != -1 || receivedMessage[1] != -1)
                {
                    UnityEngine.Debug.Log($"GetMessage receivedMessage: {receivedMessage[0]} {receivedMessage[1]}");
                }
            }
            catch(TimeoutException e) {
                Debug.Log($"Erro: {e.Message}");
                mutex.ReleaseMutex();
            }
            catch(IOException){
            }
        }
    }

    public static int[] GetMessage()
    {
        
        mutex.WaitOne();
        // Never return the same array, or you will return it's reference.
        // if(receivedMessage[0] == 8)
        // {
        //     Debug.Log("Oito!");
        // }

        int[] fromBuffer;

        if(buffer.Count > 0)
        {
            fromBuffer = buffer.Dequeue();
        }
        else
        {
            fromBuffer = new int[2]{0 , 0};
        } 

        int[] messageToSend = new int[2] {fromBuffer[0], fromBuffer[1] };
        mutex.ReleaseMutex();

        //Debug.Log($"Message to send: {messageToSend[0]} {messageToSend[1]}");

        
        return messageToSend;
    }
}