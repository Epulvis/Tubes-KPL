namespace Application_GUI.src.View
{
    partial class FilteredTaskByDateForm
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblTitle = new Label();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            btnFilter = new Button();
            btnBack = new Button();
            dataGridViewTasks = new DataGridView();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTasks).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblTitle);
            flowLayoutPanel1.Controls.Add(lblStartDate);
            flowLayoutPanel1.Controls.Add(dtpStartDate);
            flowLayoutPanel1.Controls.Add(lblEndDate);
            flowLayoutPanel1.Controls.Add(dtpEndDate);
            flowLayoutPanel1.Controls.Add(btnFilter);
            flowLayoutPanel1.Controls.Add(btnBack);
            flowLayoutPanel1.Controls.Add(dataGridViewTasks);
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(776, 426);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(773, 28);
            lblTitle.TabIndex = 7;
            lblTitle.Text = "Filter Tugas";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(3, 28);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(82, 25);
            lblStartDate.TabIndex = 8;
            lblStartDate.Text = "Tanggal: ";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(91, 31);
            dtpStartDate.MinDate = new DateTime(2025, 6, 8, 6, 22, 15, 0);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(194, 31);
            dtpStartDate.TabIndex = 11;
            dtpStartDate.Value = new DateTime(2025, 6, 8, 6, 22, 15, 0);
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(291, 28);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(19, 25);
            lblEndDate.TabIndex = 10;
            lblEndDate.Text = "-";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(316, 31);
            dtpEndDate.MinDate = new DateTime(2025, 6, 8, 0, 0, 0, 0);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(194, 31);
            dtpEndDate.TabIndex = 9;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(516, 31);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(112, 34);
            btnFilter.TabIndex = 12;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += BtnFilter_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(634, 31);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(112, 34);
            btnBack.TabIndex = 15;
            btnBack.Text = "Kembali";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBack_Click;
            // 
            // dataGridViewTasks
            // 
            dataGridViewTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTasks.Location = new Point(3, 71);
            dataGridViewTasks.Name = "dataGridViewTasks";
            dataGridViewTasks.RowHeadersWidth = 62;
            dataGridViewTasks.Size = new Size(773, 355);
            dataGridViewTasks.TabIndex = 16;
            // 
            // FilteredTaskByDateForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanel1);
            Name = "FilteredTaskByDateForm";
            Text = "-";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTasks).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblTitle;
        private Button btnFilter;
        private DateTimePicker dtpStartDate;
        private Label lblEndDate;
        private DateTimePicker dtpEndDate;
        private Label lblStartDate;
        private Button btnBack;
        private DataGridView dataGridViewTasks;
    }
}
