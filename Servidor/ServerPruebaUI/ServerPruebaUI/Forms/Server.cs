using System;
using System.Windows.Forms;
using ServerPruebaUI;
using Classes;
using ServerPruebaUI.Classes.ControlUsuarios;

namespace ServerUI
{
    public partial class Server : Form
    {
        private int count = 0;
        private FtpServer ftp;
        private FileManager file;
        private ControlUsuarios controlUsuarios;

        private string path = @"C:\server";
        
        public Server()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            AdaptadorTXT adaptadorTXT = new AdaptadorTXT(@"C:\server\usuarios.txt");
            controlUsuarios = new ControlUsuarios(adaptadorTXT);
            file = new FileManager();
            ftp = new FtpServer(this, controlUsuarios, file);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            

            string txtip = txtIp.Text;
            string txtport = txtPort.Text;
            Int32 port = 0;
            try
            {

                port = System.Convert.ToInt32(txtport);
                ftp.Start(txtip,port);
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                putText("Starting server on port "+port);
                file = new Classes.FileManager();
                bool directory = file.rootDirectory();
                if (directory)
                    putText(path + " exists!");
                else
                    putText(path + " created!");

                btnUsers.Enabled = false;
            }
            catch (FormatException Ex)
            {
                MessageBox.Show(Ex.Message, "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                btnStop.Enabled = false;

            }
            catch (OverflowException Ex)
            {
                MessageBox.Show(Ex.Message, "OverflowException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                btnStop.Enabled = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnUsers.Enabled = true;
            btnStart.Enabled = true;

            btnStop.Enabled = false;
            putText("Stopping Server...");
            ftp.Stop();
          
        }

        public void putText(string str)
        {
            txtboxCmd.AppendText(str+"\r\n");
        }
        
        private void txtboxCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
            else
            {
                e.SuppressKeyPress = true;
            }
        }


        public void showError(string err,string exc)
        {
            MessageBox.Show("Error", exc, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AdminUsers adminUsers = new AdminUsers(controlUsuarios,file);
            adminUsers.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AdminUsers adminUsers = new AdminUsers(controlUsuarios,file);
            adminUsers.Show();
        }
    }
}

