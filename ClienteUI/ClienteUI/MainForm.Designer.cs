namespace ClienteUI
{
    partial class MainForm
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
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(533, 38);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(105, 31);
            this.btnDisconnect.TabIndex = 0;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(533, 84);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(105, 31);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(533, 131);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(105, 31);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create Folder";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(533, 177);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(105, 31);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 346);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDisconnect);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnReturn;
    }
}