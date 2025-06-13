namespace Application_GUI.src.View
{
    partial class UpdateTaskStatusForm
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
            lblIdTask = new Label();
            txtIdTask = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnSimpan = new Button();
            btnKembali = new Button();
            SuspendLayout();
            // 
            // lblIdTask
            // 
            lblIdTask.AutoSize = true;
            lblIdTask.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIdTask.Location = new Point(28, 38);
            lblIdTask.Name = "lblIdTask";
            lblIdTask.Size = new Size(71, 18);
            lblIdTask.TabIndex = 0;
            lblIdTask.Text = "ID Tugas:";
            // 
            // txtIdTask
            // 
            txtIdTask.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIdTask.Location = new Point(125, 34);
            txtIdTask.Margin = new Padding(3, 4, 3, 4);
            txtIdTask.Name = "txtIdTask";
            txtIdTask.Size = new Size(240, 24);
            txtIdTask.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(28, 88);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(54, 18);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(125, 84);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(240, 26);
            cmbStatus.TabIndex = 3;
            // 
            // btnSimpan
            // 
            btnSimpan.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSimpan.Location = new Point(265, 150);
            btnSimpan.Margin = new Padding(3, 4, 3, 4);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(100, 44);
            btnSimpan.TabIndex = 4;
            btnSimpan.Text = "Lanjut";
            btnSimpan.UseVisualStyleBackColor = true;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // btnKembali
            // 
            btnKembali.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnKembali.Location = new Point(159, 150);
            btnKembali.Margin = new Padding(3, 4, 3, 4);
            btnKembali.Name = "btnKembali";
            btnKembali.Size = new Size(100, 44);
            btnKembali.TabIndex = 5;
            btnKembali.Text = "Kembali";
            btnKembali.UseVisualStyleBackColor = true;
            btnKembali.Click += btnKembali_Click;
            // 
            // UpdateTaskStatusForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 226);
            Controls.Add(btnKembali);
            Controls.Add(btnSimpan);
            Controls.Add(cmbStatus);
            Controls.Add(lblStatus);
            Controls.Add(txtIdTask);
            Controls.Add(lblIdTask);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UpdateTaskStatusForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Perbarui Status Tugas";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdTask;
        private System.Windows.Forms.TextBox txtIdTask;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnKembali;
    }
}