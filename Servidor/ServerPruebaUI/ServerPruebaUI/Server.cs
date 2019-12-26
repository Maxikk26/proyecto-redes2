using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace ServerUI
{
    public partial class Server : Form
    {
        private int count = 0;
        private Classes.FtpServer ftp;
        private Classes.FileManager file;

        private string path = @"C:\server";
        
        public Server()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            ftp = new Classes.FtpServer(this);
            
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
                button1.Enabled = false;
                btnStop.Enabled = true;
                putText("Starting server on port "+port);
                file = new Classes.FileManager();
                bool directory = file.rootDirectory();
                if (directory)
                    putText(path + " exists!");
                else
                    putText(path + " created!");

            }
            catch (FormatException Ex)
            {
                MessageBox.Show(Ex.Message, "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                btnStop.Enabled = false;

            }
            catch (OverflowException Ex)
            {
                MessageBox.Show(Ex.Message, "OverflowException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                btnStop.Enabled = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = true;
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

        public void takeCount(Boolean x)
        {
            if (x)
            {
                count++;
                putText("Users connected: "+count.ToString());
            }
            else
            {
                count--;
                putText("Users connected: " + count.ToString());
                

            }
        }

        public void showError(string err,string exc)
        {
            MessageBox.Show("Error", exc, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       
    }
}

