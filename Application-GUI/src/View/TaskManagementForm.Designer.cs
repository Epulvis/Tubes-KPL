namespace Application.View
{
    partial class TaskManagementForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPageFilterTaskByDate = new Button();
            SuspendLayout();
            // 
            // btnPageFilterTaskByDate
            // 
            btnPageFilterTaskByDate.Location = new Point(46, 91);
            btnPageFilterTaskByDate.Name = "btnPageFilterTaskByDate";
            btnPageFilterTaskByDate.Size = new Size(181, 34);
            btnPageFilterTaskByDate.TabIndex = 0;
            btnPageFilterTaskByDate.Text = "Filter Tanggal Tugas ";
            btnPageFilterTaskByDate.UseVisualStyleBackColor = true;
            btnPageFilterTaskByDate.Click += button1_Click;
            // 
            // TaskManagementForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPageFilterTaskByDate);
            Name = "TaskManagementForm";
            Text = "Aplikasi Manajemen Tugas";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPageFilterTaskByDate;
    }
}
