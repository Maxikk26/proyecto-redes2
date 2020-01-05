using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class OptionFormFolder : Form
    {
        private backend.FtpClient ftp;
        private string folderName;
        private string path = @"C:\server\";
        string response;
        private MainForm main;
        private RenameForm form;

        public OptionFormFolder(string folder, backend.FtpClient ftpClient, MainForm form, string _path)
        {
            ftp = ftpClient;
            folderName = folder;
            main = form;
            path = path + _path;
            InitializeComponent();
        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Access Click");
            string response = ftp.AccessDirectory(folderName);
            string[] split = response.Split('!');
            response = split[0];
            if(response == "Changed Succesfully")
            {
                main.RefreshButtons();
                main.EnableReturn(true);
            }
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            response  = ftp.DeleteFolder(folderName,path);
            MessageBox.Show(response, "Delete", MessageBoxButtons.OK);
            main.RefreshButtons();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            form = new RenameForm("", folderName, ftp,true);
            form.ShowDialog();
            main.RefreshButtons();
            this.Close();
        }
    }
}
