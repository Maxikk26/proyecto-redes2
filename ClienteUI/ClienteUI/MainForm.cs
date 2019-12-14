using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class MainForm : Form
    {
        private LoginForm login;
        private backend.FtpClient ftp;
        public MainForm(backend.FtpClient ftpClient)
        {
            ftp = ftpClient;
            InitializeComponent();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            login = new LoginForm();
            login.Show();
            login.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            this.Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ftp.List();
        }


    }

    
}
