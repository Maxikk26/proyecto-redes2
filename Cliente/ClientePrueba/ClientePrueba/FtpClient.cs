using System;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;

namespace ClientePrueba
{
    public class FtpClient
    {
        private TcpClient _controlClient;
        private NetworkStream _controlStream;
        private StreamReader _controlReader;
        private StreamWriter _controlWriter;
        private Socket socket;


        private string path = @"C:\pruebas";
        private string fullPath;
        private int BytesPerRead = 1024;

        public FtpClient(TcpClient c)
        {
            _controlClient = c;
            socket = _controlClient.Client;
            _controlClient.Connect("127.0.0.1", 50125);
            _controlStream = _controlClient.GetStream();
            _controlWriter = new StreamWriter(_controlStream);
            _controlReader = new StreamReader(_controlStream);
            
        }

        public void close()
        {
            send("QUIT");
            _controlClient.Close();
        }
        public void send(String message)
        {
            _controlWriter.WriteLine(message);
            _controlWriter.Flush();
            
        }

        public void send2(string msg)
        {
            string[] command = msg.Split(' ');
            string cmd = command[0].ToUpperInvariant();
            string arguments = command.Length > 1 ? msg.Substring(command[0].Length + 1) : null;
            fullPath = path + @"\" + arguments;
            if (File.Exists(fullPath))
            {
                Console.WriteLine("Existe el archivo!");
                byte[] fileNameByte = Encoding.ASCII.GetBytes(arguments);
                byte[] fileData = File.ReadAllBytes(fullPath);
                double x = 4 + fileNameByte.Length + fileData.Length;
                int y = Convert.ToInt32(x);
                socket.Send(BitConverter.GetBytes(y));
                Thread.Sleep(3500);
                byte[] clientData = new byte[y];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData,4);
                fileData.CopyTo(clientData,4 + fileNameByte.Length);
                socket.Send(clientData);
            }

        }

        public void receive()
        {
            String line;
            if (!string.IsNullOrEmpty(line = _controlReader.ReadLine()))
            {
                Console.WriteLine(line);
            }
        }

        public void receive2(string msg)
        {
            if (msg.Contains(".txt") || msg.Contains(".png")||msg.Contains(".docx")||msg.Contains(".xlsx")||msg.Contains(".zip")||msg.Contains(".jpg")||msg.Contains(".pdf")||msg.Contains(".rar"))
            {
                byte[] len = new byte[1024*5000];
                socket.Receive(len);
                Console.WriteLine("longitud "+ BitConverter.ToInt32(len));
                byte[] clientData = new byte[BitConverter.ToInt32(len)];
                decimal bytesReceived;
                bytesReceived = socket.Receive(clientData);
                Console.WriteLine("clientData " + BitConverter.ToInt32(clientData,0));
                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                BinaryWriter bWrite = new BinaryWriter(File.OpenWrite(path +@"\"+ fileName));
                bWrite.Write(clientData, 4 + fileNameLen, Convert.ToInt32(bytesReceived) - 4 - fileNameLen);
                bWrite.Close();

            }

        }
    }
}

