using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class OptionFormFile : Form
    {
        private backend.FtpClient ftp;
        private string fileName;
        private string path = @"C:\server\";
        private MainForm main;
        private string response;
        public OptionFormFile(string file, backend.FtpClient ftpClient, MainForm form, string _path)
        {
            ftp = ftpClient;
            fileName = file;
            main = form;
            path = path + _path;
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            ftp.Download(fileName,path);
            MessageBox.Show("Downloaded succesfuly!", "Download", MessageBoxButtons.OK);
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            response = ftp.DeleteFile(fileName);
            main.RefreshButtons();
            MessageBox.Show("Deleted succesfuly!", "Download", MessageBoxButtons.OK);
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
