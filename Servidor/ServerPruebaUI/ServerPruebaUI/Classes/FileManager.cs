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

        public void DownloadTxt(StreamWriter writer, string file)
        {
            fullPath = path + @"\" + file;
            if (File.Exists(fullPath))
            {
                writer.WriteLine("FILENAME");
                writer.Flush();
                writer.WriteLine(file);
                writer.Flush();
                using (StreamReader sr = File.OpenText(fullPath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        writer.WriteLine(s);
                        writer.Flush();
                    }
                }
            }
        }

        public void DownloadFromServer(Socket socket, string file)
        {
            fullPath = path + file;
            byte[] fileNameByte = Encoding.ASCII.GetBytes(file);

            byte[] fileData = File.ReadAllBytes(fullPath);
            double x = 4 + fileNameByte.Length + fileData.Length;
            int y = Convert.ToInt32(x);
            Console.WriteLine("longitud "+x.ToString());
            socket.Send(BitConverter.GetBytes(y));
            
            Thread.Sleep(3500);
            byte[] clientData = new byte[y];
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);
            socket.Send(clientData);

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
