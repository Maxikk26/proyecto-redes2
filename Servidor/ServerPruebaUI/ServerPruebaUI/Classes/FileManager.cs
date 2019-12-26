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
            //Console.WriteLine("result: " + result);
            if (!String.IsNullOrEmpty(result))
            {
                result = result + ",";
                //Console.WriteLine("Lista: " + result);
                byte[] buffer = new byte[1024];
                buffer = System.Text.Encoding.ASCII.GetBytes(result);
                socket.Send(buffer);
            }
            else
            {
                string empty = "empty";
                byte[] buffer = new byte[1024];
                buffer = System.Text.Encoding.ASCII.GetBytes(empty);
                socket.Send(buffer);
            }
                
        }

        public void ListDirectories(string path, Socket socket)
        {
            string[] directories = Directory.GetDirectories(path);
            Console.WriteLine("directories - " + directories.ToString());
            string result = "";
            result = string.Join(",", directories);

            if (!String.IsNullOrEmpty(result))
            {
                result = result + ",";
                byte[] buffer = new byte[1024];
                buffer = Encoding.ASCII.GetBytes(result);
                socket.Send(buffer);
                Console.WriteLine("Directories: " + result);
            }
            else
            {
                string empty = "empty";
                byte[] buffer = new byte[1024];
                buffer = System.Text.Encoding.ASCII.GetBytes(empty);
                socket.Send(buffer);
            }
               
        }

        public void DownloadFromServer(Socket socket,string file,string currentDirectory)
        {
            fullPath = currentDirectory + @"\" + file;
            Console.WriteLine("file-down: " + file);
            if (File.Exists(fullPath))
            {
                byte[] fileNameByte = Encoding.ASCII.GetBytes(file);

                byte[] fileData = File.ReadAllBytes(fullPath);
                double x = 4 + fileNameByte.Length + fileData.Length;
                int y = Convert.ToInt32(x);
                Console.WriteLine("longitud " + x.ToString());
                socket.Send(BitConverter.GetBytes(y));
                Console.WriteLine("Enviada la longitud");
                Thread.Sleep(3500);
                byte[] clientData = new byte[y];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                socket.Send(clientData);
            }
        }

        public void UploadToServer(Socket socket,string destiny)
        {
            destiny += @"\";
            byte[] len = new byte[1024 * 5000];
            socket.Receive(len);
            Console.WriteLine("longitud " + BitConverter.ToInt32(len,0));
            byte[] clientData = new byte[BitConverter.ToInt32(len, 0)];
            decimal bytesReceived;
            bytesReceived = socket.Receive(clientData);
            Console.WriteLine("clientData " + BitConverter.ToInt32(clientData, 0));
            int fileNameLen = BitConverter.ToInt32(clientData, 0);
            string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
            BinaryWriter bWrite = new BinaryWriter(File.OpenWrite(destiny+fileName));
            bWrite.Write(clientData, 4 + fileNameLen, Convert.ToInt32(bytesReceived) - 4 - fileNameLen);
            bWrite.Close();
        }

        public bool CreateFolder(string name, string currentDirectory)
        {
            path = currentDirectory + @"\" + name;
            try
            {
                if (Directory.Exists(path))
                {
                    return false;
                }
                else
                {
                    string folder = path;
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string name,string currentDirectory,bool check)
        {
            try
            {
                if (check)
                    Directory.Delete(currentDirectory + @"\" + name);
                else
                    File.Delete(currentDirectory + @"\" + name);
                return true;
            }
            catch(UnauthorizedAccessException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            catch(PathTooLongException e)
            {
                return false;
            }
            catch(DirectoryNotFoundException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
