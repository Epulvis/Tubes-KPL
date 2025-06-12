namespace Application_GUI.src.View
{
    partial class CreateTaskForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblJudul = new System.Windows.Forms.Label();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblKategori = new System.Windows.Forms.Label();
            this.txtJudul = new System.Windows.Forms.TextBox();
            this.dtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKembali = new System.Windows.Forms.Button();
            this.btnBersihkan = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();

            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblJudul, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDeadline, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblKategori, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtJudul, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDeadline, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbKategori, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelButtons, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(15);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 224);
            this.tableLayoutPanel1.TabIndex = 0;

            this.lblJudul.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblJudul.AutoSize = true;
            this.lblJudul.Location = new System.Drawing.Point(19, 27);
            this.lblJudul.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(95, 20);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "Judul Tugas:";

            this.lblDeadline.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDeadline.AutoSize = true;
            this.lblDeadline.Location = new System.Drawing.Point(19, 72);
            this.lblDeadline.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(75, 20);
            this.lblDeadline.TabIndex = 1;
            this.lblDeadline.Text = "Deadline:";
 
            this.lblKategori.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblKategori.AutoSize = true;
            this.lblKategori.Location = new System.Drawing.Point(19, 117);
            this.lblKategori.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(71, 20);
            this.lblKategori.TabIndex = 2;
            this.lblKategori.Text = "Kategori:";

            this.txtJudul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJudul.Location = new System.Drawing.Point(122, 24);
            this.txtJudul.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtJudul.Name = "txtJudul";
            this.txtJudul.Size = new System.Drawing.Size(437, 26);
            this.txtJudul.TabIndex = 3;

            this.dtpDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDeadline.Location = new System.Drawing.Point(122, 69);
            this.dtpDeadline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDeadline.Name = "dtpDeadline";
            this.dtpDeadline.Size = new System.Drawing.Size(437, 26);
            this.dtpDeadline.TabIndex = 4;

            this.cmbKategori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(122, 113);
            this.cmbKategori.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(437, 28);
            this.cmbKategori.TabIndex = 5;

            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanelButtons, 2);
            this.flowLayoutPanelButtons.Controls.Add(this.btnKembali);
            this.flowLayoutPanelButtons.Controls.Add(this.btnBersihkan);
            this.flowLayoutPanelButtons.Controls.Add(this.btnSimpan);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(18, 153);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(542, 53);
            this.flowLayoutPanelButtons.TabIndex = 6;

            this.btnKembali.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKembali.Location = new System.Drawing.Point(427, 8);
            this.btnKembali.Margin = new System.Windows.Forms.Padding(10, 5, 4, 5);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(111, 40);
            this.btnKembali.TabIndex = 2;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);

            this.btnBersihkan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBersihkan.Location = new System.Drawing.Point(292, 8);
            this.btnBersihkan.Margin = new System.Windows.Forms.Padding(10, 5, 4, 5);
            this.btnBersihkan.Name = "btnBersihkan";
            this.btnBersihkan.Size = new System.Drawing.Size(111, 40);
            this.btnBersihkan.TabIndex = 1;
            this.btnBersihkan.Text = "Bersihkan";
            this.btnBersihkan.UseVisualStyleBackColor = true;
            this.btnBersihkan.Click += new System.EventHandler(this.btnBersihkan_Click);

            this.btnSimpan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimpan.Location = new System.Drawing.Point(157, 8);
            this.btnSimpan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(111, 40);
            this.btnSimpan.TabIndex = 0;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 224);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tambah Tugas Baru";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Label lblKategori;
        private System.Windows.Forms.TextBox txtJudul;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBersihkan;
        private System.Windows.Forms.Button btnKembali;
    }

}