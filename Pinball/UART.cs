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

    public static void Main()
    {
        port = new SerialPort();
        port.PortName = ("/dev/ttyACM0");
        port.BaudRate = (9600);
        port.Parity = (Parity.None);
        port.DataBits = (8);
        port.StopBits = (StopBits.One);
        port.Handshake = (Handshake.RequestToSend);

        port.ReadTimeout = 500;
        port.WriteTimeout = 500;

        Thread thread = new Thread(Read);

        if(port.IsOpen)
        {
            Console.WriteLine("A porta já está aberta!");
        }
        else
        {
            try
            {
                port.Open();

                Console.WriteLine("A comunicação está aberta!");

                port.DiscardInBuffer();
                port.DiscardOutBuffer();
            }
            catch(Exception)
            {
                Console.WriteLine("Exceção!");
            }
        }

        bool reading = true;
        thread.Start();

        while(reading)
        {
            string m = Console.ReadLine();

            if(m == "<0")
            {
                Console.WriteLine("Enviando <0");
                port.Write(m);
            }
            else if(m == "quit") reading = false;
        }

        thread.Join();
        port.Close();
    }

    private static void Read()
    {
        while(true)
        {
            Console.WriteLine("DataReceivedHandler");

            int indata = -1;

            try
            {
                indata = port.ReadByte();
                message = message + (char)indata;

                Console.WriteLine($"Valor recebido: {indata}");
                Console.WriteLine($"Valor da mensagem {message}\n");

                if(message == "aa")
                {
                    try
                    {
                        port.DiscardOutBuffer();
                        port.Write("<0");

                    }
                    catch(TimeoutException) {}

                    message = "";
                }
                else if(message == "bb")
                {
                    message = "";
                }
            }
            catch(TimeoutException) {

                Console.WriteLine($"tamanho do bupffer{port.BytesToRead}");
            }
        }
    }
}