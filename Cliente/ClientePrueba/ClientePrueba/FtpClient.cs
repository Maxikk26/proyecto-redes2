using System;
using System.IO;
using System.Net.Sockets;
using System.Drawing;
using System.Text;
using System.Linq;

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
            /*if (msg.Contains(".txt"))
            {
                String line;
                if (!string.IsNullOrEmpty(line = _controlReader.ReadLine()))
                {
                    if (line.Equals("FILENAME"))
                    {
                        line = _controlReader.ReadLine();
                        fullPath = path + @"\" + line;
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    }
                    string s = "";
                    while ((s = _controlReader.ReadLine()) != null)
                    {
                        if (s.Equals("221"))
                            break;
                        File.AppendAllText(fullPath, s);
                        File.AppendAllText(fullPath, "\n");
                    }
                }
            }*/
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

