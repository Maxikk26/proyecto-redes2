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
using ServerPruebaUI.Classes.ControlUsuarios;

namespace Classes
{
    public class ClientConnection
    {
        public TcpClient _controlClient;
        public NetworkStream _controlStream;
        public StreamReader _controlReader;
        public StreamWriter _controlWriter;
        public Socket socket;
        public string _username;

        public string _currentDirectory = @"C:\server";
        public bool aux;
        public bool ren = false;
        public string oldName;

        public Server target;
        public FileManager manager;

        public ControlUsuarios controlUsuarios;


        public ClientConnection(TcpClient client, Server f1, ControlUsuarios controlUsuarios)
        {
            target = f1;
            _controlClient = client;
            socket = _controlClient.Client;
            _controlStream = _controlClient.GetStream();
            _controlReader = new StreamReader(_controlStream);
            _controlWriter = new StreamWriter(_controlStream);
            manager = new FileManager();

            this.controlUsuarios = controlUsuarios;
        }

        public void HandleClient(Object obj)
        {
            //_controlWriter.WriteLine("220 Service Ready.");
            //_controlWriter.Flush();
            string line = "";
          
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
                                case "DIR":
                                    response = AccessDirectory(arguments);
                                    break;
                                case "RET":
                                    ReturnDirectory();
                                    break;
                                case "REN":
                                    Rename(arguments);
                                    break;
                                case "MOV":
                                response = Move(arguments);
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
                
            }
            catch (FileNotFoundException ex)
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
       
        }

        private string Password(string password)
        {
            Console.WriteLine("este es mamawebo " + _username + " " + password);
            Console.WriteLine("SSScurrennn "+_currentDirectory);
            bool check = controlUsuarios.loginUsuario(_username, password);
            Console.WriteLine("bool check "+check);

            if (check)
            {
                _currentDirectory = _currentDirectory + @"\" + _username;
                Console.WriteLine("RRRRRcurrennn " + _currentDirectory);
                return "y!";
            }
            else
                return "n!";
        }

        private void Rename(string arguments)
        {
            aux = false;
            foreach(char d in arguments)
            {
                if (d == '.')
                    aux = true;
            }
            if (aux)
            {
                if (!ren)
                {
                    ren = true;
                    this.oldName = arguments;
                }
                else
                {
                    string directory = _currentDirectory + @"\" + oldName;
                    File.Move(directory, _currentDirectory + @"\" + arguments);
                    Console.WriteLine("rename " + directory);
                    ren = false;
                }
            }
            else
            {
                if (!ren)
                {
                    ren = true;
                    this.oldName = arguments;
                }
                else
                {
                    string directory = _currentDirectory + @"\" + oldName;
                    Directory.Move(directory, _currentDirectory + @"\" + arguments);
                    Console.WriteLine("rename " + directory);
                    ren = false;
                }
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

        private string AccessDirectory(string directory)
        {
            _currentDirectory = _currentDirectory + @"\" + directory;
            return "Changed Succesfully!";
        }

        private void ReturnDirectory()
        {
            int count = 0;
            int count2 = 0;
            string aux = "";
            foreach (char d in _currentDirectory)
            {
                if (d == '\\')
                {
                    count++;
                }
            }
            foreach (char d in _currentDirectory)
            {
                if (d == '\\')
                {
                    count2++;
                }
                if (count2 != count)
                {
                    aux += d;

                }
            }
            _currentDirectory = aux;
            Console.WriteLine("segunda vez: " + aux);
        }

        public string Move(string newPath)
        {
            try
            {
                string fileName = Path.GetFileName(newPath);
                File.Move(_currentDirectory + @"\" + fileName, newPath);

                return "moved succesfully!";
            }
            catch (Exception e)
            {
                return "error fatal!";
            }
        }

        #endregion
    }
}
