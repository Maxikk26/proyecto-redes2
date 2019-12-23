using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private backend.FileManager manager;
        private List<Button> buttonsAdded = new List<Button>();

        public MainForm(backend.FtpClient ftpClient)
        {
            ftp = ftpClient;
            ListFiles();
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

        private void ListFiles()
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
                else
                {
                    string[] aux2 = aux.Split('.');
                    string text = aux2[0];
                    Button button = new Button();
                    button.Text = text;
                    button.Name = aux;
                    button.Left = left;
                    button.Top = top;
                    button.Click += (sender2, e2) => DynamicButton_Click(sender2,e2,button);
                    this.Controls.Add(button);
                    buttonsAdded.Insert(0, button);
                    top += button.Height + 2;
                    aux = "";
                }
                    
            }
               
        }
        [STAThread]
        private void DynamicButton_Click(object sender, EventArgs e, Button button)
        {
            Console.WriteLine("button.Name: "+button.Name);
            OptionForm form = new OptionForm(button.Name);
            form.ShowDialog();
            //ftp.Download(button.Name);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string fileName = openFileDialog1.SafeFileName;
                    ftp.Upload(file, fileName);
                }
                catch (IOException)
                {
                }
            }
            if (buttonsAdded.Count > 0)
            {
                foreach(Control item in Controls.OfType<Button>())
                {
                    if(!item.Name.Contains("btn"))
                    {
                        Controls.Remove(item);
                    }
                }
                ListFiles();
            }
            
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }
    }

    
}
