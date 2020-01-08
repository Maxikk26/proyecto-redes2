using Classes;
using ServerPruebaUI.Classes.ControlUsuarios;
using ServerPruebaUI.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ServerPruebaUI
{
    public partial class AdminUsers : Form
    {
        public ControlUsuarios controlUsuarios;
        public FileManager file;

        public AdminUsers(ControlUsuarios controlUsuarios, FileManager file)
        {
            InitializeComponent();
            this.controlUsuarios = controlUsuarios;
            this.file = file;
            cargarUsuarios();
        }

        public void cargarUsuarios()
        {
            if (controlUsuarios.usuarios != null)
                foreach (Usuario u in controlUsuarios.usuarios)
                {
                    int n = dtgvUsers.Rows.Add();

                    dtgvUsers.Rows[n].Cells[0].Value = u.login;
                    dtgvUsers.Rows[n].Cells[1].Value = u.clave;
                    dtgvUsers.Rows[n].Cells[2].Value = u.nombre;
                    dtgvUsers.Rows[n].Cells[3].Value = u.apellido;
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminUsers_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            btnDelUser.Enabled = true;
            AddUser addUser = new AddUser(controlUsuarios, file, dtgvUsers);
            addUser.Show();
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            List<int> rowsToDelete = new List<int>();

            foreach (DataGridViewCell cell in this.dtgvUsers.SelectedCells)
            {
                if (rowsToDelete.Contains(cell.RowIndex) == false && dtgvUsers.RowCount > 1)
                {
                  
                    rowsToDelete.Add(cell.RowIndex);

                    controlUsuarios.eliminarUsuario(dtgvUsers.Rows[cell.RowIndex].Cells[0].Value.ToString());
                    controlUsuarios.salvarUsuarios();
                    file.Delete(dtgvUsers.Rows[cell.RowIndex].Cells[0].Value.ToString(), @"C:\server", true);                    
                }
            }
            rowsToDelete = rowsToDelete.OrderByDescending(rowIndex => rowIndex).ToList();
            foreach (Int32 rowIndex in rowsToDelete)
            {
                if (dtgvUsers.RowCount == 1)
                    btnDelUser.Enabled = false;
                else
                {
                    
                    this.dtgvUsers.Rows.RemoveAt(rowIndex);
                }
            }

            rowsToDelete.Clear();
        }

        private void btnModUser_Click(object sender, EventArgs e)
        {
            ModUser modUser = new ModUser(controlUsuarios, dtgvUsers, this, file);
            modUser.Show();
        }
    }
}
