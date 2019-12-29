using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerPruebaUI.Forms
{
    public partial class AddUser : Form
    {
        DataGridView dtgvUsers;
        FtpServer ftp;
        public AddUser(FtpServer ftp,DataGridView dtgvUsers)
        {
            InitializeComponent();
            this.dtgvUsers = dtgvUsers;
            this.ftp = ftp;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            
            if(ftp.createUser(txtLogin.Text, txtClave.Text, txtNombre.Text, txtApellido.Text))
            {
                int n = dtgvUsers.Rows.Add();

                dtgvUsers.Rows[n].Cells[0].Value = txtLogin.Text;
                dtgvUsers.Rows[n].Cells[1].Value = txtClave.Text;
                dtgvUsers.Rows[n].Cells[2].Value = txtNombre.Text;
                dtgvUsers.Rows[n].Cells[3].Value = txtApellido.Text;
            }
            
        }
    }
}
