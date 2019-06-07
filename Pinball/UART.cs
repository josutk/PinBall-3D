
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

        port.ReadTimeout = 250;
        port.WriteTimeout = 250;

        port.DtrEnable = true;
        port.RtsEnable = true;

        OpenPort();

        Console.WriteLine("A comunicação está aberta!");
    }


    public static void ChangeSound(string value)
    {
        int hex = Convert.ToInt32(value);
        message[0] = Convert.ToByte(hex); 
    }

    public static void ChangeLights(string value)
    {
        int hex = Convert.ToInt32(value);

        message[1] = Convert.ToByte(hex);
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
            string lightsOrSound = Console.ReadLine();

            if(lightsOrSound == "1")
            {
                string option = Console.ReadLine();
                ChangeLights(option);
            }
            else if(lightsOrSound == "2")
            {
                string option = Console.ReadLine();
                ChangeSound(option);
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