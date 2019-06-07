
// Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.
// Replace the code in Program.cs with this code.

using System;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

public class PortChat
{
    static SerialPort port;
    static byte[] ok_message = new byte[1]{ 0xFF };
    static byte[] request = new byte[1]{ 0xF0 };
    static byte[] message = new byte[2];
    static bool b = true;

    public static void OpenPort()
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

    public static void Configure()
    {
        port = new SerialPort();
        port.PortName = ("/dev/ttyACM0");
        port.BaudRate = (9600);
        port.Parity = (Parity.None);
        port.DataBits = (8);
        port.StopBits = (StopBits.One);
        port.Handshake = (Handshake.RequestToSend);

        port.ReadTimeout = -1;
        port.WriteTimeout = -1;

        port.DtrEnable = true;
        port.RtsEnable = true;

        OpenPort();

        Console.WriteLine("A comunicação está aberta!");
    }


    public static void ChangeSound(int value)
    {
        message[0] = BitConverter.GetBytes(value)[0];
    }

    public static void ChangeLights(int value)
    {
        message[1] = BitConverter.GetBytes(value)[0];
    }

    private static void SendMessage()
    {
        port.Write(request, 0, 1);
        port.Write(message, 0, 2);
    }

    public static void Main()
    {
        Configure();

        bool reading = true;

        Thread thread = new Thread(Read);
        thread.Start();
        
        byte[] byteToSend = new byte[1]{0xDF};

        port.Write(byteToSend, 0, 1);

        while(true)
        {
            string option = Console.ReadLine();

            if(option == "1")
            {
                Console.WriteLine("Escolheu a Opção 1");
                ChangeSound(0x01);
            }
            else if(option == "2")
            {
                Console.WriteLine("Escolheu a opção 2");
                ChangeSound(0x02);
            }

            SendMessage();
        }

        thread.Join();
        port.Close();
    }

    private static void Read()
    {
        while(true)
        {
            int byteToRead;

            try 
            {
                byteToRead = port.ReadByte();

                Console.WriteLine($"Byte recebido: {byteToRead}") ;

                byteToRead = port.ReadByte();

                Console.WriteLine($"Byte 2 recebido: {byteToRead}") ;

            }
            catch(TimeoutException) {
            }
        }
    }
}