using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        private FolderNameForm form;

        public MainForm(backend.FtpClient ftpClient)
        {
            ftp = ftpClient;
            ListFiles();
            Thread.Sleep(1500);
            ListFolders();
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
            //Console.WriteLine("Lista: " + list);
            int count=0;
            foreach(char c in list)
            {
                if(c == '.')
                {
                    count++;
                }
            }
            Console.WriteLine(count);

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
                    Console.WriteLine("text: "+text);
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
        private void DynamicButton_Click(object sender, EventArgs e, Button button)
        {
            Console.WriteLine("button.Name: "+button.Name);
            OptionForm form = new OptionForm(button.Name, ftp,this);
            form.ShowDialog();
            //RefreshButtons();
        }

        private void ListFolders()
        {
            string list = ftp.ListDirectories();
            int count = 0;
            int count2 = 0;
            int top = 50;
            int left = 200;
            string aux = "";
            foreach(char d in list)
            {
                if (d == '\\')
                {
                    count++;
                }
                else if(d == ',')
                {
                    break;
                }
            }
            foreach(char d in list)
            {
                if(count2 == count)
                {
                    if (d == ',')
                    {
                        count2 = 0;
                        Button button = new Button();
                        button.Text = aux;
                        button.Name = aux;
                        button.Left = left;
                        button.Top = top;
                        //button.Click += (sender2, e2) => DynamicButton_Click(sender2, e2, button);
                        this.Controls.Add(button);
                        //buttonsAdded.Insert(0, button);
                        top += button.Height + 2;
                        aux = "";
                    }
                    else
                    {
                        aux = aux + d;
                    }
                }
                else if(d == '\\')
                {
                    count2++;
                }
            }
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
            RefreshButtons();
            
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        public void RefreshButtons()
        {
            foreach (Button item in buttonsAdded)
            {
                if (!item.Name.Contains("btn"))
                {
                    Controls.Remove(item);
                }
            }
            ListFiles();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            form = new FolderNameForm(ftp);
            form.ShowDialog();
            //string response = ftp.CreateFolder("Prueba");
        }
    }

    

    
}
