using Classes;
using ServerPruebaUI.Classes.ControlUsuarios;
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
        public DataGridView dtgvUsers;
        public ControlUsuarios controlUsuarios;
        public FileManager file;

        public AddUser(ControlUsuarios controlUsuarios, FileManager file, DataGridView dtgvUsers)
        {
            InitializeComponent();
            this.dtgvUsers = dtgvUsers;
            this.controlUsuarios = controlUsuarios;
            this.file = file;
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
            
            if(controlUsuarios.crearUsuario(txtLogin.Text, txtClave.Text, txtNombre.Text, txtApellido.Text))
            {
                file.CreateFolder(txtLogin.Text, @"C:\server");
                controlUsuarios.salvarUsuarios();

                int n = dtgvUsers.Rows.Add();

                dtgvUsers.Rows[n].Cells[0].Value = txtLogin.Text;
                dtgvUsers.Rows[n].Cells[1].Value = txtClave.Text;
                dtgvUsers.Rows[n].Cells[2].Value = txtNombre.Text;
                dtgvUsers.Rows[n].Cells[3].Value = txtApellido.Text;
            }
            
        }
    }
}
