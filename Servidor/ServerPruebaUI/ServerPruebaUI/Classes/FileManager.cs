using System;
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

        public void DownloadImage(Socket socket, string file,StreamWriter writer)
        {
            fullPath = path + file;
            byte[] fileNameByte = Encoding.ASCII.GetBytes(file);

            byte[] fileData = File.ReadAllBytes(fullPath);
            int x = 4 + fileNameByte.Length + fileData.Length;
            Console.WriteLine("longitud "+x.ToString());
            writer.Write(x.ToString());
            writer.Flush();
            byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);
            socket.Send(clientData);
            socket.Close();

        }

        

    }
}
