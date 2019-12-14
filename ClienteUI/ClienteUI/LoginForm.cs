using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class LoginForm : Form
    {
        private MainForm main;
        private backend.FtpClient ftpClient;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool check = true;
            foreach(Control c in this.Controls)
            {
                if(c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("Por favor ingrese datos en todas las casillas","Casilla vacia" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        check = false;
                        break;

                    }

                }
            }
            if (check)
            {
                try
                {
                    if (txtIp.Text.Length < 7)
                    {
                        MessageBox.Show("La Direccion IP no es valida", "Direccion IP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        Int32 port = Convert.ToInt32(txtPort.Text);
                        IPAddress address = IPAddress.Parse(txtIp.Text);
                        string ip = txtIp.Text;
                        string user = txtUser.Text;
                        string pass = txtPass.Text;
                        ftpClient = new backend.FtpClient(ip,port);
                        ftpClient.Connect();
                        main = new MainForm(ftpClient);
                        main.Show();
                        main.FormClosed += new FormClosedEventHandler(LoginForm_FormClosed);
                        this.Hide();
                    }
                    
                    
                }
                catch (FormatException Ex)
                {
                    MessageBox.Show(Ex.Message, "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ArgumentNullException Ex)
                {
                    MessageBox.Show(Ex.Message, "ArgumentNullException", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            

        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
