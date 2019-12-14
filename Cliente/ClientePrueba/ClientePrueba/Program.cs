using System;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;

namespace ClientePrueba
{
    class Program
    {
        static void Main(string[] args)
        {
            String msg;
            TcpClient client = new TcpClient();
            FtpClient c = new FtpClient(client);
            c.receive();
            while (true)
            {
                Console.Write("Enter Command: ");
                msg = Console.ReadLine();
                string[] command = msg.Split(' ');
                string cmd = command[0].ToUpperInvariant();
                string arguments = command.Length > 1 ? msg.Substring(command[0].Length + 1) : null;
                if (msg.Equals("CLOSE"))
                {
                    c.close();
                    Environment.Exit(0);
                }
                else
                {
                    if (cmd.Equals("UP"))
                    {
                        c.send(msg);
                        c.send2(msg);
                    }
                    else if (cmd.Equals("DOWN"))
                    {
                        c.send(msg);
                        c.receive2(msg);
                    }
                    else if (cmd.Equals("LIST"))
                    {
                        c.send(msg);
                        Console.WriteLine("Envio mensaje LIST");
                        Thread.Sleep(1000);
                        c.receive();
                        Console.WriteLine("Paso c.receive ");
                    }
                        
                    
                    
                }
                
            }
                       

        }
    }
}
