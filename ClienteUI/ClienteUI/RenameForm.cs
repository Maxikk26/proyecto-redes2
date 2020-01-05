using ClienteUI.backend;
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
    public partial class RenameForm : Form
    {
        private string extension;
        private string oldName;
        private FtpClient ftp;
        private bool check;

        public RenameForm(string extension,string oldName, FtpClient ftp,bool check)
        {
            this.extension = extension;
            this.oldName = oldName;
            this.ftp = ftp;
            this.check = check;
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (check)
            {
                //Console.WriteLine("nombre nuevo " + name);
                ftp.Rename(name, oldName);
                MessageBox.Show("Renamed Succesfully", "Rename", MessageBoxButtons.OK);
            }
            else
            {
                name += extension;
                //Console.WriteLine("nombre nuevo " + name);
                ftp.Rename(name, oldName);
                MessageBox.Show("Renamed Succesfully", "Rename", MessageBoxButtons.OK);
                this.Close();
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
