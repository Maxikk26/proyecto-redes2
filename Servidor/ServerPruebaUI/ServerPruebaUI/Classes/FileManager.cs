using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerUI;
using System.Net.Sockets;
using System.Drawing;
using System.Net;

namespace Classes
{
    class FileManager
    {
        private string path = @"C:\server\";
        private string fullPath;

        public FileManager()
        {
        }

        public bool rootDirectory()
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    return false;
                }
            } 
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public void List(string fullPath, Socket socket)
        {
            string[] files = Directory.GetFiles(fullPath, "*.*")
                                     .Select(Path.GetFileName)
                                     .ToArray();
            string result = string.Join(",", files);

            byte[] buffer = new byte[1024];
            buffer = System.Text.Encoding.ASCII.GetBytes(result);
            socket.Send(buffer);

            /*

            int x = Convert.ToInt32(result.Length);
            Console.WriteLine(x);
            socket.Send(BitConverter.GetBytes(x));
            Thread.Sleep(1500);

            byte[] buffer = new byte[x];
            buffer = Encoding.ASCII.GetBytes(result);
            socket.Send(buffer);
            */
        }

        public void DownloadFromServer(Socket socket, string file)
        {
            fullPath = path + file;
            if (File.Exists(fullPath))
            {
                byte[] fileNameByte = Encoding.ASCII.GetBytes(file);

                byte[] fileData = File.ReadAllBytes(fullPath);
                double x = 4 + fileNameByte.Length + fileData.Length;
                int y = Convert.ToInt32(x);
                Console.WriteLine("longitud " + x.ToString());
                socket.Send(BitConverter.GetBytes(y));

                Thread.Sleep(3500);
                byte[] clientData = new byte[y];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                socket.Send(clientData);
            }
        }

        public void UploadToServer(Socket socket)
        {
            byte[] len = new byte[1024 * 5000];
            socket.Receive(len);
            Console.WriteLine("longitud " + BitConverter.ToInt32(len,0));
            byte[] clientData = new byte[BitConverter.ToInt32(len, 0)];
            decimal bytesReceived;
            bytesReceived = socket.Receive(clientData);
            Console.WriteLine("clientData " + BitConverter.ToInt32(clientData, 0));
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            BinaryWriter bWrite = new BinaryWriter(File.OpenWrite(path+fileName));
            bWrite.Write(clientData, 4 + fileNameLen, Convert.ToInt32(bytesReceived) - 4 - fileNameLen);
            bWrite.Close();
        }
    }
}
