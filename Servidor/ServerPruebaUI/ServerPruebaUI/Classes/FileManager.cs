using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerUI;
using System.Net.Sockets;

namespace Classes
{
    class FileManager
    {
        private string path = @"C:\server\";

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

        public void Download(StreamWriter writer, string file)
        {

            Console.WriteLine(path+file);
            if (File.Exists(path+file))
            {
                writer.WriteLine("FILENAME");
                writer.Flush();
                writer.WriteLine(file);
                writer.Flush();
                using (StreamReader sr = File.OpenText(path+file))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                        writer.WriteLine(s);
                        writer.Flush();
                    }
                }
            }
        }

        

    }
}
