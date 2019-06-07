
// Use this code inside a project created with the Visual C# > Windows Desktop > Console Application template.
// Replace the code in Program.cs with this code.

using System;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

public class PortChat
{
    static SerialPort port;
    static string message;
    static byte[] ok_message = new byte[1]{ 0xFF };
    static byte[] request = new byte[1]{ 0xF0 };

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

        OpenPort();

        Console.WriteLine("A comunicação está aberta!");
    }


    public static void Main()
    {
        Configure();

        bool reading = true;

        Thread thread = new Thread(Read);
        thread.Start();
        
        
        Console.WriteLine($"A thread de leitura está viva? {thread.IsAlive}");

        byte[] byteToSend = new byte[1]{0xFF};

        port.Write(byteToSend, 0, 1);

        thread.Join();
        port.Close();
    }

    private static void Read()
    {
        while(true)
        {

            Console.WriteLine("Reading!");
            byte[] byteToRead = new byte[2];
            byte[] generic = new byte[1]{ 0x41 };

            try
            {
                port.Read(byteToRead, 0, 2);

                Console.WriteLine($"Tamanho {byteToRead.Length}");

                if(byteToRead.Length == 2)
                {
                    port.Write(ok_message, 0, 1);

                }

                int b = byteToRead[0];

                if(b == 0x41)
                {
                    Console.WriteLine("São Iguais!");
                }
                else
                {
                    Console.WriteLine($"Valor recebido: {b}");
                }

            }
            catch(TimeoutException) {
            }
        }
    }
}