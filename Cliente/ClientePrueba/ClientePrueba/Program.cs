using System;
using System.Net.Sockets;

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
                if (msg.Equals("CLOSE"))
                {
                    c.close();
                    Environment.Exit(0);
                }
                else
                {
                    c.send(msg);
                    c.receive2();
                }
                
            }
                       

        }
    }
}
