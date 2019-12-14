using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteUI.backend
{
    public class FtpClient
    {
        private string ip;
        private Int32 port;

        private TcpClient client;
        private NetworkStream stream;
        private StreamWriter writer;
        private Socket socket;

        public FtpClient(string _ip, Int32 _port)
        {
            ip = _ip;
            port = _port;

        }

        public void Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                socket = client.Client;
                writer = new StreamWriter(stream);
                Receive();
            } 
            catch(ArgumentNullException e)
            {
                Console.WriteLine("Exception: "+e.Message);
                throw e;
            }
            catch (SocketException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw e;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw e;
            }
            
        }

        public void Receive()
        {
            byte[] buffer = new byte[1024];
            socket.Receive(buffer);
            var str = Encoding.ASCII.GetString(buffer);
            Console.WriteLine("str: "+str);

        }

        public void Send(String message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void List()
        {
            Send("LIST");
            Receive();
        }



    }
}
