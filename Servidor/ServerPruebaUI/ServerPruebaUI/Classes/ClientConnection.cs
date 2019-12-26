using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ServerUI;

namespace Classes
{
    class ClientConnection
    {
        private TcpClient _controlClient;
        private NetworkStream _controlStream;
        private StreamReader _controlReader;
        private StreamWriter _controlWriter;
        private Socket socket;
        private string _username;

        private string _currentDirectory = @"C:\server";
        private bool aux;

        private Server target;
        private FileManager manager;

        public ClientConnection(TcpClient client, Server f1)
        {
            target = f1;
            _controlClient = client;
            socket = _controlClient.Client;
            _controlStream = _controlClient.GetStream();
            _controlReader = new StreamReader(_controlStream);
            _controlWriter = new StreamWriter(_controlStream);
            manager = new FileManager();
        }

        public void HandleClient(Object obj)
        {
            //_controlWriter.WriteLine("220 Service Ready.");
            //_controlWriter.Flush();
            string line;
            try
            {
                while (!string.IsNullOrEmpty(line = _controlReader.ReadLine()))
                {
                    string response = null;

                    string[] command = line.Split(' ');

                    string cmd = command[0].ToUpperInvariant();
                    string arguments = command.Length > 1 ? line.Substring(command[0].Length + 1) : null;

                    if (string.IsNullOrWhiteSpace(arguments))
                        arguments = null;

                    if (response == null)
                    {
                        switch (cmd)
                        {
                            case "USER":
                                User(arguments);
                                break;
                            case "PASS":
                                response = Password(arguments);
                                break;
                            case "CWD":
                                response = ChangeWorkingDirectory(arguments);
                                break;
                            case "CDUP":
                                response = ChangeWorkingDirectory("..");
                                break;
                            case "PWD":
                                response = "257 \"/\" is current directory.";
                                break;
                            case "QUIT":
                                response = "Closing connection...";
                                Close();
                                break;
                            case "LIST-F":
                                List();
                                break;
                            case "DOWN":
                                //Console.WriteLine("arguments: " + arguments);
                                Download(arguments);
                                break;
                            case "UP":
                                Upload();
                                break;
                            case "CRT":
                                aux = CreateFolder(arguments);
                                if (aux)
                                {
                                    response = "Created Successfully!";
                                }
                                else
                                {
                                    response = "Already Exists!";
                                }
                                break;
                            case "LIST-D":
                                ListDirectories();
                                break;
                            case "DEL-D":
                                aux = Delete(arguments);
                                if (aux)
                                {
                                    response = "Deleted Successfully!";
                                }
                                else
                                {
                                    response = "Error!";
                                }
                                break;
                            case "DEL-F":
                                aux = DeleteFile(arguments);
                                if (aux)
                                {
                                    response = "Deleted Successfully!";
                                }
                                else
                                {
                                    response = "Error!";
                                }
                                break;
                            default:
                                response = "502 Command not implemented";
                                break;
                        }
                    }

                    if (_controlClient == null || !_controlClient.Connected)
                    {
                        break;
                    }
                    else if (!(String.IsNullOrEmpty(response)))
                    {
                        //Console.WriteLine("response: " + response);
                        _controlWriter.Write(response);
                        _controlWriter.Flush();
                    }

                }
            } catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #region FTP Commands

        private void User(string username)
        {
            _username = username;
            _currentDirectory = _currentDirectory + @"\" + username;
        }

        private string Password(string password)
        {
            if (true)
            {
                return "230 User logged in";
            }
            else
            {
                return "530 Not logged in";
            }
        }

        private string ChangeWorkingDirectory(string pathname)
        {
            return "250 Changed to new directory";
        }

        private void List()
        {
            string fullPath = _currentDirectory;
            Console.WriteLine("fullPath-list: "+fullPath);
            manager.List(fullPath, socket);
        }

        private void ListDirectories()
        {
            string fullPath = _currentDirectory;
            Console.WriteLine("fullPath-directories"+fullPath);
            manager.ListDirectories(fullPath, socket);
        }


        private void Close()
        {
            target.putText("User Disconnected...");
            target.takeCount(false);
            _controlClient.Close();
        }

        private void Download(string file)
        {
            
            manager.DownloadFromServer(socket, file,_currentDirectory);
        }

        private void Upload()
        {
            manager.UploadToServer(socket,_currentDirectory);
        }

        private bool CreateFolder(string name)
        {
            bool check = manager.CreateFolder(name,_currentDirectory);
            return check;
            
        }

        private bool Delete(string arguments)
        {
            bool check = manager.Delete(arguments,_currentDirectory,true);
            return check;
        }

        private bool DeleteFile(string arguments)
        {
            bool check = manager.Delete(arguments, _currentDirectory,false);
            return check;
        }

        #endregion
    }
}
