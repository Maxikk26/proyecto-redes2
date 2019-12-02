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

        private string _currentDirectory = @"C:\server";

        private string _username;

        private Server target;
        private FileManager manager;

        public ClientConnection(TcpClient client, Server f1)
        {
            target = f1;
            _controlClient = client;
            _controlStream = _controlClient.GetStream();
            _controlReader = new StreamReader(_controlStream);
            _controlWriter = new StreamWriter(_controlStream);
            manager = new FileManager();
        }

        public void HandleClient(Object obj)
        {
            _controlWriter.WriteLine("220 Service Ready.");
            _controlWriter.Flush();
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
                                response = User(arguments);
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
                            case "LIST":
                                response = List(arguments);
                                break;
                            case "DOWN":
                                Console.WriteLine("Entro");
                                Download(arguments);
                                response = "221";
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
                    else
                    {
                        _controlWriter.WriteLine(response);
                        _controlWriter.Flush();

                        if (response.StartsWith("221"))
                        {
                            break;
                        }
                    }
                }
            }catch(FileNotFoundException ex)
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

        private string User(string username)
        {
            _username = username;

            return "331 Username ok, need password";
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

        private string List(string pathname)
        {
            if (pathname == null)
            {
                pathname = string.Empty;
            }
            pathname = new DirectoryInfo(Path.Combine(_currentDirectory, pathname)).FullName;
            return pathname;
        }

        private void Close()
        {
            target.putText("User Disconnected...");
            target.takeCount(false);
            _controlClient.Close();
        }

        private void Download(string file)
        {
            Console.WriteLine("Funcion Download");
            manager.Download(_controlClient,file);
            Console.WriteLine("termino FileManager");
        }

        #endregion
    }
}
