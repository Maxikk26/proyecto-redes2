using Classes;
using ServerPruebaUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerPruebaUI
{
    public partial class AdminUsers : Form
    {
        public FtpServer ftp;

        public AdminUsers(FtpServer ftp)
        {
            InitializeComponent();
            this.ftp = ftp;

            ftp.cargarUsuarios(this);
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
            AddUser addUser = new AddUser(ftp ,dtgvUsers);
            addUser.Show();
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            List<int> rowsToDelete = new List<int>();

            foreach (DataGridViewCell cell in this.dtgvUsers.SelectedCells)
            {
                if (rowsToDelete.Contains(cell.RowIndex) == false)
                {
                  
                    rowsToDelete.Add(cell.RowIndex);

                    ftp.eliminarUser(dtgvUsers.Rows[cell.RowIndex].Cells[0].Value.ToString());
                    
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
    }
}
