
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
    public class LastQ<T> : Queue<T>
    {
        public T Last { get; private set; }

        public new void Enqueue(T item)
        {
            Last = item;
            base.Enqueue(item);
        }
    }
    static SerialPort port;
    static byte[] ok_message = new byte[1]{ 0xFF };
    static byte[] request = new byte[1]{ 0xF0 };
    static byte[] message = new byte[2];

    private static int[] receivedMessage = new int[2]{-1, -1};

    public static bool mShouldStopThread = false;

    private static Thread thread;

    private static LastQ<int[]> buffer = new LastQ<int[]>();

    private static Semaphore semaphore;

    public static int[] defaultMessage = new int[2]{ -1, -1};
    public static bool generateLauncher;
    public static bool generateButtonLeft;
    public static bool generateButtonRight;
    public static bool generateButtonSelect;

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

    public static void Start(bool fake = false)
    {
        if(fake)
        {
            //semaphore = new Semaphore(0, 1);
            thread = new Thread(FakeRead);
            receivedMessage[0] = 0;
            receivedMessage[1] = 0;
            thread.Start();
        }
        else
        {
            Configure();

            semaphore = new Semaphore(0, 1);

            thread = new Thread(Read);
            thread.Start();

            InitializeCommunication();
        }
    }

    private static void FakeRead()
    {
        Debug.Log("Starting Fake Thread!");

        while(!mShouldStopThread)
        {
            Debug.Log("Inside Fake Read!");
            if(generateButtonLeft)
            {
                GenerateButtonLeft();
                generateButtonLeft = false;
            }
            else if(generateButtonRight)
            {
                GenerateButtonRight();
                generateButtonRight = false;
            }
            else if(generateButtonSelect)
            {
                GenerateButtonSelect();
                generateButtonSelect = false;
            }

            if(generateLauncher)
            {
                GenerateLauncher();
                generateLauncher = false;
            }

            if(receivedMessage[0] != 0 || receivedMessage[1] != 0)
            {
                Debug.Log($"GetMessage receivedMessage: {receivedMessage[0]} {receivedMessage[1]}");
            }
        }
    }

    private static void GenerateButtonSelect()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000100;

        //semaphore.WaitOne();

        AddToBuffer();

        //semaphore.Release();
    }

    private static void AddToBuffer()
    {
        if (buffer.Count > 0 && buffer.Last != receivedMessage)
        {
            buffer.Enqueue(receivedMessage);
        }
        else
        {
            buffer.Enqueue(receivedMessage);
        }

        Debug.Log($"Message to Enqueue {receivedMessage[0]}");        
    }

    private static void GenerateButtonRight()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000010;

        //semaphore.WaitOne();

        AddToBuffer();

        //semaphore.Release();
    }

    private static void GenerateLauncher()
    {
        Debug.Log("Generate Launcher!");

        receivedMessage[0] = receivedMessage[0] | 0b00001000;

        Debug.Log($"Message After Launcher {receivedMessage[0]}");

        //semaphore.WaitOne();

        AddToBuffer();

        //semaphore.Release();

        receivedMessage[0] = receivedMessage[0] & 0b00000000;
        
        //semaphore.WaitOne();        
        AddToBuffer();
        //semaphore.Release();

    }

    private static void GenerateButtonLeft()
    {
        receivedMessage[0] = receivedMessage[0] | 0b00000001;

        //semaphore.WaitOne();

        AddToBuffer();

        //semaphore.Release();
    }

    public static void Stop()
    {
        Debug.Log("Quit!");

        mShouldStopThread = true;

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
                receivedMessage[0] = port.ReadByte();

                receivedMessage[1] = port.ReadByte();

                semaphore.WaitOne();

                AddToBuffer();

                semaphore.Release();

                if(receivedMessage[0] != -1 || receivedMessage[1] != -1)
                {
                    UnityEngine.Debug.Log($"GetMessage receivedMessage: {receivedMessage[0]} {receivedMessage[1]}");
                }
            }
            catch(TimeoutException) {
            }
            catch(IOException){
            }
        }
    }

    public static int[] GetMessage()
    {
        Debug.Log("Get Message!");

        if(buffer.Count > 0)
        {
            //semaphore.WaitOne();

            int[] messageToSend = buffer.Dequeue();

            //semaphore.Release();
            return messageToSend;
        }
        else
        {
            return defaultMessage;
        }
    }
}