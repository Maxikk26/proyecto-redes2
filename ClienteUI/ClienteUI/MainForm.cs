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
        private List<Button> buttonsAdded = new List<Button>();
        private FolderNameForm form;
        private string path;
        private int count = 0;

        public MainForm(backend.FtpClient ftpClient,string _directory, bool check)
        {
            ftp = ftpClient;
            path = _directory;
            ListFiles();
            Thread.Sleep(500);
            ListFolders();
            InitializeComponent();
            EnableReturn(false);
        }

        public void EnableReturn(bool check)
        {
            if (check)
            {
                btnReturn.Enabled = true;
                count++;
            }
            else
                btnReturn.Enabled = false;

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
            if(!(list == "empty"))
            {
                int count = 0;
                foreach (char c in list)
                {
                    if (c == '.')
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
                        Console.WriteLine("text: " + text);
                        Button button = new Button();
                        button.Text = text;
                        button.Name = aux;
                        button.Left = left;
                        button.Top = top;
                        button.Click += (sender2, e2) => File_Click(sender2, e2, button);
                        this.Controls.Add(button);
                        buttonsAdded.Insert(0, button);
                        top += button.Height + 2;
                        aux = "";
                    }

                }
            }
            
               
        }
        private void File_Click(object sender, EventArgs e, Button button)
        {
            Console.WriteLine("button.Name: "+button.Name);
            OptionFormFile form = new OptionFormFile(button.Name,ftp,this,path);
            form.ShowDialog();
        }

        private void ListFolders()
        {
            string list = ftp.ListDirectories();
            Console.WriteLine("list: " + list);
            if (!(list == "empty"))
            {
                int count = 0;
                int count2 = 0;
                int top = 50;
                int left = 200;
                string aux = "";
                foreach (char d in list)
                {
                    if (d == '\\')
                    {
                        count++;
                    }
                    else if (d == ',')
                    {
                        break;
                    }
                }
                foreach (char d in list)
                {
                    if (count2 == count)
                    {
                        if (d == ',')
                        {
                            count2 = 0;
                            Button button = new Button();
                            button.Text = aux;
                            button.Name = aux;
                            button.Left = left;
                            button.Top = top;
                            button.Click += (sender2, e2) => Folder_Click(sender2, e2, button);
                            this.Controls.Add(button);
                            buttonsAdded.Insert(0, button);
                            top += button.Height + 2;
                            aux = "";
                            count2 = 0;
                        }
                        else
                        {
                            aux = aux + d;
                        }
                    }
                    else if (d == '\\')
                    {
                        count2++;
                    }
                }
            }
            
        }

        private void Folder_Click(object sender, EventArgs e, Button button)
        {
            Console.WriteLine("button.Name: " + button.Name);
            OptionFormFolder form = new OptionFormFolder(button.Name, ftp, this,path);
            form.ShowDialog();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string fileName = openFileDialog1.SafeFileName;
                    ftp.Upload(file, fileName, path);
                }
                catch (IOException)
                {
                }
            }
            RefreshButtons();
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
            ListFolders();           
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            form = new FolderNameForm(ftp);
            form.ShowDialog();
            RefreshButtons();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ftp.ReturnDirectory();
            count--;
            if (count == 0)
            {
                EnableReturn(false);
            }
            RefreshButtons();
        }
    }

    

    
}
