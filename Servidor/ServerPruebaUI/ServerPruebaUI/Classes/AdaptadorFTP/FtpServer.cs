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
        private TcpListener _listener;
        private Server target;
        private FileManager file;

        private ControlUsuarios controlUsuarios;

        public FtpServer(Server f1)
        {
            target = f1;
            AdaptadorTXT adaptadorTXT = new AdaptadorTXT(@"C:\server\usuarios.txt");
            controlUsuarios = new ControlUsuarios(adaptadorTXT);
            file = new FileManager();
         /*   controlUsuarios.crearUsuario("eze", "123", "ezequiel", "montero");
            controlUsuarios.crearUsuario("max", "1234", "maxi", "bogo");
            controlUsuarios.crearUsuario("rob", "1235", "rober", "car");
            controlUsuarios.crearUsuario("rei", "1234", "reinel", "arte");
            controlUsuarios.eliminarUsuario("eze");
            controlUsuarios.motrarUsuarios();//muestra en consola
            controlUsuarios.salvarUsuarios();//importante para salvar en txt*/
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

        public bool createUser(string login, string clave, string nombre, string apellido)
        {
            if (controlUsuarios.crearUsuario(login, clave, nombre, apellido))
            {
                file.CreateFolder(login, @"C:\server");
                controlUsuarios.salvarUsuarios();
                return true;
            }
            else
                return false;
        }

        public void eliminarUser(string login)
        {
            controlUsuarios.eliminarUsuario(login);
            controlUsuarios.salvarUsuarios();
            file.Delete(login, @"C:\server", true);
        }

        public void cargarUsuarios(AdminUsers adminUsers)
        {
            if(controlUsuarios.usuarios != null)
                foreach(Usuario u in controlUsuarios.usuarios)
                {
   

                    int n = adminUsers.dtgvUsers.Rows.Add();

                    adminUsers.dtgvUsers.Rows[n].Cells[0].Value = u.login;
                    adminUsers.dtgvUsers.Rows[n].Cells[1].Value = u.clave;
                    adminUsers.dtgvUsers.Rows[n].Cells[2].Value = u.nombre;
                    adminUsers.dtgvUsers.Rows[n].Cells[3].Value = u.apellido;
                }
        }
    }
}
