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
            string list = ftp.List();
            int count=0;
            foreach(char c in list)
            {
                if(c == '.')
                {
                    count++;
                }
            }
            Console.WriteLine(count);

            List<Button> buttons = new List<Button>();
            int top = 50;
            int left = 100;
            string aux = "";
            foreach (char d in list)
            {
                if (d != ',')
                {
                    aux = aux + d;
                }
                else if (d == ',')
                {
                    string[] aux2 = aux.Split('.');
                    string text = aux2[0];
                    Button button = new Button();
                    button.Text = text;
                    button.Name = text;
                    button.Left = left;
                    button.Top = top;
                    button.Click += (sender2, e2) => DynamicButton_Click(sender2,e2,button);
                    this.Controls.Add(button);
                    top += button.Height + 2;
                    aux = "";
                }
                    
            }
               
        }

        private void DynamicButton_Click(object sender, EventArgs e, Button button)

        {
           

            MessageBox.Show("Dynamic button is clicked "+button.Name);

        }
    }

    
}
