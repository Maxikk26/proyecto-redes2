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

        public ControlUsuarios controlUsuarios;

        public FtpServer(Server f1, ControlUsuarios controlUsuarios, FileManager file)
        {
            target = f1;
            this.controlUsuarios = controlUsuarios;
            this.file = file;
        }

        public void Start(String ip, Int32 port)
        {
            IPAddress IP = IPAddress.Parse(ip);
            _listener = new TcpListener(IP, port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
        }

        public void Stop()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
            TcpClient client = _listener.EndAcceptTcpClient(result);
            EndPoint ep = client.Client.RemoteEndPoint;
            target.putText("Incoming Connection..." + ep.ToString());
            target.takeCount(true);
            ClientConnection connection = new ClientConnection(client,target,controlUsuarios);
            ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
        }
    }
}
