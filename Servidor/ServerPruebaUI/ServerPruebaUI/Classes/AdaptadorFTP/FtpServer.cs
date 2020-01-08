using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ServerUI;
using ServerPruebaUI.Classes.ControlUsuarios;

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
                b1 = false;
                _listener.Stop();
            }
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {


            if (b1)
            {
                _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
                client = _listener.EndAcceptTcpClient(result);
                EndPoint ep = client.Client.RemoteEndPoint;
                target.putText("Incoming Connection..." + ep.ToString());
                connection = new ClientConnection(client, target, controlUsuarios,ep);
                ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
            }
            
            
            
        }
    }
}
