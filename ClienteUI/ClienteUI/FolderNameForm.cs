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
    public partial class FolderNameForm : Form
    {
        private backend.FtpClient ftp;

        public FolderNameForm(backend.FtpClient ftpClient)
        {
            ftp = ftpClient;
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string response = ftp.CreateFolder(name);
            MessageBox.Show(response, "Create Folder", MessageBoxButtons.OK);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
