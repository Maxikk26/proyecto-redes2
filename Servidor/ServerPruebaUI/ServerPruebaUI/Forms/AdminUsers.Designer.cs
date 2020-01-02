namespace ServerPruebaUI
{
    partial class AdminUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminUsers));
            this.dtgvUsers = new System.Windows.Forms.DataGridView();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.controlUsuariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnModUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlUsuariosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvUsers
            // 
            this.dtgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.login,
            this.clave,
            this.nombre,
            this.apellido});
            this.dtgvUsers.Location = new System.Drawing.Point(25, 99);
            this.dtgvUsers.Name = "dtgvUsers";
            this.dtgvUsers.ReadOnly = true;
            this.dtgvUsers.Size = new System.Drawing.Size(442, 337);
            this.dtgvUsers.TabIndex = 0;
            this.dtgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // login
            // 
            this.login.HeaderText = "Login";
            this.login.Name = "login";
            this.login.ReadOnly = true;
            // 
            // clave
            // 
            this.clave.HeaderText = "Clave";
            this.clave.Name = "clave";
            this.clave.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // apellido
            // 
            this.apellido.HeaderText = "Apellido";
            this.apellido.Name = "apellido";
            this.apellido.ReadOnly = true;
            // 
            // btnDelUser
            // 
            this.btnDelUser.AutoSize = true;
            this.btnDelUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDelUser.Image")));
            this.btnDelUser.Location = new System.Drawing.Point(298, 12);
            this.btnDelUser.Name = "btnDelUser";
            this.btnDelUser.Size = new System.Drawing.Size(75, 70);
            this.btnDelUser.TabIndex = 1;
            this.btnDelUser.UseVisualStyleBackColor = true;
            this.btnDelUser.Click += new System.EventHandler(this.btnDelUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.AutoSize = true;
            this.btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            this.btnAddUser.Location = new System.Drawing.Point(392, 12);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 70);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // controlUsuariosBindingSource
            // 
            this.controlUsuariosBindingSource.DataSource = typeof(ServerPruebaUI.Classes.ControlUsuarios.ControlUsuarios);
            // 
            // btnModUser
            // 
            this.btnModUser.AutoSize = true;
            this.btnModUser.Image = ((System.Drawing.Image)(resources.GetObject("btnModUser.Image")));
            this.btnModUser.Location = new System.Drawing.Point(206, 12);
            this.btnModUser.Name = "btnModUser";
            this.btnModUser.Size = new System.Drawing.Size(75, 70);
            this.btnModUser.TabIndex = 3;
            this.btnModUser.UseVisualStyleBackColor = true;
            this.btnModUser.Click += new System.EventHandler(this.btnModUser_Click);
            // 
            // AdminUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 457);
            this.Controls.Add(this.btnModUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnDelUser);
            this.Controls.Add(this.dtgvUsers);
            this.Name = "AdminUsers";
            this.Text = "AdminUsers";
            this.Load += new System.EventHandler(this.AdminUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlUsuariosBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource controlUsuariosBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido;
        private System.Windows.Forms.Button btnDelUser;
        private System.Windows.Forms.Button btnAddUser;
        public System.Windows.Forms.DataGridView dtgvUsers;
        private System.Windows.Forms.Button btnModUser;
    }
}