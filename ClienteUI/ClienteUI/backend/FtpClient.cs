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

        public string Receive()
        {
            byte[] buffer = new byte[1024];
            int x = socket.Receive(buffer);
            char[] chars = new char[x];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, x, chars, 0);
            System.String recv = new System.String(chars);
            Console.WriteLine(recv);
            return recv;
        }

        public void Send(String message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public string List()
        {
            Send("LIST");
            string receive = Receive();
            return receive;
        }



    }
}
