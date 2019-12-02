using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ClientePrueba
{
    public class FtpClient
    {
        private TcpClient _controlClient;
        private NetworkStream _controlStream;
        private StreamReader _controlReader;
        private StreamWriter _controlWriter;


        private string path = @"C:\pruebas";
        private string fullPath;
        private int BytesPerRead = 1024;

        public FtpClient(TcpClient c)
        {
            _controlClient = c;
            _controlClient.Connect("127.0.0.1", 21);
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

        public void receive2()
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

                    Console.WriteLine(s);
                }
            }
        }
    }
}

