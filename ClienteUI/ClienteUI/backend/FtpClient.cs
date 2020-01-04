using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClienteUI.backend
{
    public class FtpClient
    {
        private FileManager manager;

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

        public void Disconnect()
        {
            client.GetStream().Close();
            client.Close();
        }

        public string Connect(string user, string pass)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                socket = client.Client;
                manager = new FileManager(socket);
                writer = new StreamWriter(stream);

                string response = Login(user, pass);
                if (response == "y")
                    return "y";
                else if (response == "n")
                    return "n";

                return "nada...";
            } 
            catch(ArgumentNullException e)
            {
                Console.WriteLine("Exception: "+e.Message);
                return e.Message;
            }
            catch (SocketException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return e.Message;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return e.Message;
            }
            
            
        }

        public void Rename(string newName, string oldName)
        {
            string msg = "REN " + oldName;
            Send(msg);
            Thread.Sleep(500);
            msg = "REN " + newName;
            Send(msg);
        }

        public string ReceiveList()
        {
            byte[] buffer = new byte[1024];
            int x = socket.Receive(buffer);
            char[] chars = new char[x];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, x, chars, 0);
            System.String recv = new System.String(chars);
            return recv;
        }

        public void Send(String message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public string Login(string user, string pass)
        {
            Console.WriteLine("LOGIN  " + user + " " + pass);

            string msg = "USER " + user;
            Send(msg);
            Thread.Sleep(500);
            msg = "PASS " + pass;
            Send(msg);
            string response = manager.ReceivedData();
            string[] split = response.Split('!');
            response = split[0];
            Console.WriteLine("La rspuesta esss login " + response);
            return response;
        }

        public string List()
        {
            Send("LIST-F");
            string receive = ReceiveList();
            return receive;
        }

        public string ListDirectories()
        {
            string msg = "LIST-D";
            Send(msg);
            string response = ReceiveList();
            Console.WriteLine("response: "+response);
            return response;
        }

        public void Download(string name,string path)
        {
            Console.WriteLine("name: "+name);
            string msg = "DOWN " + name;
            Send(msg);
            Thread.Sleep(500);
            manager.Download();
        }

        public void Upload(string fullPath, string fileName,string destiny)
        {
            Console.WriteLine("fullPath: "+fullPath);
            Console.WriteLine("fileName: " + fileName);
            string msg = "UP";
            Send(msg);
            Thread.Sleep(500);
            manager.Upload(fullPath,fileName);
        }

        public string CreateFolder(string name)
        {
            string msg = "CRT " + name;
            Send(msg);
            string response = manager.ReceivedData();
            return response;
        }

        public string DeleteFolder(string name,string path)
        {
            string msg = "DEL-D "+name;
            Send(msg);
            string response = manager.ReceivedData();
            return response;
        }

        public string DeleteFile(string name)
        {
            string msg = "DEL-F " + name;
            Send(msg);
            string response = manager.ReceivedData();
            return response;
        }

        public void ReturnDirectory()
        {
            Send("RET");
        }

        public string AccessDirectory(string directory)
        {
            Send("DIR " + directory);
            string response = manager.ReceivedData();
            return response;
        }

        public string Move(string oldPath, string fileName)
        {
            Send("MOV " + oldPath + @"\" + fileName);
            string response = manager.ReceivedData();
            return response;
        }
    }
}
