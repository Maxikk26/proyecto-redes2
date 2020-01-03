using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ServerUI;
using ServerPruebaUI.Classes.ControlUsuarios;
using ServerPruebaUI;

namespace Classes
{
    public class FtpServer
    {
        public TcpListener _listener;
        public Server target;
        public FileManager file;

        public TcpClient client;

        public ControlUsuarios controlUsuarios;

        ClientConnection connection;

        public bool b1 = true;

        public FtpServer(Server f1, ControlUsuarios controlUsuarios, FileManager file)
        {
            target = f1;
            this.controlUsuarios = controlUsuarios;
            this.file = file;
        }

        public void Start(String ip, Int32 port)
        {
            b1 = true;
            IPAddress IP = IPAddress.Parse(ip);
            _listener = new TcpListener(IP, port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
        }

        public void Stop()
        {
          
            if (_listener != null)
            {
                Console.WriteLine("STOPPPP");
                b1 = false;
                _listener.Stop();
            }
            else
                Console.WriteLine("NOT STOPPP");
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {


            if (b1)
            {
                _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
                client = _listener.EndAcceptTcpClient(result);
                EndPoint ep = client.Client.RemoteEndPoint;
                target.putText("Incoming Connection..." + ep.ToString());
                target.takeCount(true);
                connection = new ClientConnection(client, target, controlUsuarios);
                ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
            }
            
            
            
        }
    }
}
