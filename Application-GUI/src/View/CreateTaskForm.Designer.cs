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
            tableLayoutPanel1 = new TableLayoutPanel();
            lblJudul = new Label();
            lblDeadline = new Label();
            lblKategori = new Label();
            txtJudul = new TextBox();
            dtpDeadline = new DateTimePicker();
            cmbKategori = new ComboBox();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            btnKembali = new Button();
            btnBersihkan = new Button();
            btnSimpan = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblJudul, 0, 0);
            tableLayoutPanel1.Controls.Add(lblDeadline, 0, 1);
            tableLayoutPanel1.Controls.Add(lblKategori, 0, 2);
            tableLayoutPanel1.Controls.Add(txtJudul, 1, 0);
            tableLayoutPanel1.Controls.Add(dtpDeadline, 1, 1);
            tableLayoutPanel1.Controls.Add(cmbKategori, 1, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanelButtons, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(13, 15, 13, 15);
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(514, 224);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblJudul
            // 
            lblJudul.Anchor = AnchorStyles.Left;
            lblJudul.AutoSize = true;
            lblJudul.Location = new Point(17, 27);
            lblJudul.Margin = new Padding(4, 0, 4, 0);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(89, 20);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "Judul Tugas:";
            // 
            // lblDeadline
            // 
            lblDeadline.Anchor = AnchorStyles.Left;
            lblDeadline.AutoSize = true;
            lblDeadline.Location = new Point(17, 72);
            lblDeadline.Margin = new Padding(4, 0, 4, 0);
            lblDeadline.Name = "lblDeadline";
            lblDeadline.Size = new Size(72, 20);
            lblDeadline.TabIndex = 1;
            lblDeadline.Text = "Deadline:";
            // 
            // lblKategori
            // 
            lblKategori.Anchor = AnchorStyles.Left;
            lblKategori.AutoSize = true;
            lblKategori.Location = new Point(17, 117);
            lblKategori.Margin = new Padding(4, 0, 4, 0);
            lblKategori.Name = "lblKategori";
            lblKategori.Size = new Size(69, 20);
            lblKategori.TabIndex = 2;
            lblKategori.Text = "Kategori:";
            // 
            // txtJudul
            // 
            txtJudul.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtJudul.Location = new Point(114, 24);
            txtJudul.Margin = new Padding(4, 5, 4, 5);
            txtJudul.Name = "txtJudul";
            txtJudul.Size = new Size(383, 27);
            txtJudul.TabIndex = 3;
            // 
            // dtpDeadline
            // 
            dtpDeadline.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpDeadline.Location = new Point(114, 69);
            dtpDeadline.Margin = new Padding(4, 5, 4, 5);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new Size(383, 27);
            dtpDeadline.TabIndex = 4;
            // 
            // cmbKategori
            // 
            cmbKategori.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKategori.FormattingEnabled = true;
            cmbKategori.Location = new Point(114, 113);
            cmbKategori.Margin = new Padding(4, 5, 4, 5);
            cmbKategori.Name = "cmbKategori";
            cmbKategori.Size = new Size(383, 28);
            cmbKategori.TabIndex = 5;
            // 
            // flowLayoutPanelButtons
            // 
            tableLayoutPanel1.SetColumnSpan(flowLayoutPanelButtons, 2);
            flowLayoutPanelButtons.Controls.Add(btnKembali);
            flowLayoutPanelButtons.Controls.Add(btnBersihkan);
            flowLayoutPanelButtons.Controls.Add(btnSimpan);
            flowLayoutPanelButtons.Dock = DockStyle.Fill;
            flowLayoutPanelButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelButtons.Location = new Point(16, 153);
            flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            flowLayoutPanelButtons.Size = new Size(482, 53);
            flowLayoutPanelButtons.TabIndex = 6;
            // 
            // btnKembali
            // 
            btnKembali.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnKembali.Location = new Point(379, 5);
            btnKembali.Margin = new Padding(9, 5, 4, 5);
            btnKembali.Name = "btnKembali";
            btnKembali.Size = new Size(99, 40);
            btnKembali.TabIndex = 2;
            btnKembali.Text = "Kembali";
            btnKembali.UseVisualStyleBackColor = true;
            btnKembali.Click += btnKembali_Click;
            // 
            // btnBersihkan
            // 
            btnBersihkan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBersihkan.Location = new Point(267, 5);
            btnBersihkan.Margin = new Padding(9, 5, 4, 5);
            btnBersihkan.Name = "btnBersihkan";
            btnBersihkan.Size = new Size(99, 40);
            btnBersihkan.TabIndex = 1;
            btnBersihkan.Text = "Bersihkan";
            btnBersihkan.UseVisualStyleBackColor = true;
            btnBersihkan.Click += btnBersihkan_Click;
            // 
            // btnSimpan
            // 
            btnSimpan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSimpan.Location = new Point(155, 5);
            btnSimpan.Margin = new Padding(4, 5, 4, 5);
            btnSimpan.Name = "btnSimpan";
            btnSimpan.Size = new Size(99, 40);
            btnSimpan.TabIndex = 0;
            btnSimpan.Text = "Simpan";
            btnSimpan.UseVisualStyleBackColor = true;
            btnSimpan.Click += btnSimpan_Click;
            // 
            // CreateTaskForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 224);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateTaskForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tambah Tugas Baru";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanelButtons.ResumeLayout(false);
            ResumeLayout(false);

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