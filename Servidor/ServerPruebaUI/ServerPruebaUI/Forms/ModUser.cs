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
    public partial class ModUser : Form
    {
        public ControlUsuarios controlUsuarios;
        public DataGridView dtgvUsers;
        public AdminUsers adminUsers;
        public FileManager file;
        public ModUser(ControlUsuarios controlUsuarios, DataGridView dtgvUsers, AdminUsers adminUsers, FileManager file)
        {
            InitializeComponent();
            this.controlUsuarios = controlUsuarios;
            this.dtgvUsers = dtgvUsers;
            this.adminUsers = adminUsers;
            this.file = file;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogin.Text))
            {
                if (controlUsuarios.confirmarUsuario(txtLogin.Text)) {
                    Usuario usuario = controlUsuarios.buscarUsuario(txtLogin.Text);
                    if (!string.IsNullOrEmpty(txtNewLogin.Text))
                    {
                        usuario.login = txtNewLogin.Text;
                        file.Rename(@"C:\server\" + txtLogin.Text, @"C:\server\" + txtNewLogin.Text);
                    }
                    if (!string.IsNullOrEmpty(txtClave.Text))
                    {
                        usuario.clave = txtClave.Text;
                    }
                    if (!string.IsNullOrEmpty(txtNombre.Text))
                    {
                        usuario.nombre = txtNombre.Text;
                    }
                    if (!string.IsNullOrEmpty(txtApellido.Text))
                    {
                        usuario.apellido = txtApellido.Text;
                    }

                    controlUsuarios.salvarUsuarios();

                    dtgvUsers.Rows.Clear();
                    dtgvUsers.Refresh();
                    adminUsers.cargarUsuarios();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
